﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackStore.Web.ViewModels
{
    public class AddProductViewModel
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
    }
}
