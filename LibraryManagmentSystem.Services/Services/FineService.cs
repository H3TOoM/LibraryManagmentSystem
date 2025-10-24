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
    public class FineService : IFineService
    {
        private readonly IMainRepoistory<Fine> _mainRepoistory;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public FineService( IMainRepoistory<Fine> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<FineResponseDto>> GetFinesAsync()
        {
            var fines = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<FineResponseDto>>( fines );
        }

        public async Task<FineResponseDto> GetFineByIdAsync( int id )
        {
            ValiditorHelper.ValidateId( id, "Fine" );
            var fine = await _mainRepoistory.GetByIdAsync( id );
            ValiditorHelper.EntityNotFoundCheck( fine, "Fine", id );

            return _mapper.Map<FineResponseDto>( fine );
        }

        public async Task<FineResponseDto> CreateFineAsync( FineCreateDto fineCreateDto )
        {
            ValiditorHelper.ValidateData( null, fineCreateDto, "Fine" );
            var fine = new Fine
            {
                LoanId = fineCreateDto.LoanId,
                UserId = fineCreateDto.UserId,
                Amount = fineCreateDto.Amount,
                IsPaid = false,
                CreatedDate = DateTime.UtcNow
            };
            await _mainRepoistory.AddAsync( fine );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<FineResponseDto>( fine );
        }

        public async Task<FineResponseDto> UpdateFineAsync( int id, FineUpdateDto fineUpdateDto )
        {
            ValiditorHelper.ValidateData( id, fineUpdateDto, "Fine" );
            var fine = await _mainRepoistory.GetByIdAsync( id );
            ValiditorHelper.EntityNotFoundCheck( fine, "Fine", id );

            fine.Amount = fineUpdateDto.Amount ?? fine.Amount;
            fine.IsPaid = fineUpdateDto.IsPaid ?? fine.IsPaid;

            await _mainRepoistory.UpdateAsync( id, fine );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<FineResponseDto>( fine );
        }

        public async Task<bool> DeleteFineAsync( int id )
        {
            ValiditorHelper.ValidateId( id, "Fine" );
            var result = await _mainRepoistory.DeleteAsync( id );
            if (!result)
                throw new KeyNotFoundException( $"Fine with id {id} not found." );

            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
