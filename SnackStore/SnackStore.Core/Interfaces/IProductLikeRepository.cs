using SnackStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnackStore.Core.Interfaces
{
    public interface IProductLikeRepository: IRepository<ProductLike>
    {
        void RemoveLikes(IEnumerable<ProductLike> Likes);
        void AddLikes(IEnumerable<ProductLike> Likes);
    }
}
