using SnackStore.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnackStore.Core.Models
{
    public class PriceUpdated: IDomainEvent
    {
        public Product Product { get; set; }
        public double LastPrice { get; set; }
        public string User { get; set; }
    }
}
