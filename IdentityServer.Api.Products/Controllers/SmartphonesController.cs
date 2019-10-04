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
        public ActionResult<IEnumerable<dynamic>> Get()
        {
            return new List<dynamic>()
            {
                new
                {
                    Id = 1,
                    Name = "IPhone XI Pro"
                },
                new
                {
                    Id = 2,
                    Name = "Samsung Galaxy S10"
                }

            };
        }
    }
}