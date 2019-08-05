using System;
using System.Collections.Generic;
using System.Text;

namespace SnackStore.Core.Models
{
    public class ProductBuyed
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string User { get; set; }
    }
}
