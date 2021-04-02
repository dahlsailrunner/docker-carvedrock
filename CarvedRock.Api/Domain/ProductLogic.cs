using System;
using System.Collections.Generic;
using System.Linq;
using CarvedRock.Api.ApiModels;
using CarvedRock.Api.Interfaces;
using Microsoft.Extensions.Logging;

namespace CarvedRock.Api.Domain
{
    public class ProductLogic : IProductLogic
    {
        private readonly ILogger<ProductLogic> _logger;
        private readonly List<string> _validCategories = new List<string> {"all", "boots", "climbing gear", "kayaks"};


        public ProductLogic(ILogger<ProductLogic> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Product> GetProductsForCategory(string category)
        {
            //_logger.LogInformation("Starting logic to get products", category);

            if (!_validCategories.Any(c => string.Equals(category, c, StringComparison.InvariantCultureIgnoreCase)))
            {
                // invalid category -- bad request
                throw new ApplicationException($"Unrecognized category: {category}.  " +
                         $"Valid categories are: [{string.Join(",", _validCategories)}]");
            }

            if (string.Equals(category, "kayaks", StringComparison.InvariantCultureIgnoreCase))
            {
                // simulate database error or real technical error like not implemented exception
                throw new Exception("Not implemented! No kayaks have been defined in 'database' yet!!!!");
            }

            return GetAllProducts().Where(a =>
                string.Equals("all", category, StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(category, a.Category, StringComparison.InvariantCultureIgnoreCase));
        }

        private static IEnumerable<Product> GetAllProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Trailblazer", Category = "boots", Price = 69.99,
                    Description = "Great support in this high-top to take you to great heights and trails." },
                new Product { Id = 2, Name = "Coastliner", Category = "boots", Price = 49.99,
                    Description = "Easy in and out with this lightweight but rugged shoe with great ventilation to get your around shores, beaches, and boats." },
                new Product { Id = 3, Name = "Woodsman", Category = "boots", Price = 64.99,
                    Description = "ALl the insulation and support you need when wandering the rugged trails of the woods and backcountry."},
                new Product { Id = 4, Name = "Billy", Category = "boots", Price = 79.99,
                    Description = "Get up and down rocky terrain like a billy-goat with these awesome high-top boots with outstanding support." },
            };
        }
    }
}
