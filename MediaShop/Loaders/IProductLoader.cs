using System.Collections.ObjectModel;

using MediaShop.Models;

namespace MediaShop.Loaders
{
    internal interface IProductLoader
    {
        ObservableCollection<Product> LoadProducts();
        void SaveProducts(ObservableCollection<Product> products);
    }
}
