using AutoMapper;
using LibraryManagmentSystem.Data.Entities;
using LibraryManagmentSystem.Infrasturcture.Repoistories.Base;
using LibraryManagmentSystem.Services.DTOs;
using LibraryManagmentSystem.Services.Extentions;
using LibraryManagmentSystem.Services.Helpers;
using LibraryManagmentSystem.Services.Services.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibraryManagmentSystem.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IMainRepoistory<Book> _mainRepoistory;
        private readonly IMainRepoistory<Author> _authorRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService( IMainRepoistory<Book> mainRepoistory, IMainRepoistory<Author> authorRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _authorRepoistory = authorRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookResponseDto>> GetBooksAsync()
        {
            var books = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<BookResponseDto>>( books );
        }

        public async Task<BookResponseDto> GetBookByIdAsync( int id )
        {
            var book = await _mainRepoistory.GetByIdAsync( id );
            return _mapper.Map<BookResponseDto>( book );
        }



        public async Task<BookResponseDto> CreateBookAsync( BookCreateDto bookCreateDto )
        {

           ValiditorHelper.ValidateData( null, bookCreateDto, "Book" );

            var book = new Book
            {
                Title = bookCreateDto.Title,
                AuthorId = bookCreateDto.AuthorId,
                categoryId = bookCreateDto.CategoryId,
                PublishedDate = bookCreateDto.PublishedDate,
                CopiesAvailable = bookCreateDto.CopiesAvailable
            };

            await _mainRepoistory.AddAsync( book );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookResponseDto>( book );
        }

        public async Task<BookResponseDto> UpdateBookAsync( int id, BookUpdateDto bookUpdateDto )
        {
            ValiditorHelper.ValidateData( id, bookUpdateDto, "Book" );
            var book = await _mainRepoistory.GetByIdAsync( id );
            if (book == null)
                throw new KeyNotFoundException( "Book not found" );

            book.AuthorId = bookUpdateDto.AuthorId ?? book.AuthorId;
            book.categoryId = bookUpdateDto.CategoryId ?? book.categoryId;
            book.Title = bookUpdateDto.Title ?? book.Title;
            book.PublishedDate = bookUpdateDto.PublishedDate ?? book.PublishedDate;
            book.CopiesAvailable = bookUpdateDto.CopiesAvailable ?? book.CopiesAvailable;

            await _mainRepoistory.UpdateAsync( id, book );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookResponseDto>( book );
        }

        public async Task<bool> DeleteBookAsync( int id )
        {
            if (id <= 0)
                throw new ArgumentException( "Invalid book id" );

            var result = await _mainRepoistory.DeleteAsync( id );
            if (!result)
                throw new KeyNotFoundException( "Book not found" );

            await _unitOfWork.SaveChangesAsync();
            return result;
        }


    }
}
