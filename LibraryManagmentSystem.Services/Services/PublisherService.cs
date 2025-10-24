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
    public class PublisherService : IPublisherService
    {
        private readonly IMainRepoistory<Publisher> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PublisherService( IMainRepoistory<Publisher> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PublisherResponseDto>> GetPublishersAsync()
        {
            var publishers = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<PublisherResponseDto>>( publishers );
        }


        public async Task<PublisherResponseDto> GetPublisherByIdAsync( int id )
        {
            ValiditorHelper.ValidateId( id, "Publisher" );
            var publisher = await _mainRepoistory.GetByIdAsync( id );
            ValiditorHelper.EntityNotFoundCheck( publisher, "Publisher", id );

            return _mapper.Map<PublisherResponseDto>( publisher );
        }


        public async Task<PublisherResponseDto> CreatePublisherAsync( PublisherCreateDto publisherCreateDto )
        {
            ValiditorHelper.ValidateData( null, publisherCreateDto, "Publisher" );
            var publisher = new Publisher
            {
                Name = publisherCreateDto.Name,
                Address = publisherCreateDto.Address,
                PhoneNumber = publisherCreateDto.PhoneNumber,
                Website = publisherCreateDto.Website
            };

            await _mainRepoistory.AddAsync( publisher );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PublisherResponseDto>( publisher );
        }

        public async Task<PublisherResponseDto> UpdatePublisherAsync( int id, PublisherUpdateDto publisherUpdateDto )
        {
            ValiditorHelper.ValidateData( id, publisherUpdateDto, "Publisher" );
            var publisher = await _mainRepoistory.GetByIdAsync( id );
            ValiditorHelper.EntityNotFoundCheck( publisher, "Publisher", id );

            publisher.Name = publisherUpdateDto.Name ?? publisher.Name;
            publisher.Address = publisherUpdateDto.Address ?? publisher.Address;
            publisher.PhoneNumber = publisherUpdateDto.PhoneNumber ?? publisher.PhoneNumber;
            publisher.Website = publisherUpdateDto.Website ?? publisher.Website;

            await _mainRepoistory.UpdateAsync( id, publisher );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PublisherResponseDto>( publisher );
        }

        public async Task<bool> DeletePublisherAsync( int id )
        {
            ValiditorHelper.ValidateId( id, "Publisher" );
            var result = await _mainRepoistory.DeleteAsync( id );
            if (!result)
                throw new KeyNotFoundException( "Publisher not found" );

            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
