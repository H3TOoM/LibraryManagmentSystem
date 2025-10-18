using LibraryManagmentSystem.Infrasturcture.Data;
using LibraryManagmentSystem.Infrasturcture.Repoistories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Infrasturcture.Repoistories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();

        private readonly AppDbContext _context;
        public UnitOfWork( AppDbContext context )
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }


        public IMainRepoistory<T> Repository<T>() where T : class
        {
            var type = typeof( T );

            // If repository for this entity doesn't exist, create and store it
            if (!_repositories.ContainsKey( type ))
            {
                var repoInstance = new MainRepoistory<T>( _context , this );
                _repositories[type] = repoInstance;
            }

            return (IMainRepoistory<T>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
