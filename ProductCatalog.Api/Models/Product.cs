#region  Using Namespaces

using System;
using System.Collections.Generic;

using ProductCatalog.Api.Extensions;

#endregion

namespace ProductCatalog.Api.Models
{
    public class Product : ValueObject
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Price;
            yield return Quantity;
        }
    }
}