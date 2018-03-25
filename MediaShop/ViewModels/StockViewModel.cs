using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using MediaShop.Commands;
using MediaShop.Models;

namespace MediaShop.ViewModels
{
    class StockViewModel : BaseViewModel
    {
        public ProductListEditor ListEditor;

        public StockViewModel(ProductListEditor listEditor)
        {
            ListEditor = listEditor;
        }

        public ICommand AddProductCommand => new DelegateCommand(AddProduct);


        private void AddProduct()
        {

        }
                
        
    }
}
