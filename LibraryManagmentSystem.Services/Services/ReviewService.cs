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
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMainRepoistory<Review> _mainRepository;

        public ReviewService( IUnitOfWork unitOfWork, IMapper mapper , IMainRepoistory<Review> mainRepoistory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mainRepository = mainRepoistory;
        }
        public async Task<IEnumerable<ReviewResponseDto>> GetReviewsAsync()
        {
            var reviews = await _mainRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReviewResponseDto>>( reviews );
        }

        public async Task<ReviewResponseDto> GetReviewByIdAsync( int id )
        {
            ValiditorHelper.ValidateId( id, "Review" );
            var review = await _mainRepository.GetByIdAsync( id );
            ValiditorHelper.EntityNotFoundCheck( review, "Review", id );

            return _mapper.Map<ReviewResponseDto>( review );
        }


        public async Task<ReviewResponseDto> CreateReviewAsync( ReviewCreateDto reviewCreateDto )
        {
            ValiditorHelper.ValidateData( null, reviewCreateDto, "Review" );
            var review = new Review
            {
                BookId = reviewCreateDto.BookId,
                UserId = reviewCreateDto.UserId,
                Rating = reviewCreateDto.Rating,
                Comment = reviewCreateDto.Comment,
            };

            await _mainRepository.AddAsync( review );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReviewResponseDto>( review );
        }

       
        public async Task<ReviewResponseDto> UpdateReviewAsync( int id, ReviewUpdateDto reviewUpdateDto )
        {
            ValiditorHelper.ValidateId( id, "Review" );

            var review = await _mainRepository.GetByIdAsync( id );
            ValiditorHelper.EntityNotFoundCheck( review, "Review", id );

            review.Rating = reviewUpdateDto.Rating ?? review.Rating;
            review.Comment = reviewUpdateDto.Comment ?? review.Comment;

            await _mainRepository.UpdateAsync( id, review );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ReviewResponseDto>( review );
        }

        public async Task<bool> DeleteReviewAsync( int id )
        {
            ValiditorHelper.ValidateId( id, "Review" );
            var result = await _mainRepository.DeleteAsync( id );
            if (!result)
                throw new KeyNotFoundException( "Review not found" );

            await _unitOfWork.SaveChangesAsync();
            return result;
        }

    }
}
