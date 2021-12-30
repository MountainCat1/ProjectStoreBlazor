using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectStoreBlazor.Server.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }
        
        public DateTime Birthday { get; set; }
        public int PhoneNumber { get; set; }

        public string PasswordHash {get; set; }
        public int RoleId { get; set; }


        public virtual Address Address { get; set; }
        public virtual Role Role { get; set; }
        public virtual List<Product> Products { get; set; }

    }
}
