using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Services.DTOs
{
    public class FineCreateDto
    {
        [Required]
        public int LoanId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
    }

    public class FineUpdateDto
    {
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal? Amount { get; set; }

        public bool? IsPaid { get; set; }
    }

    public class FineResponseDto
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string BookTitle { get; set; }
        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? PaidDate { get; set; }
    }

    public class FineWithDetailsDto : FineResponseDto
    {
        public UserResponseDto User { get; set; }
        public LoanResponseDto Loan { get; set; }
    }

    public class FinePaymentDto
    {
        [Required]
        public int FineId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Payment amount must be greater than 0")]
        public decimal PaymentAmount { get; set; }

        public string? PaymentMethod { get; set; }
        public string? PaymentReference { get; set; }
    }

    public class FineSearchDto
    {
        public int? UserId { get; set; }
        public int? LoanId { get; set; }
        public bool? IsPaid { get; set; }
        public decimal? MinAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }
}
