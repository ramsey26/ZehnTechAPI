using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void EnableDisableProduct(int id)
        {
            var prod = _dataContext.Products.Where(x => x.Id == id).FirstOrDefault();

            prod.IsActive = !prod.IsActive;
            _dataContext.Entry(prod).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dataContext.Products
                        .Where(x => x.IsActive == true)
                        .Include(x => x.ProductInformation)
                        .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public void SaveProducts(Product product)
        {
            product.IsActive = true;
            _dataContext.Products.Add(product);

        }
    }
}