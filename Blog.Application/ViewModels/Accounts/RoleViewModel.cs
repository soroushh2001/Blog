using Blog.Data.Entites.User;

namespace Blog.Application.ViewModels.Accounts
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public List<Permission> Permissions { get; set; } = null!;
        public string PersianTitle { get; set; } = null!;
        public string EnglishTitle { get; set; } = null!;
    }
}
