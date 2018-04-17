using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

using MediaShop.Loaders;
using System.Collections.Generic;

namespace MediaShop.Models
{
    internal class ProductList
    {
        private static ProductList _instance;
        public static ProductList Instance { 
            get 
            {
                if (_instance == null){
                    _instance = new ProductList(new ProductCsvLoader(Properties.Settings.Default.productFile));
                }
                return _instance;
            }
        }

        private readonly HashSet<string> TakenIDs = new HashSet<string>();
        private readonly HashSet<string> TakenNames = new HashSet<string>();

        public bool IsIdTaken(string id)
        {
            return TakenIDs.Contains(id);
        }

        public bool IsNameTaken(string name)
        {
            return TakenNames.Contains(name);
        } 

        public ObservableCollection<Product> Products { get; set; }
        private readonly IProductLoader _productLoader;

        private ProductList(IProductLoader productLoader)
        {
            Products = new ObservableCollection<Product>();
            _productLoader = productLoader;
            Products.CollectionChanged += ProductAddRemove;
            ObservableCollection<Product> loadedProducts = _productLoader.LoadProducts();
            
            foreach (Product product in loadedProducts)
            {
                product.PropertyChanged += ProductChangedEvent;
                Products.Add(product);
            }
        }

        private void ProductAddRemove(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if (eventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                Product newProduct = (Product)eventArgs.NewItems[0];
                newProduct.PropertyChanged += ProductChangedEvent;
                TakenIDs.Add(newProduct.ID);
                TakenNames.Add(newProduct.Name);
            }
            else if (eventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                Debug.WriteLine("Delete product!");
                Product deletedProduct = (Product)eventArgs.OldItems[0];
                deletedProduct.PropertyChanged -= ProductChangedEvent;
                TakenIDs.Remove(deletedProduct.ID);
                TakenNames.Remove(deletedProduct.Name);                 
            }
        }

        private void ProductChangedEvent(object sender, PropertyChangedEventArgs eventArgs)
        {
            Debug.WriteLine("Saving!");
            Product changedProduct = (Product)sender;
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
