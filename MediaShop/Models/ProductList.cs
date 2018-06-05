using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

using MediaShop.Loaders;
using System.Collections.Generic;

namespace MediaShop.Models
{
    /// <summary>
    /// Class that represents a list of products
    /// </summary>
    internal class ProductList
    {
        /// <summary>
        /// There should only be one list of products in the program so I made it a Singleton
        /// </summary>
        private static ProductList _instance;
        public static ProductList Instance => _instance ?? (_instance = new ProductList(new ProductCsvLoader(Properties.Settings.Default.ProductFile)));

        /// <summary>
        /// The taken ID's
        /// </summary>
        private readonly HashSet<string> _takenIDs = new HashSet<string>();

        /// <summary>
        /// The taken names
        /// </summary>
        private readonly HashSet<string> _takenNames = new HashSet<string>();

        /// <summary>
        /// Checks if the parameter id is already taken
        /// </summary>
        /// <param name="id"> the id </param>
        /// <returns> Returns True if the id is already taken </returns>
        public bool IsIdTaken(string id) => _takenIDs.Contains(id);
        

        /// <summary>
        /// Checks if the name is already taken
        /// </summary>
        /// <param name="name"> the name </param>
        /// <returns> Returns True if the name is already taken </returns>
        public bool IsNameTaken(string name) => _takenNames.Contains(name);
        
        public ObservableCollection<Product> Products { get; set; }

        /// <summary>
        /// Used to load the products from source
        /// </summary>
        private readonly IProductLoader _productLoader;

        private ProductList(IProductLoader productLoader)
        {
            Products = new ObservableCollection<Product>();
            _productLoader = productLoader;
            //Products.CollectionChanged += ProductAddRemove;
        }

        public void LoadProducts()
        {
            ObservableCollection<Product> loadedProducts = _productLoader.LoadProducts();

            foreach (Product product in loadedProducts)
            {
                AddProduct(product);
            }
        }

        /// <summary>
        /// Adds a product to the list
        /// </summary>
        /// <param name="product"> The product that should be added </param>
        public void AddProduct(Product product)
        {
            if (IsIdTaken(product.Id) || IsNameTaken(product.Name)) return; // If the ID or name is already cancel the add
            if (product.Id == string.Empty || product.Name == "") return;
            product.ProductSales = new ProductSales(product.Id, product.Name); // Set the sales of the product
            product.PropertyChanged += ProductChangedEvent; // Adds ProductChangedEvent to the PropertyChanged event of the product (so the product is saved when changed)
            _takenIDs.Add(product.Id); // Adds the product's ID to the taken ID's
            _takenNames.Add(product.Name); // Adds the product's name to the taken names
            Products.Add(product); // Adds the product to the Product list
            SaveProducts();
        }

        /// <summary>
        /// Removes a product from the list
        /// </summary>
        /// <param name="product"> The product that should be removed </param>
        public void RemoveProduct(Product product)
        {
            product.ProductSales.DeleteSales(); // Delete the sales of the product
            product.PropertyChanged -= ProductChangedEvent; // Removes the ProductChangedEvent from the products PropertyChanged event
            _takenIDs.Remove(product.Id); // Removes the ID from the taken IDs
            _takenNames.Remove(product.Name); // Removes the name from the taken names
            Products.Remove(product); // The product is removed from the Product list
            SaveProducts();
        }

        /// <summary>
        /// Removes all the products
        /// </summary>
        public void RemoveAllProducts()
        {
            List<Product> prod = new List<Product>();
            for(int i = 0; i < Products.Count; i++)
            {
                prod.Add(Products[i]);
            }

            foreach (Product product in prod)
            {
                RemoveProduct(product);
            }
            SaveProducts();
        }

        /// <summary>
        /// Listens to all the product's PropertyChanged event so that they will be automatically saved when changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ProductChangedEvent(object sender, PropertyChangedEventArgs eventArgs){  SaveProducts(); }

        /// <summary>
        /// Saves the products to disk
        /// </summary>
        public void SaveProducts() => _productLoader.SaveProducts(Products);
        
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
