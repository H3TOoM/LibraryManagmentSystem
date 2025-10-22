using LibraryManagmentSystem.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Services.Base
{
    public interface IAccountService
    {
        Task<UserResponseDto> LoginAsync(UserLoginDto dto);
        Task<UserResponseDto> RegisterAsync(UserCreateDto dto);
    }
}
