using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Data.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [Range(1, 5)]

        public int Rating { get; set; } // e.g., 1 to 5

        [StringLength(1000)]
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}
