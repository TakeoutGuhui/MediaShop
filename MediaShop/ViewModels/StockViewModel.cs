using System.Windows;
using System.Windows.Input;

using MediaShop.Commands;
using MediaShop.Models;
using MediaShop.Views;
using MediaShop.Loaders;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;

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
            get { return _newProductMode; }
            set
            {
                if (value == _newProductMode) return;
                _newProductMode = value;
                RaisePropertyChangedEvent("NewProductMode");
            } 
        }

        /// <summary>
        /// The product that is currently selected in the list
        /// </summary>
        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                
                if (value == _selectedProduct) return;
                if (NewProductMode) // If you select a product in the list while a new product i being added the user is asked if he wants to cancel the adding of the new product
                {
                    MessageBoxResult result =  MessageBox.Show("Do you want to cancel adding the new product?", "Save product", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No) return;
                    else NewProductMode = false;
                }
                _selectedProduct = value;
                RaisePropertyChangedEvent("SelectedProduct");
            }
        }

        public StockViewModel(ProductList productList)
        {
            ProductList = productList;
            SelectedProduct = productList.Products[0];
            NewProductMode = false;
            
        }

        /// <summary>
        /// Command for adding a new product to the ProductList
        /// </summary>
        public ICommand AddProductCommand { get { return new DelegateCommand(AddProduct); } }
        private void AddProduct()
        {
            if (!NewProductMode) return; // If not in the NewProductMode, return
            bool error = false;
            string errorMessage = "";
            if (SelectedProduct.ID == "" || SelectedProduct.Name == "")
            {
                // Ugly last minute fix
                errorMessage += "The ID or Name can't be empty \n";
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK);
                return; 
            } 

            if (ProductList.IsIdTaken(SelectedProduct.ID)) // If the ID already is taken, ask the user to enter a different one
            {
                error = true;
                errorMessage += "The ID \"" + SelectedProduct.ID + "\" is already taken, please choose another one\n";
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
        public ICommand DeleteProductCommand { get { return new DelegateCommand(DeleteProduct);} }
        private void DeleteProduct()
        {
            if (NewProductMode)
            {
                SelectedProduct = new Product();
                return;
            }

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
        public ICommand NewProductCommand { get { return new DelegateCommand(NewProduct); } }
        private void NewProduct()
        {
            SelectedProduct = new Product(); // SelectedProduct is set to a new, empty one
            NewProductMode = true; // NewProductMode is turned on 
        }

        /// <summary>
        /// Command for adding stock to a product
        /// </summary>
        public ICommand AddStockCommand { get { return new DelegateCommand(AddStock); } }
        private void AddStock()
        {
            AddStockView addView = new AddStockView();
            addView.DataContext = this;
            if ((bool)addView.ShowDialog()) 
            {
                SelectedProduct.AddStock(StockToAdd);
                StockToAdd = 0;
            }
           
        }

        private const string _importPath = "../../../../MediaIntegrator/MediaIntegrator/bin/Debug/toMediaShop";

        public ICommand ImportCommand { get { return new DelegateCommand(Import); } }
        private void Import()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV File (.csv)|*.csv";
            dialog.DefaultExt = ".csv";
            dialog.InitialDirectory = Path.GetFullPath(_importPath);
            if (dialog.ShowDialog() == true)
            {

                ProductCsvLoader loader = new ProductCsvLoader(Path.GetFullPath(dialog.FileName));
                ICollection<Product> importedProducts = loader.LoadProducts();
                foreach (Product importedProduct in importedProducts)
                {
                    ProductList.AddProduct(importedProduct);
                }
            }
        }

        private const string _exportPath = "../../../../MediaIntegrator/MediaIntegrator/bin/Debug/fromMediaShop";

        public ICommand ExportCommand { get { return new DelegateCommand(Export); } }
        private void Export()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV File (.csv)|*.csv";
            dialog.DefaultExt = ".csv";
            dialog.FileName = "exported";
            dialog.InitialDirectory = Path.GetFullPath(_exportPath);
            if (dialog.ShowDialog() == true)
            {

                ProductCsvLoader loader = new ProductCsvLoader(Path.GetFullPath(dialog.FileName));
                loader.SaveProducts(ProductList.Products);
            }
        }
    }
}
