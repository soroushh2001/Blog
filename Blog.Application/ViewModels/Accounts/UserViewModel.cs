namespace Blog.Application.ViewModels.Accounts
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get;set;  }
        public string Email { get; set;  }
        public DateTime RegisterDate { get; set; }
        public List<string> Roles { get; set; }
        public bool IsBanned { get; set; }
        public bool IsActive { get; set; }
    }
}
