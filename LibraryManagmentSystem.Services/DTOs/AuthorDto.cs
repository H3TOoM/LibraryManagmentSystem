using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Services.DTOs
{
    public class AuthorCreateDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? LastName { get; set; }
    }

    public class AuthorUpdateDto
    {
        [StringLength(50, MinimumLength = 2)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }
    }

    public class AuthorResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
        public int BooksCount { get; set; }
    }

    public class AuthorWithBooksDto : AuthorResponseDto
    {
        public List<BookResponseDto> Books { get; set; } = new List<BookResponseDto>();
    }
}
