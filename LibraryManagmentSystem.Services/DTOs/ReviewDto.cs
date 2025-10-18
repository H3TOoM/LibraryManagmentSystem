using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Services.DTOs
{
    public class ReviewCreateDto
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string? Comment { get; set; }
    }

    public class ReviewUpdateDto
    {
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int? Rating { get; set; }

        [StringLength(1000)]
        public string? Comment { get; set; }
    }

    public class ReviewResponseDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }

    public class ReviewWithDetailsDto : ReviewResponseDto
    {
        public UserResponseDto User { get; set; }
        public BookResponseDto Book { get; set; }
    }

    public class ReviewSearchDto
    {
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public int? MinRating { get; set; }
        public int? MaxRating { get; set; }
        public DateTime? ReviewDateFrom { get; set; }
        public DateTime? ReviewDateTo { get; set; }
        public bool? HasComment { get; set; }
    }

    public class BookRatingSummaryDto
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public int Rating1Count { get; set; }
        public int Rating2Count { get; set; }
        public int Rating3Count { get; set; }
        public int Rating4Count { get; set; }
        public int Rating5Count { get; set; }
    }
}
