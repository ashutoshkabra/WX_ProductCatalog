#region Using Namespaces

using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Xunit;
using ProductCatalog.Api.Models;
using ProductCatalog.Api.Controllers;

#endregion

namespace ProductCatalog.Api.Tests.Controllers
{
    public class UserControllerTest
    {
        #region Tests

        [Fact]
        public async Task FindUser_RequestIsValid_ReturnStatusOkWithUser()
        {
            // Act
            IActionResult actionResult      = await new UserController().FindUserAsync();

            // Assert
            OkObjectResult objectResult     = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            UserResponseModel actualResult  = Assert.IsType<UserResponseModel>(objectResult.Value);

            Assert.Equal("John Smith", actualResult.Name);
            Assert.Equal("25a4f06f-8fd5-49b3-a711-c013c156f8c8", actualResult.Token);
        }

        #endregion
    }
}