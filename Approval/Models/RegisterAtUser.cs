using System;
using System.ComponentModel.DataAnnotations;

namespace Approval.Models
{
    public class RegisterAtUserModel : AutorizeUserModel
    {
        public Guid IdUser { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
    public class AutorizeUserModel
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string EnterPassword { get; set; }
    }
}
