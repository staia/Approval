using System;
using System.ComponentModel.DataAnnotations;

namespace Approval.Models
{
    public class RegisterAtUserModel : AutorizeUserModel    //для нового
    {
        public Guid IdUser { get; set; }

        [Required]
        //[StringLength(20,MinimumLength =3)]
        public string UserName { get; set; }

        public string Role { get; set; }

    }
    public class AutorizeUserModel         //авторизация
    {
        //[Required]
        //[StringLength(30, MinimumLength = 10)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //[StringLength(20, MinimumLength = 8)]
        public string EnterPassword { get; set; }
    }
}
