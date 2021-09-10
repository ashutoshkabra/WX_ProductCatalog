#region Using Namespaces

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ProductCatalog.Api.Models;

#endregion

namespace ProductCatalog.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region AllowAnonymous Methods

        [HttpGet]
        public async Task<IActionResult> FindUserAsync()
        {
            return await Task.Run(() => Ok(new UserResponseModel
            {
                Name    = "John Smith",
                Token   = "25a4f06f-8fd5-49b3-a711-c013c156f8c8"
            }));
        }

        #endregion
    }
}