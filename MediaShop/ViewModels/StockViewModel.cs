using System.Windows.Input;

using MediaShop.Commands;
using MediaShop.Models;

namespace MediaShop.ViewModels
{
    class StockViewModel : BaseViewModel
    {
        public ProductList ProductList { get; set;  }

        public StockViewModel(ProductList productList)
        {
            ProductList = productList;
        }

        public ICommand AddProductCommand => new DelegateCommand(AddProduct);


        private void AddProduct()
        {

        }
                
        
    }
}
