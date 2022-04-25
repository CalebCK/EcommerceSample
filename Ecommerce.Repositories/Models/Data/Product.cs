using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Models.Data
{
    public class ProductResponse
    {
        public List<Product> Products { get; set; }
    }
    public class Product
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string[] Sizes { get; set; }
        public string Description { get; set; }
    }
}
