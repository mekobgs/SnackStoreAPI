using SnackStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnackStore.Core.Seed
{
    public class SeedProduct
    {
        public static List<Product> CreateDumpData()
        {
            return new List<Product>
            {
                new Product()
                {
                    Name = "Muffins",
                    Stock = 15,
                    Price = 1.50,
                    Likes = 5
                },
                new Product()
                {
                    Name = "Cookies with Chocolate Chips",
                    Stock = 5,
                    Price = 0.75,
                    Likes = 3
                },
                new Product()
                {
                    Name = "Gelato",
                    Stock = 5,
                    Price = 2.25,
                    Likes = 1
                }
            };
        }
    }
}
