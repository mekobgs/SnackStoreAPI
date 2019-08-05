using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnackStore.Core.Helpers;
using SnackStore.Core.Interfaces;
using SnackStore.Core.Models;
using SnackStore.Web.Helpers;
using SnackStore.Web.ViewModels;

namespace SnackStore.Web.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenFactory _tokenFactory;

        public ProductController(IProductRepository productRepository, IAccountRepository accountRepository, ITokenFactory tokenFactory)
        {
            _productRepository = productRepository;
            _accountRepository = accountRepository;
            _tokenFactory = tokenFactory;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery]ProductsViewModel item = null)
        {
            item = item ?? new ProductsViewModel();
            var result = await _productRepository.GetAllPaginated(new Pagination
            {
                Number = item.PageNumber,
                PageSize = item.PageSize,
                Sort = item.SortBy.ToString(),
                Order = item.Order.ToString()
            });
            return Ok(result.Select(a => new ProductViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Stock = a.Stock,
                Likes = a.Likes,
                Price = a.Price
            }));
        }

        [HttpGet]
        [Route("product")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByName([FromQuery]string name)
        {
            var result = await _productRepository.GetByName(name);
            if (result == null)
                return NotFound($"Product with Name: {name} not found");
            return Ok(new ProductViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Stock = result.Stock,
                Likes = result.Likes,
                Price = result.Price
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody]AddProductViewModel item)
        {
            var product = await _productRepository.GetByName(item.Name);

            if (product != null)
                return Error($"Name is already in use: {item.Name}");

            if (string.IsNullOrEmpty(item.Name.Trim()) || item.Stock < 0 || item.Price < 0)
                return Error($"Invalid data.");
            product = new Product()
            {
                Name = item.Name,
                Stock = item.Stock,
                Price = item.Price
            };

            await _productRepository.AddProduct(product);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("stock")]
        public async Task<IActionResult> Stock([FromQuery]int id, int stock)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return ProductNotFound(id);

            if (stock < 0)
                return Error("Invalid data.");

            product.Stock += stock;

            await _productRepository.EditProduct(product);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("price")]
        public async Task<IActionResult> Price([FromQuery]int id, double price)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return ProductNotFound(id);

            if (price < 0)
                return Error("Invalid price.");
            var lastPrice = product.Price;
            product.Price = price;
            product.AddDomainEvent(new PriceUpdated() { Product = product, LastPrice = lastPrice });

            await _productRepository.EditProduct(product);
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return ProductNotFound(id);

            await _productRepository.RemoveProduct(product);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Guest")]
        [Route("{id}/like/toggle")]
        public async Task<IActionResult> ToggleLike(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
                return ProductNotFound(id);

            var userNameClaim = _tokenFactory.GetUser();
            if (userNameClaim == null)
                return Unauthorized();

            var account = await _accountRepository.FindByUserName(userNameClaim);
            if (account == null)
                return Error("Account not found.");

            var _account = product.ProductLikes.FirstOrDefault(a => a.AccountId == account.Id);
            if (_account != null)
            {
                product.Likes--;
                product.ProductLikes.Remove(_account);
            }
            else
            {
                product.Likes++;
                product.ProductLikes.Add(new ProductLike
                {
                    AccountId = account.Id,
                    ProductId = product.Id
                });
            }
            
            await _productRepository.EditProduct(product);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Guest")]
        [Route("{id}/buy")]
        public async Task<IActionResult> Buy(int id, [FromQuery]int quantity)
        {
            var product = await _productRepository.GetById(id);

            if (product == null)
                return ProductNotFound(id);

            if (quantity < 1)
                return Error("The quantity can't be less than zero");
            if (product.Stock < quantity)
                return Error("The quantity exceeds product's stock.");

            product.Stock -= quantity;
            product.AddDomainEvent(new ProductBuyed() { Product = product, Quantity = quantity });
            await _productRepository.EditProduct(product);
            return Ok();
        }

        private IActionResult ProductNotFound(int id)
        {
            return NotFound($"Product with Id: {id} not found.");
        }
    }
}