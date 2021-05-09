using System;
using CarvedRock.Api.ApiModels;

namespace CarvedRock.Api.Integrations
{
    public interface IOrderProcessingNotification
    {
        void QuickOrderReceived(QuickOrder order, int customerId, Guid orderId);
    }
}
