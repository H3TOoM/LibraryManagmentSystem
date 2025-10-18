using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Data.Entities
{
    public class Fine
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int LoanId { get; set; }
        public Loan Loan { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public decimal Amount { get; set; }

       
        public bool IsPaid { get; set; } = false;
    }
}
