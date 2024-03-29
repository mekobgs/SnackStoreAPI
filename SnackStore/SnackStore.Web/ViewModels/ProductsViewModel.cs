﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackStore.Web.ViewModels
{
    public class ProductsViewModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Sorter SortBy { get; set; } = Sorter.Name;
        public Order Order { get; set; } = Order.Asc;
    }
    public enum Sorter
    {
        Name = 0,
        Likes = 1
    }

    public enum Order
    {
        Asc = 0,
        Desc = 1
    }
}
