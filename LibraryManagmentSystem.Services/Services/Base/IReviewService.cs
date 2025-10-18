using LibraryManagmentSystem.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Services.Base
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewResponseDto>> GetReviewsAsync();
        Task<ReviewResponseDto> GetReviewByIdAsync(int id);
        Task<ReviewResponseDto> CreateReviewAsync(ReviewCreateDto reviewCreateDto);
        Task<ReviewResponseDto> UpdateReviewAsync(int id, ReviewUpdateDto reviewUpdateDto);
        Task<bool> DeleteReviewAsync(int id);
    }
}
