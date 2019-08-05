using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SnackStore.Core.Models;
using SnackStore.Core.Seed;
using SnackStore.Infrastructure;

namespace SnackStore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var serviceScope = host.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SnackStoreDbContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Account.AddRange(SeedAccount.SeedData());
                context.Product.AddRange(SeedProduct.SeedData());
                context.ProductLikes.AddRange(SeedProductLikes.SeedData());
                context.SaveChanges();
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
