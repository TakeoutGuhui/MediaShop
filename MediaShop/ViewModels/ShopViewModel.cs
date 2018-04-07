using System.Diagnostics;
using System.Windows.Input;

using MediaShop.Commands;
using MediaShop.Models;
using System.Windows.Data;
using System;
using System.ComponentModel;

namespace MediaShop.ViewModels
{
    class ShopViewModel : BaseViewModel
    {
        public ProductList ProductList { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public ICollectionView ProductView { get; set;}

        public Product SelectedProduct { get; set; }
        public CartItem SelectCartItem { get; set; }


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

        private bool FilterProducts(object obj)
        {
            Product item = (Product)obj;
            if (item.ID.ToString().IndexOf(IdFilter, StringComparison.CurrentCultureIgnoreCase) == -1) { return false; };
            if (item.Name != null && item.Name.IndexOf(NameFilter, StringComparison.CurrentCultureIgnoreCase) == -1) { return false; };
            if (item.Price.ToString().IndexOf(PriceFilter, StringComparison.CurrentCultureIgnoreCase) == -1) { return false; };
            if (item.Stock.ToString().IndexOf(StockFilter, StringComparison.CurrentCultureIgnoreCase) == -1) { return false; };
            if (item.Artist != null && item.Artist.IndexOf(ArtistFilter, StringComparison.CurrentCultureIgnoreCase) == -1) { return false; };
            if (item.Genre != null && item.Genre.IndexOf(GenreFilter, StringComparison.CurrentCultureIgnoreCase) == -1) { return false; };
            if (item.Comment != null && item.Comment.IndexOf(CommentFilter, StringComparison.CurrentCultureIgnoreCase) == -1) { return false; };
            if (item.Publisher != null && item.Publisher.IndexOf(PublisherFilter, StringComparison.CurrentCultureIgnoreCase) == -1) { return false; };
            if (item.Year.ToString().IndexOf(YearFilter, StringComparison.CurrentCultureIgnoreCase) == -1) { return false; };
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

        public ICommand CheckoutCommand { get { return new DelegateCommand(Checkout); } }
        private void Checkout()
        {
            ShoppingCart.Checkout();
            ProductList.SaveProducts();
        }
    }
    
}
