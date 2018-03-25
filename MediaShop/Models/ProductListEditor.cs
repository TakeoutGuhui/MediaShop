using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Loaders;

namespace MediaShop.Models
{
    class ProductListEditor
    {
        public ProductList ProductList { get; set; }

        public ProductListEditor(ProductList productList)
        {
            ProductList = productList;
        }

        public void AddProduct(Product product)
        {
            ProductList.Products.Add(product);
            ProductList.SaveProducts();
        }

        public void RemoveProduct(Product product)
        {
            ProductList.Products.Remove(product);
            ProductList.SaveProducts();
        }
    }
}
