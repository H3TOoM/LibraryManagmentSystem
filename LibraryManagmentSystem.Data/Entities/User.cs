using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string FirstName { get; set; }
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [MinLength(2)]
        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateOfBirth { get; set; }

       


    }
}
