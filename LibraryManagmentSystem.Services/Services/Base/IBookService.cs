using LibraryManagmentSystem.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Services.Base
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponseDto>> GetBooksAsync();
        Task<BookResponseDto> GetBookByIdAsync( int id );
        Task<BookResponseDto> CreateBookAsync( BookCreateDto bookCreateDto );
        Task<BookResponseDto> UpdateBookAsync( int id, BookUpdateDto bookUpdateDto );
        Task<bool> DeleteBookAsync( int id );
    }
}
