using System.Windows.Controls;
using System.Windows.Input;
using MediaShop.Models;
using MediaShop.ViewModels;

namespace MediaShop.Views
{
    /// <summary>
    /// Interaction logic for StockView.xaml
    /// </summary>
    public partial class StockView
    {
        public StockView()
        {
            InitializeComponent();
            DataContext = new StockViewModel((ProductList.Instance));
        }
    }
}
