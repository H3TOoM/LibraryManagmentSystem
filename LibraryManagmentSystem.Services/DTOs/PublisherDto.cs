using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Services.DTOs
{
    public class PublisherCreateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        [Url]
        public string Website { get; set; }
    }

    public class PublisherUpdateDto
    {
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [StringLength(100)]
        [Url]
        public string? Website { get; set; }
    }

    public class PublisherResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string Website { get; set; }
        public int BooksCount { get; set; }
    }

    public class PublisherWithBooksDto : PublisherResponseDto
    {
        public List<BookResponseDto> Books { get; set; } = new List<BookResponseDto>();
    }
}
