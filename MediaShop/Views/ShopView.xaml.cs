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
            ((ShopViewModel)DataContext).ProductViewSource.Filter += SearchBoxFilter;
        }

        private void SearchBoxChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ((ShopViewModel)DataContext).ProductViewSource.View.Refresh();
        }

        private void SearchBoxFilter(object sender, FilterEventArgs e)
        {
            e.Accepted = true;
            Product item = (Product) e.Item;
            if (item.ID.ToString().IndexOf(IdSearch.Text, StringComparison.CurrentCultureIgnoreCase) == -1) { e.Accepted = false; return; };
            if (item.Name != null && item.Name.IndexOf(NameSearch.Text, StringComparison.CurrentCultureIgnoreCase) == -1) { e.Accepted = false; return; };
            if (item.Price.ToString().IndexOf(PriceSearch.Text, StringComparison.CurrentCultureIgnoreCase) == -1) { e.Accepted = false; return; };
            if (item.Stock.ToString().IndexOf(StockSearch.Text, StringComparison.CurrentCultureIgnoreCase) == -1) { e.Accepted = false; return; };
            if (item.Artist != null && item.Artist.IndexOf(ArtistSearch.Text, StringComparison.CurrentCultureIgnoreCase) == -1) { e.Accepted = false; return; };
            if (item.Genre != null && item.Genre.IndexOf(GenreSearch.Text, StringComparison.CurrentCultureIgnoreCase) == -1) { e.Accepted = false; return; };
            if (item.Comment != null && item.Comment.IndexOf(CommentSearch.Text, StringComparison.CurrentCultureIgnoreCase) == -1) { e.Accepted = false; return; };
            if (item.Publisher != null && item.Publisher.IndexOf(PublisherSearch.Text, StringComparison.CurrentCultureIgnoreCase) == -1) { e.Accepted = false; return; };
            if (item.Year.ToString().IndexOf(YearSearch.Text, StringComparison.CurrentCultureIgnoreCase) == -1) { e.Accepted = false; return; };
        } 
    }
}
