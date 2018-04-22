using System.Windows.Controls;
using System.Windows.Input;
using MediaShop.Models;
using MediaShop.ViewModels;
using System.Text.RegularExpressions;

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

        private void YearAndStock_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
