using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SnackStore.Core.Models
{
    public class ProductLike: Entity
    {
        [Key]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Key]
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
