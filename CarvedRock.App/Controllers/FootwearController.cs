using System.Threading.Tasks;
using CarvedRock.App.Integrations;
using CarvedRock.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarvedRock.App.Controllers
{
    public class FootwearController : Controller
    {
        private readonly ICarvedRockApiClient _apiClient;

        public FootwearController(ICarvedRockApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _apiClient.GetProducts();

            return View(products);
        }

        public async Task<IActionResult> QuickOrder(int id)
        {
            var orderId = await _apiClient.PlaceQuickOrder(new QuickOrder {ProductId = id, Quantity = 1});

            return View("QuickOrderConfirmation", orderId);
        }
    }
}
