using LibraryManagmentSystem.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Services.Base
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherResponseDto>> GetPublishersAsync();
        Task<PublisherResponseDto> GetPublisherByIdAsync(int id);
        Task<PublisherResponseDto> CreatePublisherAsync(PublisherCreateDto publisherCreateDto);
        Task<PublisherResponseDto> UpdatePublisherAsync(int id, PublisherUpdateDto publisherUpdateDto);
        Task<bool> DeletePublisherAsync(int id);
    }
}
