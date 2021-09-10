#region Using Namespaces

using System;
using System.Collections.Generic;

#endregion

namespace ProductCatalog.Api.ViewModels
{
    public class TrolleyCart
    {
        public List<Product> products { get; set; }
        public List<Special> specials { get; set; }
        public List<Quantity> quantities { get; set; }

    }
}