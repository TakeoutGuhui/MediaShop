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

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string name = ((TextBox) e.Source).Name;
            string content = ((TextBox) e.Source).Text;
            switch (name)
            {
                case "IdBox":
                    e.Handled = Product.ValidId(content); 
                    break;
                case "NameBox":
                    e.Handled = Product.ValidName(content);
                    break;
                case "PriceBox":
                    e.Handled = Product.ValidPrice(content);
                    break;
                case "StockBox":
                    e.Handled = Product.ValidStock(content);
                    break;
                case "ArtistBox":
                    e.Handled = Product.ValidArtist(content);
                    break;
                case "GenreBox":
                    e.Handled = Product.ValidGenre(content);
                    break;
                case "PublisherBox":
                    e.Handled = Product.ValidPublisher(content);
                    break;
                case "YearBox":
                    e.Handled = Product.ValidYear(content);
                    break;
                case "CommentBox":
                    e.Handled = Product.ValidComment(content);
                    break;

            }
        }
    }
}
