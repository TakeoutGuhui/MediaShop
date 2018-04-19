using System.Collections.ObjectModel;

using MediaShop.Models;

namespace MediaShop.Loaders
{
    /// <summary>
    /// Made an interface to make it easier to change fileformat if it is needed in the future
    /// </summary>
    internal interface IProductLoader
    {
        ObservableCollection<Product> LoadProducts();
        void SaveProducts(ObservableCollection<Product> products);
    }
}
