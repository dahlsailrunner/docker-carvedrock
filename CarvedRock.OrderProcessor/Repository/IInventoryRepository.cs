using System.Threading.Tasks;

namespace CarvedRock.OrderProcessor.Repository
{
    public interface IInventoryRepository
    {
        Task<int> GetInventoryForProduct(int productId);
        Task UpdateInventoryForProduct(int productId, int newInventory);
    }
}
