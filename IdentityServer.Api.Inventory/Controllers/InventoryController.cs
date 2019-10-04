using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IdentityServer.Api.Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<dynamic>> Get()
        {
            return new List<dynamic>()
            {
                new
                {
                    Id = 1,
                    Qty = 23
                },
                new
                {
                    Id = 2,
                    Qty = 13
                }

            };
        }
    }
}
