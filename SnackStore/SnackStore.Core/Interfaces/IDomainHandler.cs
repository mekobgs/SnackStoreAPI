using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnackStore.Core.Interfaces
{
    public interface IDomainHandler<T> where T : IDomainEvent
    {
        Task Handle(T @event);
    }
}
