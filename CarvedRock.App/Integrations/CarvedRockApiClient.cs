using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CarvedRock.App.Models;
using Microsoft.Extensions.Configuration;

namespace CarvedRock.App.Integrations
{
    public class CarvedRockApiClient : ICarvedRockApiClient
    {
        private readonly HttpClient _client;

        public CarvedRockApiClient(HttpClient client, IConfiguration config)
        {
            _client = client;
            _client.BaseAddress = new Uri(config.GetValue<string>("CarvedRockApiUrl"));
        }

        public async Task<List<Product>> GetProducts(string category = null)
        {
            var requestUri = "products";
            if (!string.IsNullOrWhiteSpace(category))
            {
                requestUri += $"?category={category}";
            }

            //var products = new List<Product>
            //{
            //    new Product { Id = 1, Name = "Trailblazer", Category = "boots", Price = 69.99,
            //        Description = "Great support in this high-top to take you to great heights and trails." },
            //    new Product { Id = 2, Name = "Coastliner", Category = "boots", Price = 49.99,
            //        Description = "Easy in and out with this lightweight but rugged shoe with great ventilation to get your around shores, beaches, and boats." },
            //    new Product { Id = 3, Name = "Woodsman", Category = "boots", Price = 64.99,
            //        Description = "ALl the insulation and support you need when wandering the rugged trails of the woods and backcountry."},
            //    new Product { Id = 4, Name = "Billy", Category = "boots", Price = 79.99,
            //        Description = "Get up and down rocky terrain like a billy-goat with these awesome high-top boots with outstanding support." },
            //};
            //return products;

            return await _client.GetFromJsonAsync<List<Product>>(requestUri);
        }

        public async Task<Guid> PlaceQuickOrder(QuickOrder order)
        {
            var response = await _client.PostAsJsonAsync("quickorder", order);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Guid>();
            }

            var ex = new Exception("Failed placing QuickOrder.");
            ex.Data.Add("Product", order.ProductId);
            ex.Data.Add("Quantity", order.Quantity);
            ex.Data.Add("URI", response.RequestMessage?.RequestUri);
            ex.Data.Add("ResponseCode", (int)response.StatusCode);
            throw ex;
        }
    }
}
