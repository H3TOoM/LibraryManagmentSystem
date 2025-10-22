using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Data.Entities
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        [StringLength(100)]
        [Url]
        [Required]
        public string Website { get; set; }
        
        // Navigation properties
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
