using System.Threading.Tasks;
using CarvedRock.App.Integrations;
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

        public IActionResult QuickOrder(int id)
        {
            return View("QuickOrderConfirmation");
        }
    }
}
