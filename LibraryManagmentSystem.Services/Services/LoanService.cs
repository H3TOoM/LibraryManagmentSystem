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
    public class LoanService : ILoanService
    {
        private readonly IMainRepoistory<Loan> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LoanService( IMainRepoistory<Loan> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<LoanResponseDto>> GetLoansAsync()
        {
            var loans = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<LoanResponseDto>>( loans );
        }

        public async Task<LoanResponseDto> GetLoanByIdAsync( int id )
        {
            ValiditorHelper.ValidateId( id, "Loan" );
            var loan = await _mainRepoistory.GetByIdAsync( id );
            ValiditorHelper.EntityNotFoundCheck( loan, "Loan", id );

            return _mapper.Map<LoanResponseDto>( loan );
        }

        public async Task<LoanResponseDto> CreateLoanAsync( LoanCreateDto loanCreateDto )
        {
            ValiditorHelper.ValidateData( null, loanCreateDto, "Loan" );
            var loan = new Loan
            {
                UserId = loanCreateDto.UserId,
                BookId = loanCreateDto.BookId,
                DueDate = loanCreateDto.DueDate,
            };

            await _mainRepoistory.AddAsync( loan );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<LoanResponseDto>( loan );
        }

        public async Task<LoanResponseDto> UpdateLoanAsync( int id, LoanUpdateDto loanUpdateDto )
        {
            ValiditorHelper.ValidateId( id, "Loan" );
            var loan = await _mainRepoistory.GetByIdAsync( id );
            ValiditorHelper.EntityNotFoundCheck( loan, "Loan", id );

            loan.DueDate = loanUpdateDto.DueDate ?? loan.DueDate;
            loan.ReturnDate = loanUpdateDto.ReturnDate ?? loan.ReturnDate;

            await _mainRepoistory.UpdateAsync( id, loan );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<LoanResponseDto>( loan );
        }

        public async Task<bool> DeleteLoanAsync( int id )
        {
            ValiditorHelper.ValidateId( id, "Loan" );
            var result = await _mainRepoistory.DeleteAsync( id );
            if (!result)
                throw new KeyNotFoundException( "Loan not found" );

            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
