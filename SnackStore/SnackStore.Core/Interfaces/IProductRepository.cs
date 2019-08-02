using SnackStore.Core.Helpers;
using SnackStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnackStore.Core.Interfaces
{
    public interface IProductRepository: IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetAllPaginated(Pagination pagination);
        Task<Product> GetById(int id);
        Task<Product> GetByName(string name);
        Task EditProduct(Product product);
        Task AddProduct(Product product);
        Task RemoveProduct(Product product);
    }
}
