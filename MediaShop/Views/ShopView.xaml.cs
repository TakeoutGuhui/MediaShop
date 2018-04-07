using MediaShop.Loaders;
using MediaShop.Models;
using MediaShop.ViewModels;
using System;
using System.Windows.Data;

namespace MediaShop.Views
{
    /// <summary>
    /// Interaction logic for ShopView.xaml
    /// </summary>
    public partial class ShopView
    {
        public ShopView()
        {
            InitializeComponent();
            DataContext = new ShopViewModel(ProductList.Instance);
        }
    }
}
