using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Data.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(2)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
