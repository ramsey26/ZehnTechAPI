using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
   // [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
       
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get-products")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productService.GetProducts();
        }

         [HttpPost("save-products")]
        public async Task<ActionResult> SaveProducts(Product product)
        {
            _productService.SaveProducts(product);

            if( await _productService.SaveChangesAsync()) return Ok();
            return BadRequest("Failed to save product");
        }

         [HttpPost("{id}")]
        public async Task<ActionResult> EnableOrDisableProducts(int id)
        {
            _productService.EnableDisableProduct(id);

            if( await _productService.SaveChangesAsync()) return Ok();
            return BadRequest("Failed to update product");
        }
    }
}