using AutoMapper;
using LibraryManagmentSystem.Data.Entities;
using LibraryManagmentSystem.Infrasturcture.Repoistories.Base;
using LibraryManagmentSystem.Services.DTOs;
using LibraryManagmentSystem.Services.Extentions;
using LibraryManagmentSystem.Services.Helpers;
using LibraryManagmentSystem.Services.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMainRepoistory<Category> _mainRepoistory;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public CategoryService( IMainRepoistory<Category> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync()
        {
            var categories = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryResponseDto>>( categories );
        }

        public async Task<CategoryResponseDto> GetCategoryByIdAsync( int id )
        {
            var category = await _mainRepoistory.GetByIdAsync( id );
            return _mapper.Map<CategoryResponseDto>( category );
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync( CategoryCreateDto categoryCreateDto )
        {

            ValiditorHelper.ValidateData( null, categoryCreateDto , "Category");

            var category = new Category
            {
                Name = categoryCreateDto.Name
            };
            await _mainRepoistory.AddAsync( category );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryResponseDto>( category );
        }

        public async Task<CategoryResponseDto> UpdateCategoryAsync( int id, CategoryUpdateDto categoryUpdateDto )
        {
            
            ValiditorHelper.ValidateId( id  , "Category" );

            var category = await _mainRepoistory.GetByIdAsync( id );
            ValiditorHelper.EntityNotFoundCheck( category, "Category", id );

            category.Name = categoryUpdateDto.Name ?? category.Name;
            await _mainRepoistory.UpdateAsync( id, category );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryResponseDto>( category );
        }

        public async Task<bool> DeleteCategoryAsync( int id )
        {
            ValiditorHelper.ValidateId( id , "Category" );

            var result = await _mainRepoistory.DeleteAsync( id );
            if (!result)
                throw new KeyNotFoundException( "Category not found" );

            await _unitOfWork.SaveChangesAsync();
            return result;
        }



    }
}
