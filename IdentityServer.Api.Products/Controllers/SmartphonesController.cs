using IdentityServer.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IdentityServer.Api.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SmartphonesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var user = User; // No subjectId

            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "IPhone XI Pro"
                },
                new Product
                {
                    Id = 2,
                    Name = "Samsung Galaxy S10"
                }

            };
        }
    }
}