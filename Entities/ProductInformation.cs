using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ProductInformation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }  

         [ForeignKey("Products")]
        public int ProductId { get; set; }
    }
}