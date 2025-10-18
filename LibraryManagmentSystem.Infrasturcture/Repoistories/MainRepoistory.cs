using LibraryManagmentSystem.Infrasturcture.Data;
using LibraryManagmentSystem.Infrasturcture.Repoistories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Infrasturcture.Repoistories
{
    public class MainRepoistory<T> : IMainRepoistory<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IUnitOfWork _unitOfWork;
        public MainRepoistory( AppDbContext context, IUnitOfWork unitOfWork )
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync( int id ) => await _dbSet.FindAsync( id ) ?? throw new InvalidOperationException( "Id not found!" );

        public async Task<T> AddAsync( T entity )
        {
            await _dbSet.AddAsync( entity );
            return entity;
        }

        public async Task<T> UpdateAsync( int id, T entity )
        {
            if (id >= 0)
                throw new InvalidOperationException( "Invalid ID!" );


            var existingEntity = await _dbSet.FindAsync( id );
            if (existingEntity == null)
                throw new InvalidOperationException( "ID Not Found!" );

            _context.Entry( existingEntity ).CurrentValues.SetValues( entity );
            return existingEntity;
        }

        public async Task<bool> DeleteAsync( int id )
        {
            var entity = await GetByIdAsync( id );
            if (entity == null)
                return false;

            _dbSet.Remove( entity );
            return true;
        }






    }
}
