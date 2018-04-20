using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Models
{
    class ProductSale
    {
        public int NumItems { get; set; }
        public decimal Price { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalPrice { get { return Price * NumItems; } } 
    }
}
