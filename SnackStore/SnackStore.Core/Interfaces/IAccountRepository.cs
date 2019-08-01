using SnackStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnackStore.Core.Interfaces
{
    public interface IAccountRepository: IRepository<Account>
    {
        Task<Account> FindByUserName(string user);
        Task<Account> FinbByCredentials(string user, string password);
        Task Register(Account account);
    }
}
