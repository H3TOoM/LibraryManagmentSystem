using System.ComponentModel.DataAnnotations;

namespace LibraryManagmentSystem.Services.DTOs
{
    public class CategoryCreateDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
    }

    public class CategoryUpdateDto
    {
        [StringLength(50, MinimumLength = 3)]
        public string? Name { get; set; }
    }

    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BooksCount { get; set; }
    }

    public class CategoryWithBooksDto : CategoryResponseDto
    {
        public List<BookResponseDto> Books { get; set; } = new List<BookResponseDto>();
    }
}
