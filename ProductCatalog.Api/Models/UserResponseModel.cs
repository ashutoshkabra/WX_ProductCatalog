#region Using Namespaces

using System;

#endregion

namespace ProductCatalog.Api.Models
{
    public class UserResponseModel
    {
        public string Token { get; set; }

        public string Name { get; set; }
    }
}