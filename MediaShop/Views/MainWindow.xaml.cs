using MediaShop.Models;
using System.Diagnostics;
using System.Windows;
using MediaShop.Loaders;
using MediaShop.ViewModels;

namespace MediaShop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*
            ProductList productList = new ProductList(new ProductCsvLoader("../../Data/products.csv"));
            Product newProduct = new Product(5,"Nya",32.90m, 8);
            Product newProduct2 = new Product(6,"Hellblade", 39.90m, 34);
            productList.AddItem(newProduct);
            productList.AddItem(newProduct2);
            Debug.WriteLine(productList);
            productList.RemoveItem(newProduct2);
            Debug.WriteLine(productList);
            */
            //ShopViewModel viewModel = new ShopViewModel(new ProductList(new ProductCsvLoader("../../Data/products.csv")));
        }
    }
}
