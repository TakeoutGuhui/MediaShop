using MediaShop.ViewModels;
using System;

namespace MediaShop.Models
{   
    /// <summary>
    /// Class that represents an item in the cart
    /// </summary>
    class CartItem : BaseViewModel
    {
        /// <summary>
        /// The product that the item represents
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Number of items of the product in the cart
        /// </summary>
        private uint _numItemsInCart;
        public uint NumItemsInCart
        {
            get => _numItemsInCart;
            private set
            {
                if (value == _numItemsInCart) return;
                _numItemsInCart = value;
                TotalPrice = GetTotalPrice(); // The total price is updated
                RaisePropertyChangedEvent("NumItemsInCart");
            }
        }

        /// <summary>
        /// The total price of the items
        /// </summary>
        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                if (value == _totalPrice) return;
                _totalPrice = value;
                RaisePropertyChangedEvent("TotalPrice");
            }
        }

        /// <summary>
        /// Constructor for this class
        /// </summary>
        /// <param name="product"> The product that the cart item should represent </param>
        public CartItem(Product product)
        {
            Product = product;
            NumItemsInCart = 1; // When a product is first added to the cart the number is 1
        }

        /// <summary>
        /// Adds another item of the product to the cart item
        /// </summary>
        public void AddAnother()
        {
            if (Product.InStock())
            {
                NumItemsInCart += 1;
            }
        }

        /// <summary>
        /// Removes a item of the product from the cart
        /// </summary>
        public void RemoveOne()
        {
            if (NumItemsInCart > 1)
            {
                NumItemsInCart -= 1;
            }
        }

        /// <summary>
        /// Checks if the product is in stock
        /// </summary>
        /// <returns> Returns True if the product is still in stock </returns>
        public bool InStock()
        {
            return NumItemsInCart < Product.Stock;
        }

        /// <summary>
        /// Checks out the cart item. The number of items is substracted from the products stock and a sale is added to the products sales
        /// </summary>
        public void Checkout()
        {
            Product.Stock -= NumItemsInCart;
            Product.ProductSales.AddSale(new ProductSale { NumItems = NumItemsInCart, Price = Product.Price, SaleDate = DateTime.Now });
        }

        /// <summary>
        /// The total price of the items 
        /// </summary>
        /// <returns> The price </returns>
        public decimal GetTotalPrice()
        {
            return Product.Price * NumItemsInCart;
        }

        public override string ToString()
        {
            string products = NumItemsInCart + "st*" + Product.Price;
            return $"{Product.Name,-15} {products,-13} {Product.Price * NumItemsInCart,10}";
        }
    }
}
