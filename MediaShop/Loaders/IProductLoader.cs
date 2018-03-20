using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Models;

namespace MediaShop.Loaders
{
    internal interface IProductLoader
    {
        List<Product> LoadProducts();
        void SaveProducts(List<Product> products);
    }
}
