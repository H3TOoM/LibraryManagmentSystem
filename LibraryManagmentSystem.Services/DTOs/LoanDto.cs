using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Services.DTOs
{
    public class LoanCreateDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }

    public class LoanUpdateDto
    {
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

    public class LoanResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsOverdue { get; set; }
        public int DaysOverdue { get; set; }
    }

    public class LoanWithDetailsDto : LoanResponseDto
    {
        public UserResponseDto User { get; set; }
        public BookResponseDto Book { get; set; }
        public List<FineResponseDto> Fines { get; set; } = new List<FineResponseDto>();
    }

    public class LoanReturnDto
    {
        [Required]
        public int LoanId { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

    public class LoanSearchDto
    {
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public DateTime? LoanDateFrom { get; set; }
        public DateTime? LoanDateTo { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
        public bool? IsOverdue { get; set; }
        public bool? IsReturned { get; set; }
    }
}
