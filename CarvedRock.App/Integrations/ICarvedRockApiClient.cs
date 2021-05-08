using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarvedRock.App.Models;

namespace CarvedRock.App.Integrations
{
    public interface ICarvedRockApiClient
    {
        Task<List<Product>> GetProducts(string category = null);
        Task<Guid> PlaceQuickOrder(QuickOrder order);
    }
}
