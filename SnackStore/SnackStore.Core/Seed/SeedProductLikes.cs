using SnackStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnackStore.Core.Seed
{
    public class SeedProductLikes
    {
        public static List<ProductLike> SeedData()
        {
            return new List<ProductLike>
            {
                new ProductLike()
                {
                    ProductId = 1,
                    AccountId = 1
                },
                new ProductLike()
                {
                    ProductId = 2,
                    AccountId = 1
                },
                new ProductLike()
                {
                    ProductId = 3,
                    AccountId = 1
                },
                new ProductLike()
                {
                    ProductId = 1,
                    AccountId = 2
                },
            };
        }
    }
}
