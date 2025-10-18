using LibraryManagmentSystem.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Services.Base
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanResponseDto>> GetLoansAsync();
        Task<LoanResponseDto> GetLoanByIdAsync(int id);
        Task<LoanResponseDto> CreateLoanAsync(LoanCreateDto loanCreateDto);
        Task<LoanResponseDto> UpdateLoanAsync(int id, LoanUpdateDto loanUpdateDto);
        Task<bool> DeleteLoanAsync(int id);
    }
}
