using System;
using System.ComponentModel.DataAnnotations;

namespace Approval.Models
{
    public class RegisterAtUserModel 
    {
        //public string RequestId { get; set; }

        //public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public Guid IdUser { get; set; }

        [Required]
        //[StringLength(20,MinimumLength =3)]
        public string UserName { get; set; }

        [Required]
        //[StringLength(20, MinimumLength = 8)]
        public string EnterPassword { get; set; }

        //[Required]
        //[StringLength(30, MinimumLength = 10)]
        [EmailAddress] 
        public string Email { get; set; }

        public string Role { get; set; }
    }
}
