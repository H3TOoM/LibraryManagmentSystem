using AutoMapper;
using LibraryManagmentSystem.Data.Entities;
using LibraryManagmentSystem.Infrasturcture.Repoistories.Base;
using LibraryManagmentSystem.Services.DTOs;
using LibraryManagmentSystem.Services.Helpers;
using LibraryManagmentSystem.Services.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Services
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMainRepoistory<User> _mainRepository;
        #endregion

        #region Constructor
        public UserService( IUnitOfWork unitOfWork, IMainRepoistory<User> mainRepoistory )
        {
            _unitOfWork = unitOfWork;
            _mainRepository = mainRepoistory;
        }
        #endregion


        #region Methods
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _mainRepository.GetAllAsync();
            return users;
        }

        public async Task<User> GetUserByIdAsync( int id )
        {
            ValiditorHelper.ValidateId( id, "User" );
            var user = await _mainRepository.GetByIdAsync( id );
            ValiditorHelper.EntityNotFoundCheck( user, "User", id );

            return user;
        }



        public async Task<User> CreateUserAsync( UserCreateDto userCreateDto )
        {
            ValiditorHelper.ValidateData( null, userCreateDto, "User" );
            var user = new User
            {
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                Email = userCreateDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword( userCreateDto.Password ),
                DateOfBirth = userCreateDto.DateOfBirth,
            };

            await _mainRepository.AddAsync( user );
            await _unitOfWork.SaveChangesAsync();

            return user;
        }



        public async Task<User> UpdateUserAsync( int id, UserUpdateDto userUpdateDto )
        {
            ValiditorHelper.ValidateId( id, "User" );
            var user = await _mainRepository.GetByIdAsync( id );
            ValiditorHelper.EntityNotFoundCheck( user, "User", id );

            user.FirstName = userUpdateDto.FirstName ?? user.FirstName;
            user.LastName = userUpdateDto.LastName ?? user.LastName;
            user.Email = userUpdateDto.Email ?? user.Email;
            if (userUpdateDto.Password != null)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword( userUpdateDto.Password );
            }
            user.DateOfBirth = userUpdateDto.DateOfBirth ?? user.DateOfBirth;

            await _mainRepository.UpdateAsync( id, user );
            await _unitOfWork.SaveChangesAsync();

            return user;
        }

        public async Task<bool> DeleteUserAsync( int id )
        {
            ValiditorHelper.ValidateId( id, "User" );
            var result = await _mainRepository.DeleteAsync( id );
            if (!result)
                throw new KeyNotFoundException( $"User with id {id} not found." );

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        #endregion

    }
}
