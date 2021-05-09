using System.Collections.Generic;
using System.Threading.Tasks;
using CarvedRock.Api.ApiModels;
using CarvedRock.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CarvedRock.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductLogic _productLogic;

        public ProductsController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts(string category = "all")
        {
            //Log.Information("Starting controller action GetProducts for {category}", category);
            Log.ForContext("Category", category)
               .Information("Starting controller action GetProducts");

            return await _productLogic.GetProductsForCategory(category);
        }
    }
}