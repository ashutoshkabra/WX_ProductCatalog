#region  Using Namespaces

using System;
using System.Collections.Generic;

#endregion

namespace ProductCatalog.Api.ViewModels
{
    public class Special
    {
        public List<Quantity> quantities { get; set; }
        public int total { get; set; }
    }
}