using Blog.Application.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Blog.Application.ViewModels.Accounts
{
    public class FilterUsersViewModel : Paging<UserViewModel>
    {
        public string? Search { get; set;  }
        public int? RoleId { get; set; }
        public UserStatus Status { get; set; } 
        public UserOrderBy OrderBy { get; set; }
    }
    public enum UserStatus
    {
        [Display(Name = "همه")]
        All,
        [Display(Name = "فعال")]
        Active,
        [Display(Name = "غیرفعال")]
        Banned,
        [Display(Name = "بن")]
        NotActive
    }
    public enum UserOrderBy 
    {
        [Display(Name ="جدیدترین")]
        Date_Descending,
        [Display(Name = "قدیمی ترین")]
        Date_Ascending,
    }
}
