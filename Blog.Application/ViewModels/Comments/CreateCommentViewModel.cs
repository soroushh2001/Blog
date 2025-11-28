namespace Blog.Application.ViewModels.Comments
{
    public class CreateCommentViewModel
    {
        public int PostId { get; set; }
        public string Text { get; set; } = null!;
        public int? ParentId { get; set; }

        public string PostSlug { get; set; } = null!;
    }
}
