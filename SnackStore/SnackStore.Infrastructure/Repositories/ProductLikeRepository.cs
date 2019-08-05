using SnackStore.Core.Interfaces;
using SnackStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnackStore.Infrastructure.Repositories
{
    public class ProductLikeRepository : Repository<ProductLike>, IProductLikeRepository
    {
        public ProductLikeRepository(SnackStoreDbContext context, IEventDispatcher domainEventsDispatcher) : base(context, domainEventsDispatcher)
        {
        }

        public void RemoveLikes(IEnumerable<ProductLike> likesToRemove)
        {
            _dbSet.RemoveRange(likesToRemove);
        }

        public void AddLikes(IEnumerable<ProductLike> likesToAdd)
        {
            _dbSet.AddRange(likesToAdd);
        }
    }
}
