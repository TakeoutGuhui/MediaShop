﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

using MediaShop.Loaders;

namespace MediaShop.Models
{
    internal class ProductList
    {
        private static ProductList _instance;

        //public static ProductList Instance => _instance ?? (_instance = new ProductList(new ProductCsvLoader("../../Data/products.csv")));
        public static ProductList Instance { 
            get 
            {
                if (_instance == null){
                    _instance = new ProductList(new ProductCsvLoader("../../Data/products.csv"));
                }
                return _instance;
            }
        }


        public ObservableCollection<Product> Products { get; set; }
        private readonly IProductLoader _productLoader;

        private ProductList(IProductLoader productLoader)
        {
            Products = new ObservableCollection<Product>();
            _productLoader = productLoader;
            Products = _productLoader.LoadProducts();
            Products.CollectionChanged += ProductAddRemove;
            foreach (Product product in Products)
            {
                product.PropertyChanged += ProductChangedEvent;
            }
        }

        private void ProductAddRemove(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if (eventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                Debug.WriteLine("New product!");
                if (eventArgs.NewItems[0] is Product) ((Product)eventArgs.NewItems[0]).PropertyChanged += ProductChangedEvent;
            }
            else if (eventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                Debug.WriteLine("Delete product!");
                if (eventArgs.OldItems[0] is Product)
                {
                    ((Product)eventArgs.OldItems[0]).PropertyChanged -= ProductChangedEvent;
                    SaveProducts();
                }
                    
            }
        }

        private void ProductChangedEvent(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            Debug.WriteLine("Saving!");
            SaveProducts();
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
            //stringBuilder.AppendLine($"{"ID",-4} {"Name",-15} {"Price",-10} {"Stock",-10}");
            stringBuilder.AppendLine(string.Format("{0,-4} {1,-15} {2,-10} {3,-10}", "ID", "Name", "Price", "Stock"));
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
