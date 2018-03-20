using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public void AddItem(Product product)
        {
            if (product.InStock())
            {
                CartItem cartItem = AlreadyInCart(product);
                if (cartItem != null)
                {
                    cartItem.AddAnother();
                }
                else
                {
                    CartItems.Add(new CartItem(product));
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

        public void RemoveAll()
        {
            CartItems = new List<CartItem>();
        }

        public void RemoveItem(Product product)
        {
            CartItem cartProduct = AlreadyInCart(product);
            if (cartProduct != null)
                CartItems.Remove(cartProduct);
        }

        public override string ToString()
        {
            if (CartItems.Count == 0) return "Cart is empty";
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

            return stringBuilder.ToString();
        }
    }
}
