using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Services.DTOs
{
    public class BookCreateDto
    {
        [Required]
        [StringLength(70, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Copies available must be at least 1")]
        public int CopiesAvailable { get; set; }
    }

    public class BookUpdateDto
    {
        [StringLength(70, MinimumLength = 3)]
        public string? Title { get; set; }

        public int? AuthorId { get; set; }

        public int? CategoryId { get; set; }

        public DateTime? PublishedDate { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Copies available cannot be negative")]
        public int? CopiesAvailable { get; set; }
    }

    public class BookResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime PublishedDate { get; set; }
        public int CopiesAvailable { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
    }

    public class BookWithDetailsDto : BookResponseDto
    {
        public AuthorResponseDto Author { get; set; }
        public CategoryResponseDto Category { get; set; }
        public List<ReviewResponseDto> Reviews { get; set; } = new List<ReviewResponseDto>();
    }

    public class BookSearchDto
    {
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public string? CategoryName { get; set; }
        public DateTime? PublishedFrom { get; set; }
        public DateTime? PublishedTo { get; set; }
        public bool? IsAvailable { get; set; }
    }
}
