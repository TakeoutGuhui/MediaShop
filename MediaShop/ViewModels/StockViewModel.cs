﻿using System.Windows;
using System.Windows.Input;

using MediaShop.Commands;
using MediaShop.Models;
using MediaShop.Views;
using MediaShop.Loaders;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace MediaShop.ViewModels
{
    /// <summary>
    /// View Model for the Stock view
    /// </summary>
    class StockViewModel : BaseViewModel
    {
        /// <summary>
        /// The list of products
        /// </summary>
        public ProductList ProductList { get; set;  }

        public uint StockToAdd { get; set; }

        /// <summary>
        /// A boolean which indicates if a new product i currently being added
        /// </summary>
        private bool _newProductMode;
        public bool NewProductMode 
        {
            get => _newProductMode;
            set
            {
                if (value == _newProductMode) return;
                _newProductMode = value;
                RaisePropertyChangedEvent("NewProductMode");
            } 
        }

        private bool _updateProducts;
        public bool UpdateProducts
        {
            get => _updateProducts;
            set
            {
                if (value == _updateProducts) return;
                _updateProducts = value;
                RaisePropertyChangedEvent("UpdateProducts");
            }
        }

        /// <summary>
        /// The product that is currently selected in the list
        /// </summary>
        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                
                if (value == _selectedProduct) return;
                if (NewProductMode) // If you select a product in the list while a new product i being added the user is asked if he wants to cancel the adding of the new product
                {
                    MessageBoxResult result =  MessageBox.Show("Do you want to cancel adding the new product?", "Save product", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No) return;
                    NewProductMode = false;
                }
                _selectedProduct = value;
                RaisePropertyChangedEvent("SelectedProduct");
            }
        }

        public StockViewModel(ProductList productList)
        {
            ProductList = productList;
            if(productList.Products.Count != 0) SelectedProduct = productList.Products[0];
            NewProductMode = false;
        }

        /// <summary>
        /// Command for adding a new product to the ProductList
        /// </summary>
        public ICommand AddProductCommand => new DelegateCommand(AddProduct);

        private void AddProduct()
        {
            if (!NewProductMode) return; // If not in the NewProductMode, return
            bool error = false;
            string errorMessage = "";
            if (SelectedProduct.Id == "" || SelectedProduct.Name == "")
            {
                // Ugly last minute fix
                errorMessage += "The ID or Name can't be empty \n";
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK);
                return; 
            } 

            if (ProductList.IsIdTaken(SelectedProduct.Id)) // If the ID already is taken, ask the user to enter a different one
            {
                error = true;
                errorMessage += "The ID \"" + SelectedProduct.Id + "\" is already taken, please choose another one\n";
            } 
            if (ProductList.IsNameTaken(SelectedProduct.Name)) // If the name already is taken, ask the user to enter a different one
            {
                error = true;
                errorMessage += "The name \"" + SelectedProduct.Name + "\" is already taken, please choose another one\n";
            }
            if (error) // If error, show message and cancel the adding so the user can correct the problem
            {
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK);
                return;
            }
            ProductList.AddProduct(SelectedProduct);
            NewProductMode = false; // Turn off NewProductMode
        }

        /// <summary>
        /// Command for deleting a product from the ProductList
        /// </summary>
        public ICommand DeleteProductCommand => new DelegateCommand(DeleteProduct);

        private void DeleteProduct()
        {
            if (NewProductMode)
            {
                SelectedProduct = new Product();
                return;
            }
            if(SelectedProduct == null) return;

            if (_selectedProduct.InStock()) // If the product still is in stock, ask if the user really wants to remove it
            {
                MessageBoxResult result = MessageBox.Show("There's still " + _selectedProduct.Stock + " products left in stock, do you want to remove it anyway?",
                    "Stock not empty", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) // If he chose yes, remove it
                {
                    ProductList.RemoveProduct(_selectedProduct);
                    _selectedProduct = null;
                }
            }
            else // If not in stock, remove it
            {
                ProductList.RemoveProduct(_selectedProduct);
            }
        }

        /// <summary>
        /// Command for making a new product
        /// </summary>
        public ICommand NewProductCommand => new DelegateCommand(NewProduct);

        private void NewProduct()
        {
            SelectedProduct = new Product(); // SelectedProduct is set to a new, empty one
            NewProductMode = true; // NewProductMode is turned on 
        }

        /// <summary>
        /// Command for adding stock to a product
        /// </summary>
        public ICommand AddStockCommand => new DelegateCommand(AddStock);

        private void AddStock()
        {
            AddStockView addView = new AddStockView {DataContext = this};
            if ((bool)addView.ShowDialog()) 
            {
                SelectedProduct.AddStock(StockToAdd);
                StockToAdd = 0;
            }
           
        }

        public ICommand ImportCommand => new DelegateCommand(Import);

        private void Import()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "CSV File (.csv)|*.csv", // Only CSV files can be opened
                DefaultExt = ".csv",
                InitialDirectory = Path.GetFullPath(Properties.Settings.Default.DefaultImportFolder) // Set the default path to mediashops import directory in MediaIntegrator
            };
            if (dialog.ShowDialog() == true)
            {
                ProductCsvLoader loader = new ProductCsvLoader(Path.GetFullPath(dialog.FileName));
                ICollection<Product> importedProducts = loader.LoadProducts(); 
                foreach (Product importedProduct in importedProducts)
                {
                    // If the ID and Name is already taken we assume that the imported product already exists in the shop.
                    // And if the Update box is checked we update the product with the properties of the imported product
                    if ( UpdateProducts && ProductList.IsIdTaken(importedProduct.Id) && ProductList.IsNameTaken(importedProduct.Name))
                    {
                        Product product = ProductList.Products.First(p => p.Id == importedProduct.Id);
                        product.Update(importedProduct);
                    }
                    else
                    {
                        ProductList.AddProduct(importedProduct); // The AddProduct function in ProductList will not add the product if the name or ID already is taken.
                    }
                    
                }
                
            }
        }

        public ICommand ExportCommand => new DelegateCommand(Export);

        private void Export()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV File (.csv)|*.csv"; 
            dialog.DefaultExt = ".csv";
            dialog.FileName = "exported";
            dialog.InitialDirectory = Path.GetFullPath(Properties.Settings.Default.DefaultExportFolder);
            if (dialog.ShowDialog() != true) return;
            ProductCsvLoader loader = new ProductCsvLoader(Path.GetFullPath(dialog.FileName));
            loader.SaveProducts(ProductList.Products);
        }
    }
}
