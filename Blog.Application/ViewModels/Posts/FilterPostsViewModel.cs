using Blog.Application.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Blog.Application.ViewModels.Posts
{
    public class FilterPostsViewModel : Paging<PostListViewModel>
    {
        public string? Search { get; set; }
        public string? CategorySlug { get; set; }
        public PostStatusForAdmin PostStatus { get; set; }
        public PostSortBy PostSortBy { get; set; }

        public PostOrderBy PostOrderBy { get; set; }
        public string? Tag { get; set; }
    }

    public enum PostStatusForAdmin
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "پیش نویس")]
        Draft,
        [Display(Name = "منتشر شده")]
        Published,
        [Display(Name = "بایگانی شده")]
        Archived,
        [Display(Name = "حذف شده")]
        Deleted
    }

    public enum PostSortBy
    {

        [Display(Name = "تاریخ")]
        Date,
        [Display(Name = "بازدید")]
        Visit
    }

    public enum PostOrderBy
    {
        Asc,
        Desc
    }
}
