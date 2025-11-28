using Blog.Application.Extensions;
using Blog.Application.Services.Interfaces;
using Blog.Application.Statics;
using Blog.Application.ViewModels.Posts;
using Blog.Data.Entites.Blog;
using Blog.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Services.Implementation
{
    public class PostService : IPostService
    {
        #region Constructor

        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        #endregion

        public async Task<FilterPostsViewModel> FilterPostsAsync(FilterPostsViewModel filter)
        {
            var query = _postRepository.GetAllQueryable();

            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(q => q.Title.Contains(filter.Search));
            }

            if (!string.IsNullOrEmpty(filter.CategorySlug))
            {
                query = query.Where(q => q.Category.Slug.Contains(filter.CategorySlug));
            }


            switch (filter.PostStatus)
            {
                case PostStatusForAdmin.All:
                    break;
                case PostStatusForAdmin.Draft:
                    query = query.Where(q => q.Status == Data.Entites.Blog.PostStatus.Draft);
                    break;
                case PostStatusForAdmin.Published:
                    query = query.Where(q => q.Status == Data.Entites.Blog.PostStatus.Published);
                    break;
                case PostStatusForAdmin.Archived:
                    query = query.Where(q => q.Status == Data.Entites.Blog.PostStatus.Archived);
                    break;
                case PostStatusForAdmin.Deleted:
                    query = query.Where(q => q.Status == Data.Entites.Blog.PostStatus.Deleted);
                    break;
            }

            switch (filter.PostSortBy)
            {
                case PostSortBy.Date:
                    query = filter.PostOrderBy == PostOrderBy.Desc
                        ? query.OrderByDescending(x => x.UpdatedAt)
                        : query.OrderBy(x => x.UpdatedAt);
                    break;
                case PostSortBy.Visit:
                    query = filter.PostOrderBy != PostOrderBy.Desc ?
                        query.OrderByDescending(x => x.Visit) :
                        query.OrderBy(x => x.Visit);
                    break;
            }


            var items = query.Select(q => new PostListViewModel
            {
                Id = q.Id,
                LastUpdateDate = q.UpdatedAt,
                Slug = q.Slug,
                Title = q.Title,
                Status = q.Status,
                IsDeleted = q.IsDeleted,
                CategoryTitle = q.Category.Title,
                CategoryBootstarpClass = q.Category.CategoryColor.BootstrapClassName,
                MainImage = q.MainImage,
                ShortDescription = q.ShortDescription
            });
           
            await filter.SetPagingAsync(items);

            return filter;
        }

        public async Task<CreatePostResult> CreatePostAsync(CreatePostViewModel create)
        {
            var newPost = new Post
            {
                ShortDescription = create.ShortDescription,
                Description = create.Description,
                CategoryId = create.CategoryId,
                Title = create.Title,
                Slug = create.Title.GenerateSlug(),
                Status = create.Status,
            };

            var imageName = create.MainImage.FileNameGenerator();
            var upResult = await create.MainImage.UploadImage(imageName, PathTools.PostOrgImageServerPath, PathTools.PostThumbImageServerPath);
            if (!upResult)
                return CreatePostResult.InvalidImage;

            newPost.MainImage = imageName;

            await _postRepository.AddAsync(newPost);
            await _postRepository.SaveChangesAsync();

            await AddTagsToPostAsync(create.Tags, newPost.Id);

           

            return CreatePostResult.Success;
        }

        public async Task<List<string>> GetTagTitlesAsync(string term)
        {
            return await _postRepository.GetTagTitlesAsync(term);
        }

        public async Task AddTagsToPostAsync(List<string> tags, int postId)
        {
            if (tags != null)
            {
                foreach (var item in tags)
                {
                    var tag = await _postRepository.CheckTagExisted(item);
                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Title = item,
                            Slug = item.GenerateSlug(),
                        };
                        await _postRepository.AddTagAsync(tag);
                        await _postRepository.SaveChangesAsync();
                    }

                    await _postRepository.AddPostTagAsync(new PostTag
                    {
                        PostId = postId,
                        TagId = tag.Id
                    });

                }
                await _postRepository.SaveChangesAsync();

            }
        }

        public async Task<EditPostViewModel?> GetPostForEditAsync(int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
                return null;
            return new EditPostViewModel
            {
                CategoryId = post.CategoryId,
                Description = post.Description,
                Tags = await _postRepository.GetSelectedTagTitlesAsync(post.Id),
                MainImage = post.MainImage,
                ShortDescription = post.ShortDescription,
                Title = post.Title,
                Id = post.Id
            };
        }

        public async Task<EditPostResult> EditPostAsync(EditPostViewModel edit)
        {
            var postToEdit = await _postRepository.GetByIdAsync(edit.Id);
            if (postToEdit == null)
                return EditPostResult.NotFound;

            postToEdit.Title = edit.Title;
            postToEdit.ShortDescription = edit.ShortDescription;
            postToEdit.Description = edit.Description;
            postToEdit.CategoryId = edit.CategoryId;
            postToEdit.Status = edit.Status;
            postToEdit.Slug = edit.Title.GenerateSlug();
            postToEdit.UpdatedAt = DateTime.Now;

            if (edit.NewMainImage != null)
            {
                postToEdit.MainImage.DeleteImage(PathTools.PostOrgImageServerPath, PathTools.PostThumbImageServerPath);
                var imageName = edit.NewMainImage.FileNameGenerator();
                var upResult = await edit.NewMainImage.UploadImage(imageName, PathTools.PostOrgImageServerPath, PathTools.PostThumbImageServerPath);
                if (!upResult)
                    return EditPostResult.InvalidImage;
                postToEdit.MainImage = imageName;
            }
            _postRepository.Update(postToEdit);
            await _postRepository.SaveChangesAsync();

            _postRepository.RemoveTagsFromPostByPostId(edit.Id);
            await _postRepository.SaveChangesAsync();

            await AddTagsToPostAsync(edit.Tags, edit.Id);

            return EditPostResult.Success;
        }

        public async Task<bool> ToggleDeletePostAsync(int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null) return false;
            post.IsDeleted = !post.IsDeleted;

            post.Status = post.IsDeleted ? PostStatus.Deleted : PostStatus.Archived;

            _postRepository.Update(post);
            await _postRepository.SaveChangesAsync();
            return true;
        }

        public async Task<List<LatestPostViewModel>> GetLatestPostAsync(int take)
        {
            var posts = await _postRepository.GetLatestAsync(take);
            return posts.Select(p => new LatestPostViewModel
            {
                Category = p.Category,
                PublishedDate = p.UpdatedAt,
                Slug = p.Slug,
                Title = p.Title,
                MainImage = p.MainImage
            }).ToList();
        }

        public async Task<PostViewModel?> GetPostBySlugAsync(string slug)
        {
            var post = await _postRepository.GetBySlygAsync(slug);
            if (post == null)
                return null;
            return new PostViewModel
            {
                Category = post.Category,
                DatePublished = post.UpdatedAt,
                Description = post.Description,
                ShortDescription = post.ShortDescription,
                Slug = post.Slug,
                Tags = await _postRepository.GetSelectedTagTitlesAsync(post.Id),
                Title = post.Title,
                MainImage = post.MainImage,
                Id = post.Id
            };
        }
    }
}
