using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectStoreBlazor.Shared.Models
{
    public class RegisterUserDto
    {
        [MinLength(3, ErrorMessage = "Username length must be between 3-32")]
        [MaxLength(32, ErrorMessage = "Username length must be between 3-32")]
        public string Username { get; set; }
        [MinLength(1)]
        [MaxLength(32)]
        public string Name { get; set; }
        [MinLength(1, ErrorMessage = "Name length must be between 1-32")]
        [MaxLength(32, ErrorMessage = "Name length must be between 1-32")]
        public string Surname { get; set; }

        [MinLength(1, ErrorMessage = "Surname length must be between 1-32")]
        [MaxLength(32, ErrorMessage = "Surname length must be between 1-32")]

        public string Password { get; set; }
        [MinLength(6, ErrorMessage = "Password length must be between 6-32")]
        [MaxLength(32)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public int RoleId { get; set; }=1;
        public DateTime Birthday { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
