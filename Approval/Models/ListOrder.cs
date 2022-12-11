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
        public string EnterPassword { get; set; }
    }
    public class AnRegisterUser : RegisterAtUser
    { 
        public string Email { get; set; }

        public string Role { get; set; }
    }
}
