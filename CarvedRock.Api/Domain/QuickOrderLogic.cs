using System;
using System.Threading.Tasks;
using CarvedRock.Api.ApiModels;
using CarvedRock.Api.Integrations;
using CarvedRock.Api.Interfaces;
using CarvedRock.Api.Repository;
using Microsoft.Extensions.Logging;

namespace CarvedRock.Api.Domain
{
    public class QuickOrderLogic : IQuickOrderLogic
    {
        private readonly ICarvedRockRepository _repo;
        private readonly IOrderProcessingNotification _orderProcessingNotification;
        private readonly ILogger<QuickOrderLogic> _logger;

        public QuickOrderLogic(ICarvedRockRepository repo, 
            IOrderProcessingNotification orderProcessingNotification, 
            ILogger<QuickOrderLogic> logger)
        {
            _repo = repo;
            _orderProcessingNotification = orderProcessingNotification;
            _logger = logger;
        }
        public async Task<Guid> PlaceQuickOrder(QuickOrder order, int customerId)
        {
            _logger.LogInformation("Placing order and sending update for inventory...");
            var orderId = Guid.NewGuid();
            // persist order to database or wherever
            await _repo.SubmitNewQuickOrder(order, customerId, orderId);

            // post "orderplaced" event to rabbitmq
            _orderProcessingNotification.QuickOrderReceived(order, customerId, orderId);

            return orderId;
        }
    }
}
