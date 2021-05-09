using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace CarvedRock.OrderProcessor.Repository
{
    public class InventoryRepository :IInventoryRepository
    {
        private readonly IDbConnection _db;

        public InventoryRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> GetInventoryForProduct(int productId)
        {
            return await _db.ExecuteScalarAsync<int>(
                "SELECT QuantityOnHand FROM dbo.Inventory WHERE ProductId = @ProductId",
                new {productId});
        }

        public async Task UpdateInventoryForProduct(int productId, int newInventory)
        {
            await _db.ExecuteAsync(
                "UPDATE dbo.Inventory SET QuantityOnHand = @newInventory WHERE ProductId = @productId",
                new {productId, newInventory});
        }
    }
}
