using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Services.Services.Base
{
    public interface IAccountService
    {
        Task<string> LoginAsync(string username, string password);
        Task<bool> RegisterAsync(string username, string password, string email);
    }
}
