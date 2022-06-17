using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interface
{
    public interface IProductService
    {
        void SaveProducts(Product product);
        Task<IEnumerable<Product>> GetProducts();

        void EnableDisableProduct(int id);

        Task<bool> SaveChangesAsync();
    }
}