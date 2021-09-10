#region MyRegion

using System;
using System.Collections.Generic;

using ProductCatalog.Api.Extensions;

#endregion

namespace ProductCatalog.Api.Models
{
    public class ShopperHistory : ValueObject
    {
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CustomerId;
            yield return Products;
        }
    }
}