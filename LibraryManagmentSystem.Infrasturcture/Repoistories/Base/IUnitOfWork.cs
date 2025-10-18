using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Infrasturcture.Repoistories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IMainRepoistory<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
    }
}
