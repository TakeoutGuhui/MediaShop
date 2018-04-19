using MediaShop.Models;
using MediaShop.ViewModels;
using System.Windows.Controls;

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
            /*
            ProductGrid.AutoGeneratingColumn += 
                new System.EventHandler<DataGridAutoGeneratingColumnEventArgs>(delegate (object obj, DataGridAutoGeneratingColumnEventArgs args){ 
                    if(args.PropertyName == "ProductSales"){
                        args.Cancel = true;
                    }
                });
            */
        }
    }
}

