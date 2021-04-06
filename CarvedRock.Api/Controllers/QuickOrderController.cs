using System;
using CarvedRock.Api.ApiModels;
using CarvedRock.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarvedRock.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuickOrderController : ControllerBase
    {
        private readonly ILogger<QuickOrderController> _logger;
        private readonly IQuickOrderLogic _orderLogic;

        public QuickOrderController(ILogger<QuickOrderController> logger, IQuickOrderLogic orderLogic)
        {
            _logger = logger;
            _orderLogic = orderLogic;
        }

        [HttpPost]
        public Guid SubmitQuickOrder(QuickOrder orderInfo)
        {
            _logger.LogInformation($"Submitting order for {orderInfo.Quantity} of {orderInfo.ProductId}.");
            return _orderLogic.PlaceQuickOrder(orderInfo, 1234); // would get customer id from authN system/User claims
        }
    }
}
