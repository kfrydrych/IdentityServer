using IdentityServer.Shared;
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
        public ActionResult<IEnumerable<Stock>> Get()
        {
            var user = User; // No subjectId

            return new List<Stock>
            {
                new Stock
                {
                    ProductId = 1,
                    Quantity = 23
                },
                new Stock
                {
                    ProductId = 2,
                    Quantity = 13
                }

            };
        }
    }
}
