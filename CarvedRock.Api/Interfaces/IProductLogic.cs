using System.Collections.Generic;
using System.Threading.Tasks;
using CarvedRock.Api.ApiModels;

namespace CarvedRock.Api.Interfaces
{
    public interface IProductLogic
    {
        Task<IEnumerable<Product>> GetProductsForCategory(string category);
    }
}
