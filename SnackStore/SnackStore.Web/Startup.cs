using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SnackStore.Core.Interfaces;
using SnackStore.Core.Models;
using SnackStore.Infrastructure;
using SnackStore.Infrastructure.Repositories;
using SnackStore.Web.Handlers;
using SnackStore.Web.Helpers;
using Swashbuckle.AspNetCore.Swagger;

namespace SnackStore.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDatabase(services);
            services.AddScoped<IRepository<PriceLog>, Repository<PriceLog>>();
            services.AddScoped<IRepository<PurchaseLog>, Repository<PurchaseLog>>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductLikeRepository, ProductLikeRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IDomainHandler<PriceUpdated>, PriceUpdatedHandler>();
            services.AddScoped<IDomainHandler<ProductBuyed>, ProductBuyedHandler>();
            services.AddScoped<IEventDispatcher, NetCoreEventContainer>();
            services.AddHttpContextAccessor();
            services.AddSingleton<ITokenFactory, JwtFactory>();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SnackStoreAPI - ApplaudoStudios", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header {token}",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
                c.DescribeAllEnumsAsStrings();
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var signingKey = Convert.FromBase64String(Configuration["Jwt:SigningSecret"]);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(signingKey)
                    };
                });
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("StoreConnection");
            services.AddDbContext<SnackStoreDbContext>(opt => opt.UseSqlServer(connectionString));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SnackStore API - ApplaudoStudio");
            });
        }
    }
}
