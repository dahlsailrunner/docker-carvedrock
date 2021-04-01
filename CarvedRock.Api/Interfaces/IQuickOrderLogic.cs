using System;
using CarvedRock.Api.ApiModels;

namespace CarvedRock.Api.Interfaces
{
    public interface IQuickOrderLogic
    {
        Guid PlaceQuickOrder(QuickOrder order, int customerId);
    }
}
