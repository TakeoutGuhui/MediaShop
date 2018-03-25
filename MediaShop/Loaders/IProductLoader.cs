using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Models;

namespace MediaShop.Loaders
{
    internal interface IProductLoader
    {
        ObservableCollection<Product> LoadProducts();
        void SaveProducts(ObservableCollection<Product> products);
    }
}
