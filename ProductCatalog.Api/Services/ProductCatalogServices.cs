#region Using Namespaces

using System;
using System.Linq;
using System.Text;
using System.Net.Mime;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

using Newtonsoft.Json;
using ProductCatalog.Api.Models;

#endregion

namespace ProductCatalog.Api.Services
{
    public interface IProductCatalogServices
    {
        Task<List<Product>> GetSortedProductQueryAsync(string sortOption);
        Task<string> TrolleyCalculatorAsync(string httpContent);
    }

    public class ProductCatalogServices : IProductCatalogServices
    {
        #region Internal Members
        private const string _token                 = "25a4f06f-8fd5-49b3-a711-c013c156f8c8";
        private const string _productUrl            = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/products";
        private const string _trolleyCalcUrl        = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/trolleyCalculator";
        private const string _shopperHistoryUrl     = "http://dev-wooliesx-recruitment.azurewebsites.net/api/resource/shopperHistory";

        #endregion

        #region Public Methods

        public async Task<List<Product>> GetSortedProductQueryAsync(string sortOption)
        {
            List<Product> lstProducts = await GetProductListAsync();
            bool isSortOptionRecommended = !string.IsNullOrWhiteSpace(sortOption) && sortOption.Equals("Recommended", StringComparison.InvariantCultureIgnoreCase);

            if (isSortOptionRecommended)
            {
                lstProducts = await GetProductListByRecommendation(lstProducts);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(sortOption))
                {
                    lstProducts = true switch
                    {
                        bool when sortOption.Equals("Low", StringComparison.InvariantCultureIgnoreCase)         => lstProducts.OrderBy(product => product.Price).ToList(),
                        bool when sortOption.Equals("High", StringComparison.InvariantCultureIgnoreCase)        => lstProducts.OrderByDescending(product => product.Price).ToList(),
                        bool when sortOption.Equals("Ascending", StringComparison.InvariantCultureIgnoreCase)   => lstProducts.OrderBy(product => product.Name).ToList(),
                        bool when sortOption.Equals("Descending", StringComparison.InvariantCultureIgnoreCase)  => lstProducts.OrderByDescending(product => product.Name).ToList(),
                        _ => lstProducts,
                    };
                }
            }

            return lstProducts;
        }

        public async Task<string> TrolleyCalculatorAsync(string httpContent)
        {
            return await ExecPostHttpMethodAsync(_trolleyCalcUrl, _token, httpContent);
        }

        #endregion

        #region Internal Methods

        private async Task<string> ExecGetHttpMethodAsync(string url, string token)
        {
            string strResponse = default;
            
            try
            {
                using (HttpClient client = new ())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                    client.DefaultRequestHeaders.Add("User-Agent", ".NET ProductCatalogServices");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");

                    strResponse = await client.GetStringAsync($"{url}?token={token}");
                }
            }
            catch (Exception)
            {
                strResponse = string.Empty;
            }

            return strResponse;
        }

        private async Task<string> ExecPostHttpMethodAsync(string url, string token, string httpContent)
        {
            string strResponse = default;

            try
            {
                using (HttpClient client = new())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                    client.DefaultRequestHeaders.Add("User-Agent", ".NET ProductCatalogServices");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");

                    StringContent data              = new (httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
                    HttpResponseMessage response    = await client.PostAsync($"{url}?token={token}", data);
                    strResponse                     = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception)
            {
                strResponse = string.Empty;
            }

            return strResponse;
        }

        private async Task<List<Product>> GetProductListAsync()
        {
            List<Product> lstProducts;
            
            try
            {
                string strResponse  = await ExecGetHttpMethodAsync(_productUrl, _token);
                lstProducts         = JsonConvert.DeserializeObject<List<Product>>(strResponse);
            }
            catch (JsonSerializationException)
            {
                lstProducts = new List<Product>();
            }

            return lstProducts;
        }

        private async Task<List<Product>> GetProductListByRecommendation(IEnumerable<Product> products)
        {
            List<Product> lstProducts;

            try
            {
                string strResponse = await ExecGetHttpMethodAsync(_shopperHistoryUrl, _token);
                List<ShopperHistory> lstShopperHistory = JsonConvert.DeserializeObject<List<ShopperHistory>>(strResponse);

                IEnumerable<Product> productsOrderedBasedOnNumberOfOrders = from shoppingHistory in lstShopperHistory
                    let allOrders = shoppingHistory.Products
                    from order in allOrders
                    group order by order.Name into ordersGroupedByName
                    let productsAndNumberOfOrders = new
                    {
                        NumberOfOrders  = ordersGroupedByName.Sum(product => product.Quantity),
                        Product         = products.SingleOrDefault(product => product.Name == ordersGroupedByName.Key)
                    }
                    orderby productsAndNumberOfOrders.NumberOfOrders descending
                    select productsAndNumberOfOrders.Product;

                lstProducts = productsOrderedBasedOnNumberOfOrders.ToList();
                IEnumerable<Product> productsThatWereNotOrdered = products.Except(lstProducts);
                lstProducts.AddRange(productsThatWereNotOrdered);
            }
            catch (JsonSerializationException)
            {
                lstProducts = new List<Product>();
            }

            return lstProducts;
        }

        #endregion
    }
}