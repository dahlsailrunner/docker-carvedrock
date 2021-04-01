using System.Collections.Generic;
using CarvedRock.Api.ApiModels;
using CarvedRock.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarvedRock.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductLogic _productLogic;

        public ProductsController(ILogger<ProductsController> logger, IProductLogic productLogic)
        {
            _logger = logger;
            _productLogic = productLogic;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts(string category = "all")
        {
            _logger.LogInformation("Starting the get products API call...");

            return _productLogic.GetProductsForCategory(category);
        }
    }
}
