using System;
using System.Collections.Generic;
using System.Text;

namespace SnackStore.Core.Helpers
{
    public class Pagination
    {
        public int Number { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; }
        public string Order { get; set; }
    }
}
