using System.Diagnostics;
using MediaShop.ViewModels;

namespace MediaShop.Models
{
    class CartItem : BaseViewModel
    {
        public Product Product { get; set; }

        private int _numItemsInCart;
        public int NumItemsInCart
        {
            get => _numItemsInCart;
            private set
            {
                if (value != _numItemsInCart)
                {
                    _numItemsInCart = value;
                    TotalPrice = GetTotalPrice();
                    RaisePropertyChangedEvent("NumItemsInCart");
                }
            }
        }

        private decimal _totalPrice;
        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                if (value != _totalPrice)
                {
                    _totalPrice = value;
                    RaisePropertyChangedEvent("TotalPrice");
                }
            }
        }

        public CartItem(Product product)
        {
            Product = product;
            NumItemsInCart = 1;
            Product.Stock -= 1;
        }

        public void AddAnother()
        {
            if (Product.InStock())
            {
                NumItemsInCart += 1;
                Product.Stock -= 1;
            }
        }

        public void RemoveOne()
        {
            if (NumItemsInCart > 1)
            {
                NumItemsInCart -= 1;
                Product.Stock += 1;
                Debug.WriteLine("Removed one of the item \"" + Product.Name + "\" from the cart, " + NumItemsInCart + " left");
            }
        }

        public void Checkout()
        {
            Product.Stock -= NumItemsInCart;
        }

        public void RestoreStock()
        {
            Product.Stock += NumItemsInCart;
        }

        public decimal GetTotalPrice()
        {
            return Product.Price * NumItemsInCart;
        }

        public override string ToString()
        {
            string products = NumItemsInCart + "st*" + Product.Price;
            return $"{Product.Name,-15} {products, -13} {Product.Price * NumItemsInCart,10}";
        }
    }
}
