namespace Blog.Application.Statics
{
    public static class PathTools
    {
        #region Category
        public const string CategoryOrgImagePath = "/content/category/org/";
        public static string CategoryOrgImageServerPath =
            Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{CategoryOrgImagePath}");
        public const string CategoryThumbImagePath = "/content/category/thumb/";
        public static string CategoryThumbImageServerPath =
            Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{CategoryThumbImagePath}");
        #endregion

        #region Post

        public const string PostOrgImagePath = "/content/post/org/";
        public static string PostOrgImageServerPath =
            Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{PostOrgImagePath}");
        public const string PostThumbImagePath = "/content/post/thumb/";
        public static string PostThumbImageServerPath =
            Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{PostThumbImagePath}");


        #endregion

        #region RichTextEditor

        public const string RichTextEditorContentPath = "/content/editor/";
        public static string RichTextEditorContentServerPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{RichTextEditorContentPath}");

        #endregion

        #region UserAvatar

        public const string UserAvatarOrgPath = "/content/user/org/";
        public static string UserAvatarOrgServerPath =
            Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{UserAvatarOrgPath}");
        public const string UserAvatarThumbPath = "/content/user/thumb/";
        public static string UserAvatarThumbServerPath =
            Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{UserAvatarThumbPath}");

        #endregion

    }
}
