using LibraryManagmentSystem.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Services.Base
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorResponseDto>> GetAuthorsAsync();
        Task<AuthorResponseDto> GetAuthorByIdAsync(int id);
        Task<AuthorResponseDto> CreateAuthorAsync(AuthorCreateDto authorCreateDto);
        Task<AuthorResponseDto> UpdateAuthorAsync(int id, AuthorUpdateDto authorUpdateDto);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
