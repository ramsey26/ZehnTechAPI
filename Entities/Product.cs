using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public string Sku { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

       public ProductInformation ProductInformation { get; set; }
    }
}