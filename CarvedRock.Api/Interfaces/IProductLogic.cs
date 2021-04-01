using System.Collections.Generic;
using CarvedRock.Api.ApiModels;

namespace CarvedRock.Api.Interfaces
{
    public interface IProductLogic
    {
        IEnumerable<Product> GetProductsForCategory(string category);
    }
}
