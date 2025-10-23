using AutoMapper;
using LibraryManagmentSystem.Data.Entities;
using LibraryManagmentSystem.Infrasturcture.Repoistories.Base;
using LibraryManagmentSystem.Services.DTOs;
using LibraryManagmentSystem.Services.Extentions;
using LibraryManagmentSystem.Services.Helpers;
using LibraryManagmentSystem.Services.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMainRepoistory<Author> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthorService( IMainRepoistory<Author> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorResponseDto>> GetAuthorsAsync()
        {
            var authors = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorResponseDto>>( authors );
        }

        public async Task<AuthorResponseDto> GetAuthorByIdAsync( int id )
        {
            var author = await _mainRepoistory.GetByIdAsync( id );
            return _mapper.Map<AuthorResponseDto>( author );
        }

        public async Task<AuthorResponseDto> CreateAuthorAsync( AuthorCreateDto authorCreateDto )
        {

            ValiditorHelper.ValidateData( null, authorCreateDto, "Author" );

            var author = new Author
            {
                FirstName = authorCreateDto.FirstName,
                LastName = authorCreateDto.LastName ?? string.Empty
            };

            await _mainRepoistory.AddAsync( author );
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AuthorResponseDto>( author );
        }

        public async Task<AuthorResponseDto> UpdateAuthorAsync( int id, AuthorUpdateDto authorUpdateDto )
        {           
            ValiditorHelper.ValidateData( id, authorUpdateDto, "Author" );

            var author = await _mainRepoistory.GetByIdAsync( id );
            if (author == null)
                throw new KeyNotFoundException( $"Author with id {id} not found." );

            author.FirstName = authorUpdateDto.FirstName ?? author.FirstName;
            author.LastName = authorUpdateDto.LastName ?? author.LastName;

            await _mainRepoistory.UpdateAsync( id, author );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AuthorResponseDto>( author );
        }

        public async Task<bool> DeleteAuthorAsync( int id )
        {
            if (id <= 0)
                throw new ArgumentException( "Invalid author id" );

            var result = await _mainRepoistory.DeleteAsync( id );
            if (!result)
                throw new KeyNotFoundException( $"Author with id {id} not found." );

            await _unitOfWork.SaveChangesAsync();
            return result;

        }



    }
}
