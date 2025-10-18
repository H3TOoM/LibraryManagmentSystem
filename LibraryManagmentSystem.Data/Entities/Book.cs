using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Data.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(70)]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }
        
        public Author Author { get; set; }

        [Required]
        public int categoryId { get; set; }

        public Category Category { get; set; }
        
        public DateTime PublishedDate { get; set; } 
        public int CopiesAvailable { get; set; }
    }
}
