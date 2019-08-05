using SnackStore.Core.Interfaces;
using SnackStore.Core.Models;
using SnackStore.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackStore.Web.Handlers
{
    public class PriceUpdatedHandler : IDomainHandler<PriceUpdated>
    {
        private readonly IRepository<PriceLog> _priceUpdateLogRepository;
        private readonly ITokenFactory _tokenFactory;

        public PriceUpdatedHandler(IRepository<PriceLog> priceUpdateLogRepository, ITokenFactory tokenFactory)
        {
            _priceUpdateLogRepository = priceUpdateLogRepository;
            _tokenFactory = tokenFactory;
        }

        public async Task Handle(PriceUpdated @event)
        {
            _priceUpdateLogRepository.Add(new PriceLog
            {
                ProductId = @event.Product.Id,
                ProductName = @event.Product.Name,
                PriceBefore = @event.LastPrice,
                PriceAfter = @event.Product.Price,
                User = _tokenFactory.GetUser()
            });
        }
    }
}
