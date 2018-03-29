﻿using MediaShop.Loaders;
using MediaShop.Models;
using MediaShop.ViewModels;

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
