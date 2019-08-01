using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SnackStore.Core.Models
{
    public class PriceLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }       
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double PriceBefore { get; set; }
        public double PriceAfter { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string User { get; set; }
    }
}
