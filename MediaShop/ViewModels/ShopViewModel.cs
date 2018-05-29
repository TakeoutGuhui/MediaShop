using MediaShop.Commands;
using MediaShop.Models;
using MediaShop.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Linq;

namespace MediaShop.ViewModels
{
    /// <summary>
    /// View Model for the Shopview
    /// </summary>
    class ShopViewModel : BaseViewModel
    {
        /// <summary>
        /// The list of products in the store
        /// </summary>
        public ProductList ProductList { get; set; }
        
        /// <summary>
        /// The shopping cart
        /// </summary>
        public ShoppingCart ShoppingCart { get; set; }

        /// <summary>
        /// The products in the store (used to be able to search the products)
        /// </summary>
        public ICollectionView ProductView { get; set;}

        /// <summary>
        /// The product that is selected in the list
        /// </summary>
        public Product SelectedProduct { get; set; }

        /// <summary>
        /// The item that is selected in the shopping cart
        /// </summary>
        public CartItem SelectCartItem { get; set; }


        /// <summary>
        /// Bool that binds to the checkbox in the shopping cart. Indicates if the receipt should be printed when checking out the cart
        /// </summary>
        private bool _printReceipt;
        public bool PrintReceipt 
        { 
            get => _printReceipt;
            set
            {
                if(_printReceipt == value) return;
                _printReceipt = value;
                
            }
        }

        #region Filters

        /// <summary>
        /// Used to filter the product list. The search boxes in the view i bound to these and everytime the text changes in one of them the ProductView is refreshed
        /// </summary>

        private string _idFilter = "";
        public string IdFilter
        {
            get => _idFilter;
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
            get => _nameFilter;
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
            get => _priceFilter;
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
            get => _stockFilter;
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
            get => _artistFilter;
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
            get => _genreFilter;
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
            get => _commentFilter;
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
            get => _publisherFilter;
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
            get => _yearFilter;
            set
            {
                if (value != _yearFilter)
                {
                    _yearFilter = value;
                    ProductView.Refresh();
                }
            }
        }

        /// <summary>
        /// Checks if filter is part of text in some way. Not case sensitive
        /// </summary>
        /// <param name="filter"> The filter </param>
        /// <param name="text"> The text </param>
        /// <returns> True if filter is part of text, else false</returns>
        private static bool PartOf(string filter, string text)
        {
            return text.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) != -1;
        }

        /// <summary>
        /// Filters the products in the product list based on the strings above. Is called everytime one of the strings above is changed
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool FilterProducts(object obj)
        {
            Product item = (Product)obj;
            if (IdFilter != "" && !PartOf(IdFilter, item.Id.ToString())) { return false; };
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

        /// <summary>
        /// Constructor for this class. 
        /// </summary>
        /// <param name="productList"> The productlist that this class should use</param>
        public ShopViewModel(ProductList productList)
        {
            ProductList = productList;
            ShoppingCart = new ShoppingCart();
            ProductView = CollectionViewSource.GetDefaultView(ProductList.Products);
            ProductView.Filter = FilterProducts;
           
            
        }

        /// <summary>
        /// Method for adding the currently selected product to the cart
        /// </summary>
        public ICommand AddToCartCommand => new DelegateCommand(AddToCart);

        private void AddToCart()
        {
            if (SelectedProduct != null)
            {
                ShoppingCart.AddItem(SelectedProduct);
            }
        }

        /// <summary>
        /// Method to removing the currently selected cart item from the cart
        /// </summary>
        public ICommand RemoveFromCartCommand => new DelegateCommand(RemoveFromCart);

        private void RemoveFromCart()
        {
            if (SelectCartItem != null)
            {
                ShoppingCart.RemoveItem(SelectCartItem.Product, false);
            }
        }


        /// <summary>
        /// Command for clearing the shopping cart
        /// </summary>
        public ICommand ClearCartCommand => new DelegateCommand(ClearCart);

        private void ClearCart()
        {
            ShoppingCart.RemoveAllItems();
        }


        /// <summary>
        /// Command for checking out the cart
        /// </summary>
        public ICommand CheckoutCommand => new DelegateCommand(Checkout);

        private void Checkout()
        {
            if (ShoppingCart.Empty()) return;
            if (PrintReceipt)
            {
                new Printer().Print(ShoppingCart.ToString());
            }
            

            ShoppingCart.Checkout();
            ProductList.SaveProducts();
        }

        /// <summary>
        /// Command for making a return on the product that is currently selected
        /// </summary>
        public ICommand ReturnProductCommand => new DelegateCommand(ReturnProduct);

        private void ReturnProduct()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Please select product that you want to return and try again", "Not selected");
            }
            else
            {
                SelectedProduct.Stock++;
                MessageBox.Show("1 item of the product \"" + SelectedProduct.Name + "\" has been returned");
            }
        }

        /// <summary>
        /// Shows the info for the currently selected product
        /// </summary>
        public ICommand ShowInfoCommand => new DelegateCommand(ShowInfo);

        private void ShowInfo()
        {
            if (SelectedProduct != null) // If a product is selected
            {
                ProductInfo productInfo = new ProductInfo();
                productInfo.DataContext = SelectedProduct.ProductSales;
                productInfo.Show();
            }

        }

        /// <summary>
        /// Command that shows the top 10 list
        /// </summary>
        public ICommand ShowTopCommand => new DelegateCommand(ShowTop);

        private void ShowTop()
        {
            List<ProductSales> productSales = new List<ProductSales>();
            foreach (Product product in ProductList.Products)
            {
                productSales.Add(product.ProductSales); // All product's productsales are added to the list
            }
            var topTenList = productSales.OrderByDescending(s => s.AllTime.ItemsSold).ToList() // The list is ordered by most items sold
                .Take(10) // Extracts the 10 most sold
                .Where(s => s.AllTime.ItemsSold > 0); // If there are products with 0 sales they are removed
            TopTenWindow window = new TopTenWindow(); 
            window.DataContext = topTenList; // The DataContext is set to the top 10 list
            window.Show(); // The windows is set to appear


        }

        
    }    
}
