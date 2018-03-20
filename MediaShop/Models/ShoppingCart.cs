using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MediaShop.Models
{
    class ShoppingCart
    {
        public List<CartItem> CartItems;
        public ShoppingCart()
        {
            CartItems = new List<CartItem>();
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
            CartItems = new List<CartItem>();
            Debug.WriteLine("Cart was checked out");
        }

        public void AddItem(Product product)
        {
            if (product.InStock())
            {
                CartItem cartItem = AlreadyInCart(product);
                if (cartItem != null)
                {
                    cartItem.AddAnother();
                    Debug.WriteLine("Added one more of the item \"" + cartItem.Product.Name + "\" to the cart, now a total of " + cartItem.NumItemsInCart);
                }
                else
                {
                    CartItems.Add(new CartItem(product));
                    Debug.WriteLine(product.Name + " was added to the shopping cart");
                }   
            }       
        }

        public decimal TotalPrice()
        {
            decimal totalPrice = new decimal();
            foreach (CartItem cartItem in CartItems)
            {
                totalPrice += cartItem.TotalPrice();
            }
            return totalPrice;
        }

        public void RemoveAllItems()
        {
            CartItems = new List<CartItem>();
        }

        public void RemoveItem(Product product, bool removeAll)
        {
            CartItem cartProduct = AlreadyInCart(product);
            if(cartProduct == null) return;
            if (removeAll || cartProduct.NumItemsInCart < 2)
                CartItems.Remove(cartProduct);
            else 
                cartProduct.RemoveOne();
        }

        public override string ToString()
        {
            if (CartItems.Count == 0) return "Cart is empty \n";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("-------------------------------------");
            stringBuilder.AppendLine();
            stringBuilder.Append("Shopping cart");
            stringBuilder.AppendLine();
            stringBuilder.Append("-------------------------------------");
            stringBuilder.AppendLine();
            foreach (var product in CartItems)
            {
                stringBuilder.Append(product);
                stringBuilder.AppendLine();
            }
            stringBuilder.Append("-------------------------------------");
            stringBuilder.AppendLine();
            stringBuilder.Append($"Total {TotalPrice(),31}");
            stringBuilder.AppendLine();
            stringBuilder.Append("-------------------------------------");
            stringBuilder.AppendLine();

            return stringBuilder.ToString();
        }
    }
}
