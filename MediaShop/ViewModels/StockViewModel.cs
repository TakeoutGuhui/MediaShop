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
        }

        public ICommand AddProductCommand { get { return new DelegateCommand(AddProduct); } }

        private void AddProduct()
        {
            ProductList.Products.Add(_selectedProduct);
        }

        public ICommand DeleteProductCommand { get { return new DelegateCommand(DeleteProduct);} }

        private void DeleteProduct()
        {
            if (_selectedProduct.InStock())
            {
                MessageBoxResult result = MessageBox.Show("There's still products left in stock, do you want to remove it anyways", "Bla", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ProductList.Products.Remove(_selectedProduct);
                }
            }
        }

        public ICommand NewProductCommand { get { return new DelegateCommand(NewProduct); } }
        private void NewProduct()
        {
            SelectedProduct = new Product();
        }

                
        
    }
}
