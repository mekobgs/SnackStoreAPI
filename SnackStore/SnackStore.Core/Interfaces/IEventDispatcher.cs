using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnackStore.Core.Interfaces
{
    public interface IEventDispatcher
    {
        Task Dispatch<T>(T eventToDispatch) where T : IDomainEvent;
    }
}
