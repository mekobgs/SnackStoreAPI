using Microsoft.Extensions.DependencyInjection;
using SnackStore.Core.Interfaces;
using SnackStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackStore.Web.Handlers
{
    public class NetCoreEventContainer : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public NetCoreEventContainer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Dispatch<T>(T eventToDispatch) where T : IDomainEvent
        {
            if (eventToDispatch.GetType() == typeof(PriceUpdated))
            {
                var domainHandler = _serviceProvider.GetServices<IDomainHandler<PriceUpdated>>();
                foreach (var handler in domainHandler)
                {
                    await handler.Handle(eventToDispatch as PriceUpdated);
                }
            }
            if (eventToDispatch.GetType() == typeof(ProductBuyed))
            {
                var domainHandler = _serviceProvider.GetServices<IDomainHandler<ProductBuyed>>();
                foreach (var handler in domainHandler)
                {
                    await handler.Handle(eventToDispatch as ProductBuyed);
                }
            }

        }
    }
}
