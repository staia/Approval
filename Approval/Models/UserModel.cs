using System;
using System.ComponentModel.DataAnnotations;

namespace Approval.Models
{
    public class UserModel
    {
        public string Message { get; set; }
        public Guid IdUser { get; set; }    

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }   

        
        public string Role { get; set; }    
    }
}
