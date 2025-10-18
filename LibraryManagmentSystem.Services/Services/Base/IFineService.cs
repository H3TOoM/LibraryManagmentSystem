using LibraryManagmentSystem.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Services.Base
{
    public interface IFineService
    {
        Task<IEnumerable<FineResponseDto>> GetFinesAsync();
        Task<FineResponseDto> GetFineByIdAsync(int id);
        Task<FineResponseDto> CreateFineAsync(FineCreateDto fineCreateDto);
        Task<FineResponseDto> UpdateFineAsync(int id, FineUpdateDto fineUpdateDto);
        Task<bool> DeleteFineAsync(int id);
    }
}
