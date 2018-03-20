using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Models
{
    internal class Product
    {
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product(int productNumber, string name, decimal price, int stock)
        {
            ProductNumber = productNumber;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public bool InStock()
        {
            return Stock > 0 ? true : false;
        }

        public override string ToString()
        {
            return $"Id: {ProductNumber} Name: {Name} Price: {Price} Stock: {Stock}";
        }
    }
}
