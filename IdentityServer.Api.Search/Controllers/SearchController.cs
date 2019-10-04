using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer.Api.Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SearchController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44356");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return StatusCode(StatusCodes.Status500InternalServerError, disco.Error);
            }
            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "search-api",
                ClientSecret = "zbieram-pieczarki",
                Scope = "api-products api-search"
            });
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return StatusCode(StatusCodes.Status500InternalServerError, tokenResponse.ErrorDescription);
            }

            Console.WriteLine(tokenResponse.Json);


            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("https://localhost:44337/api/smartphones");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
                return Ok(JArray.Parse(content));
            }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
