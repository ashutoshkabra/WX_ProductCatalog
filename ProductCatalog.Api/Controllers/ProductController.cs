#region Using Namespaces

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Newtonsoft.Json;
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Services;
using ProductCatalog.Api.ViewModels;

#endregion

namespace ProductCatalog.Api.Controllers
{
    [Route("/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Internal Members

        private IProductCatalogServices _productCatalogServices;

        #endregion

        #region Constructors

        public ProductController(IProductCatalogServices productCatalogServices)
        {
            _productCatalogServices = productCatalogServices;
        }

        #endregion

        #region AllowAnonymous Methods

        // GET: /products?sortOption=High
        [HttpGet()]
        public async Task<IActionResult> Sort([FromQuery] string sortOption)
        {
            List<Models.Product> lstSortedProducts = await _productCatalogServices.GetSortedProductQueryAsync(sortOption);
            
            if(lstSortedProducts == null || lstSortedProducts.Count == 0)
                return NotFound("Unable to get product catalog. Try again later.");
            else
                return Ok(lstSortedProducts);

        }

        // POST: /trolleytotal
        [HttpPost("/trolleyTotal")]
        public async Task<IActionResult> TrolleyCalculator([FromBody] TrolleyCart strOption)
        {
            // Convert TrolleyCart object to JSON String
            string httpContent  = JsonConvert.SerializeObject(strOption);
            string strResponse  = await _productCatalogServices.TrolleyCalculatorAsync(httpContent);
            return await Task.Run(() => Ok(strResponse));
        }

        #endregion
    }
}