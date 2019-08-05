using SnackStore.Core.Interfaces;
using SnackStore.Core.Models;
using SnackStore.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackStore.Web.Handlers
{
    public class ProductBuyedHandler : IDomainHandler<ProductBuyed>
    {
        private readonly IRepository<PurchaseLog> _purchaseLogRepository;
        private readonly ITokenFactory _tokenFactory;

        public ProductBuyedHandler(IRepository<PurchaseLog> purchaseLogRepository, ITokenFactory tokenFactory)
        {
            _purchaseLogRepository = purchaseLogRepository;
            _tokenFactory = tokenFactory;
        }

        public async Task Handle(ProductBuyed @event)
        {
            _purchaseLogRepository.Add(new PurchaseLog
            {
                ProductId = @event.Product.Id,
                ProductName = @event.Product.Name,
                Quantity = @event.Quantity,
                User = _tokenFactory.GetUser()
            });
        }
    }
}
