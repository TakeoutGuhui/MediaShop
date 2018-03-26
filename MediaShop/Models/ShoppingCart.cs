using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using MediaShop.ViewModels;

namespace MediaShop.Models
{
    class ShoppingCart : BaseViewModel
    {
        private ObservableCollection<CartItem> _cartItems;

        public ObservableCollection<CartItem> CartItems
        {
            get => _cartItems;
            set
            {
                if(value == _cartItems) return;
                _cartItems = value;
                RaisePropertyChangedEvent("CartItems");
            }
        }

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

        public ShoppingCart()
        {
            CartItems = new ObservableCollection<CartItem>();
        }

        public CartItem AlreadyInCart(Product product)
        {
            foreach (CartItem cartItem in CartItems)
            {
                if (cartItem.Product.Equals(product)) return cartItem;
            }

            return null;
        }

        public void Checkout()
        {
            foreach (CartItem cartProduct in CartItems)
            {
                cartProduct.Checkout();
            }
            CartItems = new ObservableCollection<CartItem>();
            TotalPrice = GetTotalPrice();
            Debug.WriteLine("Cart was checked out");
        }

        public void AddItem(Product product)
        {
            if (product.InStock())
            {
                CartItem cartItem = AlreadyInCart(product);
                if (cartItem != null && cartItem.Product.InStock())
                {
                    cartItem.AddAnother();
                    TotalPrice = GetTotalPrice();
                    Debug.WriteLine("Added one more of the item \"" + cartItem.Product.Name + "\" to the cart, there's now a total of " + cartItem.NumItemsInCart);
                }
                else
                {
                    CartItems.Add(new CartItem(product));
                    TotalPrice = GetTotalPrice();
                    Debug.WriteLine(product.Name + " was added to the shopping cart");
                }   
            }       
        }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = new decimal();
            foreach (CartItem cartItem in CartItems)
            {
                totalPrice += cartItem.GetTotalPrice();
            }
            return totalPrice;
        }

        public void RemoveAllItems()
        {
            foreach (CartItem cartItem in CartItems)
            {
                cartItem.RestoreStock();
            }
            CartItems = new ObservableCollection<CartItem>();
            TotalPrice = GetTotalPrice();
            Debug.WriteLine("Removed all items from the shopping cart");
        }

        public void RemoveItem(Product product, bool removeAll)
        {
            CartItem cartItem = AlreadyInCart(product);
            if(cartItem == null) return;
            if (removeAll || cartItem.NumItemsInCart < 2)
            {
                cartItem.RestoreStock();
                CartItems.Remove(cartItem);
                Debug.WriteLine("Removed all " + cartItem.NumItemsInCart + " \"" + cartItem.Product.Name + "\" items from the shopping cart");
            } else 
                cartItem.RemoveOne();
            TotalPrice = GetTotalPrice();
        }

        public override string ToString()
        {
            if (CartItems.Count == 0) return "Cart is empty \n";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("----------------------------------------");
            stringBuilder.AppendLine("Shopping cart");
            stringBuilder.AppendLine("----------------------------------------");
            foreach (var product in CartItems)
            {
                stringBuilder.AppendLine(product.ToString());
            }
            stringBuilder.AppendLine("----------------------------------------");
            stringBuilder.AppendLine($"Total {TotalPrice, 34}");
            stringBuilder.AppendLine("----------------------------------------");

            return stringBuilder.ToString();
        }
    }
}
