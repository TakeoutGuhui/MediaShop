using MediaShop.Commands;
using MediaShop.Models;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace MediaShop.ViewModels
{
    class ShopViewModel : BaseViewModel
    {
        public ProductList ProductList { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public ICollectionView ProductView { get; set;}

        public Product SelectedProduct { get; set; }
        public CartItem SelectCartItem { get; set; }

        private bool _printReceipt;
        public bool PrintReceipt 
        { 
            get { return _printReceipt; }
            set
            {
                if(_printReceipt == value) return;
                _printReceipt = value;
                
            }
        }
        #region Filters

        private string _idFilter = "";
        public string IdFilter
        {
            get { return _idFilter;  }
            set
            {
                if (value != _idFilter)
                {
                    _idFilter = value;
                    ProductView.Refresh();
                }
            }
        }

        private string _nameFilter = "";
        public string NameFilter
        {
            get { return _nameFilter; }
            set
            {
                if (value != _nameFilter)
                {
                    _nameFilter = value;
                    ProductView.Refresh();
                }
            }
        }

        private string _priceFilter = "";
        public string PriceFilter
        {
            get { return _priceFilter; }
            set
            {
                if (value != _priceFilter)
                {
                    _priceFilter = value;
                    ProductView.Refresh();
                }
            }
        }

        private string _stockFilter = ""; 
        public string StockFilter
        {
            get { return _stockFilter; }
            set
            {
                if (value != _stockFilter)
                {
                    _stockFilter = value;
                    ProductView.Refresh();
                }
            }
        }

        private string _artistFilter = "";
        public string ArtistFilter
        {
            get { return _artistFilter; }
            set
            {
                if (value != _artistFilter)
                {
                    _artistFilter = value;
                    ProductView.Refresh();
                }
            }
        }

        private string _genreFilter = "";
        public string GenreFilter
        {
            get { return _genreFilter; }
            set
            {
                if (value != _genreFilter)
                {
                    _genreFilter = value;
                    ProductView.Refresh();
                }
            }
        }

        private string _commentFilter = "";
        public string CommentFilter
        {
            get { return _commentFilter; }
            set
            {
                if (value != _commentFilter)
                {
                    _commentFilter = value;
                    ProductView.Refresh();
                }
            }
        }

        private string _publisherFilter = "";
        public string PublisherFilter
        {
            get { return _publisherFilter; }
            set
            {
                if (value != _publisherFilter)
                {
                    _publisherFilter = value;
                    ProductView.Refresh();
                }
            }
        }

        private string _yearFilter = "";
        public string YearFilter
        {
            get { return _yearFilter; }
            set
            {
                if (value != _yearFilter)
                {
                    _yearFilter = value;
                    ProductView.Refresh();
                }
            }
        }

        private static bool PartOf(string filter, string text)
        {
            return text.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) != -1;
        }

        private bool FilterProducts(object obj)
        {
            Product item = (Product)obj;
            if (IdFilter != "" && !PartOf(IdFilter, item.ID.ToString())) { return false; };
            if (NameFilter != "" && !PartOf(NameFilter, item.Name)) { return false; };
            if (PriceFilter != "" && !PartOf(PriceFilter, item.Price.ToString(CultureInfo.CurrentCulture))) { return false; };
            if (StockFilter != "" && !PartOf(StockFilter, item.Stock.ToString())) { return false; };
            if (ArtistFilter != "" && !PartOf(ArtistFilter, item.Artist)) { return false; };
            if (GenreFilter != "" && !PartOf(GenreFilter, item.Genre)) { return false; };
            if (CommentFilter != "" && !PartOf(CommentFilter, item.Comment)) { return false; };
            if (PublisherFilter != "" && !PartOf(PublisherFilter, item.Publisher)) { return false; };
            if (YearFilter != "" && !PartOf(YearFilter, item.Year.ToString())) { return false; };
            return true;
        }

        #endregion

        public ShopViewModel(ProductList productList)
        {
            ProductList = productList;
            ShoppingCart = new ShoppingCart();
            ProductView = CollectionViewSource.GetDefaultView(ProductList.Products);
            ProductView.Filter = FilterProducts;
        }

        public ICommand AddToCartCommand { get { return new DelegateCommand(AddToCart); } }

        private void AddToCart()
        {
            if (SelectedProduct != null)
            {
                ShoppingCart.AddItem(SelectedProduct);
            }
        }

        public ICommand RemoveFromCartCommand { get { return new DelegateCommand(RemoveFromCart); } }

        private void RemoveFromCart()
        {
            if (SelectCartItem != null)
            {
                ShoppingCart.RemoveItem(SelectCartItem.Product, false);
            }
        }

        public ICommand ClearCartCommand { get { return new DelegateCommand(ClearCart); } }

        private void ClearCart()
        {
            ShoppingCart.RemoveAllItems();
        }

        public ICommand CheckoutCommand { get { return new DelegateCommand(Checkout); } }
        private void Checkout()
        {
            if (PrintReceipt)
            {
                new Printer().bla(ShoppingCart.ToString());
            }
            ShoppingCart.Checkout();
            ProductList.SaveProducts();
        }
    }    
}
