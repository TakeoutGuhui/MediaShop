using System.Windows;
using System.Windows.Input;

using MediaShop.Commands;
using MediaShop.Models;

namespace MediaShop.ViewModels
{
    class StockViewModel : BaseViewModel
    {
        public ProductList ProductList { get; set;  }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (value == _selectedProduct) return;
                _selectedProduct = value;
                RaisePropertyChangedEvent("SelectedProduct");
            }
        }

        public StockViewModel(ProductList productList)
        {
            ProductList = productList;
            SelectedProduct = productList.Products[0];
        }

        public ICommand AddProductCommand { get { return new DelegateCommand(AddProduct); } }

        private void AddProduct()
        {
            bool error = false;
            string errorMessage = "";
            if (ProductList.IsIdTaken(_selectedProduct.ID))
            {
                error = true;
                errorMessage += "The ID \"" + _selectedProduct.ID + "\" is already taken, please choose another one\n";
            } 
            if (ProductList.IsNameTaken(_selectedProduct.Name))
            {
                error = true;
                errorMessage += "The name \"" + _selectedProduct.Name + "\" is already taken, please choose another one\n";
            }
            if (error)
            {
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK);
                return;
            }
            ProductList.Products.Add(_selectedProduct);
        }

        public ICommand DeleteProductCommand { get { return new DelegateCommand(DeleteProduct);} }

        private void DeleteProduct()
        {
            if (_selectedProduct.InStock())
            {
                MessageBoxResult result = MessageBox.Show("There's still " + _selectedProduct.Stock + " products left in stock, do you want to remove it anyway?",
                    "Stock not empty", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ProductList.Products.Remove(_selectedProduct);
                    _selectedProduct = null;
                }
            }
            else
            {
                ProductList.Products.Remove(_selectedProduct);
            }
        }

        public ICommand NewProductCommand { get { return new DelegateCommand(NewProduct); } }
        private void NewProduct()
        {
            SelectedProduct = new Product();
        }

                
        
    }
}
