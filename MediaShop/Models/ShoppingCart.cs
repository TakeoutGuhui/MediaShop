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
                    Debug.WriteLine("Added one more of the item \"" + cartItem.Product.Name + "\" to the cart, there's now a total of " + cartItem.NumItemsInCart);
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
            Debug.WriteLine("Removed all items from the shopping cart");
        }

        public void RemoveItem(Product product, bool removeAll)
        {
            CartItem cartItem = AlreadyInCart(product);
            if(cartItem == null) return;
            if (removeAll || cartItem.NumItemsInCart < 2)
            {
                CartItems.Remove(cartItem);
                Debug.WriteLine("Removed all " + cartItem.NumItemsInCart + " \"" + cartItem.Product.Name + "\" items from the shopping cart");
            } else 
                cartItem.RemoveOne();
        }

        public override string ToString()
        {
            if (CartItems.Count == 0) return "Cart is empty \n";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            stringBuilder.Append("-------------------------------------").AppendLine();
            stringBuilder.Append("Shopping cart").AppendLine();
            stringBuilder.Append("-------------------------------------").AppendLine();
            foreach (var product in CartItems)
            {
                stringBuilder.Append(product).AppendLine();
            }
            stringBuilder.Append("-------------------------------------").AppendLine();
            stringBuilder.Append($"Total {TotalPrice(), 31}").AppendLine();
            stringBuilder.Append("-------------------------------------").AppendLine();

            return stringBuilder.ToString();
        }
    }
}
