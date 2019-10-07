using IdentityModel.Client;
using IdentityServer.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer.Api.Search.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SearchController : ControllerBase
    {

        private readonly HttpClient _httpClient;

        private IEnumerable<Product> _products;
        private IEnumerable<Stock> _stocks;

        public SearchController()
        {
            _httpClient = new HttpClient();
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            await SecureClient(_httpClient);

            Task.WaitAll(GetProducts(), GetStock());

            return BuildDto();

        }

        private async Task SecureClient(HttpClient httpClient)
        {
            // discover endpoints from metadata
            var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:44356");
            if (disco.IsError)
                throw new Exception(disco.Error);

            // request token
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "search-api",
                ClientSecret = "zbieram-pieczarki",
                Scope = "api-products api-inventory"
            });
            if (tokenResponse.IsError)
                throw new Exception(tokenResponse.ErrorDescription);

            httpClient.SetBearerToken(tokenResponse.AccessToken);
        }

        private async Task GetProducts()
        {
            var response = await _httpClient.GetAsync("https://localhost:44337/api/smartphones");
            if (!response.IsSuccessStatusCode)
                _products = null;

            var content = await response.Content.ReadAsStringAsync();
            _products = JsonConvert.DeserializeObject<List<Product>>(content);
        }

        private async Task GetStock()
        {
            var response = await _httpClient.GetAsync("https://localhost:44376/api/inventory");
            if (!response.IsSuccessStatusCode)
                _stocks = null;

            var content = await response.Content.ReadAsStringAsync();
            _stocks = JsonConvert.DeserializeObject<List<Stock>>(content);
        }

        private IActionResult BuildDto()
        {
            var productInventory = new List<ProductInventory>();
            foreach (var product in _products)
            {
                var quantity = _stocks.Single(x => x.ProductId == product.Id).Quantity;
                var inventory = new ProductInventory
                {
                    Id = product.Id,
                    Name = product.Name,
                    Quantity = quantity
                };
                productInventory.Add(inventory);
            }

            return Ok(productInventory);
        }
    }
}
