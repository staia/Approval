using System;

namespace Approval.Models
{
    public class ListOrder 
    {
    }

    public class RegisterAtUser
    {
        public Guid Hash { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class AnRegisterUser
    { 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
