using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CarvedRock.Api.ApiModels;
using Dapper;

namespace CarvedRock.Api.Repository
{
    public class CarvedRockRepository :ICarvedRockRepository
    {
        private readonly IDbConnection _db;

        public CarvedRockRepository(IDbConnection db)
        {
            _db = db;
        }
        public async Task<List<Product>> GetProducts(string category)
        {
            return (await _db.QueryAsync<Product>(
                "SELECT * FROM dbo.Product WHERE Category = COALESCE(@category, Category) OR @category = 'all' ",
                new {category})).ToList();
        }

        public async Task SubmitNewQuickOrder(QuickOrder order, int customerId, Guid orderId)
        {
            await _db.ExecuteScalarAsync<int>(
                "INSERT INTO dbo.[Order] (OrderId, CustomerId, ProductId, QuantityOrdered, OrderTotal) VALUES " +
                                          "(@OrderId, @CustomerId, @ProductId, @Quantity, @Total)", new
                {
                    orderId,
                    customerId,
                    order.ProductId,
                    order.Quantity,
                    Total = 1.00  // TODO: retrieve price and multiply by quantity
                });
        }
    }
}
