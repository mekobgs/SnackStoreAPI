using Microsoft.EntityFrameworkCore;
using SnackStore.Core.Helpers;
using SnackStore.Core.Interfaces;
using SnackStore.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackStore.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(SnackStoreDbContext context, IEventDispatcher domainEventsDispatcher) : base(context, domainEventsDispatcher)
        {
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await FindAll().OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllPaginated(Pagination pagination)
        {
            var property = TypeDescriptor.GetProperties(typeof(Product)).Find(pagination.Sort, true);
            var query = pagination.Order == "Desc"
                ? FindAll().OrderByDescending(a => property.GetValue(a))
                : FindAll().OrderBy(a => property.GetValue(a));
            return await query
                .Skip((pagination.Number - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await FindByExpression(p => p.Id == id).Include(a => a.Likes).FirstOrDefaultAsync();
        }

        public async Task<Product> GetByName(string name)
        {
            return await FindByExpression(p => p.Name == name).FirstOrDefaultAsync();
        }

        public async Task AddProduct(Product product)
        {
            Add(product);
            await SaveAsync();
        }

        public async Task RemoveProduct(Product product)
        {
            Remove(product);
            await SaveAsync();
        }

        public async Task EditProduct(Product product)
        {
            Edit(product);
            await SaveAsync();
        }
    }
}
