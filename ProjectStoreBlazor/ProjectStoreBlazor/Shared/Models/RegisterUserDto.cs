using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectStoreBlazor.Shared.Models
{
    public class RegisterUserDto
    {
        
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public int RoleId { get; set; }=1;

        public DateTime Birthday { get; set; }
        public int PhoneNumber { get; set; }
    }
}
