#region Using Namespaces

using System;
using System.Collections.Generic;

using ProductCatalog.Api.Models;

#endregion

namespace ProductCatalog.Api.Tests.DataHelpers
{
    public static class ListOfProducts
    {
        public static readonly List<Product> ANotSortedProductsFormLowToHigh = new List<Product>
        {
            new Product {Name = "Test Product B", Price = 101.99, Quantity = 0},
            new Product {Name = "Test Product A", Price = 99.99, Quantity = 0},
            new Product {Name = "Test Product C", Price = 10.99, Quantity = 0},
            new Product {Name = "Test Product D", Price = 5, Quantity = 0},
            new Product {Name = "Test Product F", Price = 999999999999, Quantity = 0}
        };

        public static readonly List<Product> SortedProductsFormLowToHigh = new List<Product>
        {
            new Product {Name = "Test Product D", Price = 5, Quantity = 0},
            new Product {Name = "Test Product C", Price = 10.99, Quantity = 0},
            new Product {Name = "Test Product A", Price = 99.99, Quantity = 0},
            new Product {Name = "Test Product B", Price = 101.99, Quantity = 0},
            new Product {Name = "Test Product F", Price = 999999999999, Quantity = 0}
        };

        public static readonly List<Product> SortedProductsFormHighToLow = new List<Product>
        {
            new Product {Name = "Test Product F", Price = 999999999999, Quantity = 0},
            new Product {Name = "Test Product B", Price = 101.99, Quantity = 0},
            new Product {Name = "Test Product A", Price = 99.99, Quantity = 0},
            new Product {Name = "Test Product C", Price = 10.99, Quantity = 0},
            new Product {Name = "Test Product D", Price = 5, Quantity = 0}
        };

        public static readonly List<Product> SortedAscending = new List<Product>
        {
            new Product {Name = "Test Product A", Price = 99.99, Quantity = 0},
            new Product {Name = "Test Product B", Price = 101.99, Quantity = 0},
            new Product {Name = "Test Product C", Price = 10.99, Quantity = 0},
            new Product {Name = "Test Product D", Price = 5, Quantity = 0},
            new Product {Name = "Test Product F", Price = 999999999999, Quantity = 0}
        };

        public static readonly List<Product> SortedDescending = new List<Product>
        {
            new Product {Name = "Test Product F", Price = 999999999999, Quantity = 0},
            new Product {Name = "Test Product D", Price = 5, Quantity = 0},
            new Product {Name = "Test Product C", Price = 10.99, Quantity = 0},
            new Product {Name = "Test Product B", Price = 101.99, Quantity = 0},
            new Product {Name = "Test Product A", Price = 99.99, Quantity = 0}
        };

        public static readonly List<Product> SortedBasedOnRecommended = new List<Product>
        {
            new Product {Name = "Test Product A", Price = 99.99, Quantity = 0},
            new Product {Name = "Test Product B", Price = 101.99, Quantity = 0},
            new Product {Name = "Test Product F", Price = 999999999999, Quantity = 0},
            new Product {Name = "Test Product C", Price = 10.99, Quantity = 0},
            new Product {Name = "Test Product D", Price = 5, Quantity = 0}
        };
    }
}