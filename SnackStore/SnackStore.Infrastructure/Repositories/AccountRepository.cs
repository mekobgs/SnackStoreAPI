using Microsoft.EntityFrameworkCore;
using SnackStore.Core.Interfaces;
using SnackStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnackStore.Infrastructure.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(SnackStoreDbContext context, IEventDispatcher domainEventsDispatcher) : base(context, domainEventsDispatcher)
        {
        }

        public async Task<Account> FindByUserName(string userName)
        {
            return await FindByExpression(p => p.User == userName).FirstOrDefaultAsync();
        }

        public async Task<Account> FindByCredentials(string userName, string password)
        {
            return await FindByExpression(p => p.User == userName && p.Password == password).FirstOrDefaultAsync();
        }

        public async Task Register(Account accountEntity)
        {
            Add(accountEntity);
            await SaveAsync();
        }
    }
}
