using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarvedRock.Api.ApiModels;

namespace CarvedRock.Api.Repository
{
    public interface ICarvedRockRepository
    {
        Task<List<Product>> GetProducts(string category);
        Task SubmitNewQuickOrder(QuickOrder order, int customerId, Guid orderId);
    }
}
