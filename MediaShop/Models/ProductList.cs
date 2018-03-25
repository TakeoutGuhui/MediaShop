using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MediaShop.Loaders;

namespace MediaShop.Models
{
    internal class ProductList
    {
        public ObservableCollection<Product> Products { get; set; }
        private readonly IProductLoader _productLoader;

        public ProductList(IProductLoader productLoader)
        {
            Products = new ObservableCollection<Product>();
            _productLoader = productLoader;
            Products = _productLoader.LoadProducts();
        }

        public void SaveProducts()
        {
            _productLoader.SaveProducts(Products);
        }

        public void LoadProducts()
        {
            Products = _productLoader.LoadProducts();
        }

        public override string ToString()
        {   
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("----------------------------------------");
            stringBuilder.AppendLine("Product list");
            stringBuilder.AppendLine("----------------------------------------");
            stringBuilder.AppendLine($"{"ID",-4} {"Name",-15} {"Price",-10} {"Stock",-10}");
            stringBuilder.AppendLine("----------------------------------------");
            foreach (var product in Products)
            {
                stringBuilder.Append(product).AppendLine();
            }
            stringBuilder.AppendLine("----------------------------------------");
            return stringBuilder.ToString();
        }
    }
}
