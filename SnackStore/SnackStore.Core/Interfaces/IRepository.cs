using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SnackStore.Core.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByExpression(Expression<Func<T, bool>> expression);
        void Add(T obj);
        void Edit(T obj);
        void Remove(T obj);
        Task SaveAsync();
        void Save();
    }
}
