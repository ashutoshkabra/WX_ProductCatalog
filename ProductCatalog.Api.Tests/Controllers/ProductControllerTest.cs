#region Using Namespaces

using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

using Moq;
using Xunit;
using Newtonsoft.Json;
using ProductCatalog.Api.Services;
using ProductCatalog.Api.Controllers;

using ProductCatalog.Api.ViewModels;
using ProductCatalog.Api.Tests.DataHelpers;
using Product = ProductCatalog.Api.Models.Product;

#endregion

namespace ProductCatalog.Api.Tests.Controllers
{
    public class ProductControllerTest
    {
        #region Internal Members

        private ProductCatalogServices _productCatalogServices;
        private Mock<IProductCatalogServices> _mockProductCatalogServices;

        #endregion

        #region Constructors

        public ProductControllerTest()
        {
            _productCatalogServices     = new ProductCatalogServices();
            _mockProductCatalogServices = new Mock<IProductCatalogServices>();
        }

        #endregion

        #region Tests

        [Fact]
        public async Task Sort_SortedProductListIsNotEmpty_ReturnOKStatusCode()
        {
            // Arrange
            List<Product> lstSortedProducts = ListOfProducts.SortedAscending;
            _mockProductCatalogServices.Setup(gspq => gspq.GetSortedProductQueryAsync(It.IsAny<string>())).ReturnsAsync(lstSortedProducts);

            // Act
            IActionResult actionResult = await new ProductController(_mockProductCatalogServices.Object).Sort("High");

            // Assert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
        }

        [Fact]
        public async Task Sort_SortedProductListIsEmpty_ReturnNotFoundStatusCode()
        {
            // Arrange
            List<Product> lstSortedProducts = new List<Product>();
            _mockProductCatalogServices.Setup(gspq => gspq.GetSortedProductQueryAsync(It.IsAny<string>())).ReturnsAsync(lstSortedProducts);

            // Act
            IActionResult actionResult = await new ProductController(_mockProductCatalogServices.Object).Sort("High");

            // Assert
            NotFoundObjectResult objectResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }

        [Fact]
        public async Task Sort_SortedProductListIsNull_ReturnNotFoundStatusCode()
        {
            // Arrange
            List<Product> lstSortedProducts = null;
            _mockProductCatalogServices.Setup(gspq => gspq.GetSortedProductQueryAsync(It.IsAny<string>())).ReturnsAsync(lstSortedProducts);

            // Act
            IActionResult actionResult = await new ProductController(_mockProductCatalogServices.Object).Sort("High");

            // Assert
            NotFoundObjectResult objectResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.NotFound, objectResult.StatusCode);
        }

        [Fact]
        public async Task Sort_SortByLow_ReturnSortedListFromLowToHigh()
        {
            // Arrange 

            // Act
            IActionResult actionResult  = await new ProductController(_productCatalogServices).Sort("Low");

            // Assert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            List<Product> actualResult  = Assert.IsType<List<Product>>(objectResult.Value);
            Assert.Equal(ListOfProducts.SortedProductsFormLowToHigh, actualResult);
        }

        [Fact]
        public async Task Sort_SortByHigh_ReturnSortedListFromHighToLow()
        {
            // Arrange 

            // Act
            IActionResult actionResult  = await new ProductController(_productCatalogServices).Sort("High");

            // Assert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            List<Product> actualResult  = Assert.IsType<List<Product>>(objectResult.Value);
            Assert.Equal(ListOfProducts.SortedProductsFormHighToLow, actualResult);
        }

        [Fact]
        public async Task Sort_SortByAscending_ReturnSortedListFromAscendingToDescending()
        {
            // Arrange 

            // Act
            IActionResult actionResult = await new ProductController(_productCatalogServices).Sort("Ascending");

            // Assert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            List<Product> actualResult = Assert.IsType<List<Product>>(objectResult.Value);
            Assert.Equal(ListOfProducts.SortedAscending, actualResult);
        }

        [Fact]
        public async Task Sort_SortByDescending_ReturnSortedListFromDescendingToAscending()
        {
            // Arrange 

            // Act
            IActionResult actionResult = await new ProductController(_productCatalogServices).Sort("Descending");

            // Assert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            List<Product> actualResult = Assert.IsType<List<Product>>(objectResult.Value);
            Assert.Equal(ListOfProducts.SortedDescending, actualResult);
        }

        [Fact]
        public async Task Sort_SortByRecommended_ReturnSortedListByPopularity()
        {
            // Arrange 

            // Act
            IActionResult actionResult = await new ProductController(_productCatalogServices).Sort("Recommended");

            // Assert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            List<Product> actualResult = Assert.IsType<List<Product>>(objectResult.Value);
            Assert.Equal(ListOfProducts.SortedBasedOnRecommended, actualResult);
        }

        [Fact]
        public async Task Sort_SortByDefault_ReturnSortedListByAscending()
        {
            // Arrange 

            // Act
            IActionResult actionResult = await new ProductController(_productCatalogServices).Sort("RandomText");

            // Assert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            List<Product> actualResult = Assert.IsType<List<Product>>(objectResult.Value);
            Assert.Equal(ListOfProducts.SortedAscending, actualResult);
        }

        [Fact]
        public async Task Sort_SortByNull_ReturnSortedListByAscending()
        {
            // Arrange 

            // Act
            IActionResult actionResult = await new ProductController(_productCatalogServices).Sort(null);

            // Assert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            List<Product> actualResult = Assert.IsType<List<Product>>(objectResult.Value);
            Assert.Equal(ListOfProducts.SortedAscending, actualResult);
        }

        [Fact]
        public async Task TrolleyCalculator_ExecTrolleyCalculator_ReturnsValidResult()
        {
            // Arrange
            string requestContent   = "{\"products\": [{\"name\": \"test\",\"price\": 100.0}],\"specials\": [{\"quantities\": [{\"name\": \"test\",\"quantity\": 2}],\"total\":150}],\"quantities\": [{\"name\": \"test\",\"quantity\": 2}]}";
            TrolleyCart trolleyCart = JsonConvert.DeserializeObject<TrolleyCart>(requestContent);
            
            // Act
            IActionResult actionResult  = await new ProductController(_productCatalogServices).TrolleyCalculator(trolleyCart);

            // Assert
            OkObjectResult objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            string actualResult = Assert.IsType<string>(objectResult.Value);
            Assert.Equal("150.0", actualResult);
        }

        #endregion
    }
}