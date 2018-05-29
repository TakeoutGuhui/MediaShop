using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

using MediaShop.ViewModels;

namespace MediaShop.Models
{
    /// <summary>
    /// This class represents the shopping car
    /// </summary>
    class ShoppingCart : BaseViewModel
    {
        /// <summary>
        /// The items in the cart
        /// </summary>
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

        /// <summary>
        /// The price of all the items in the cart
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

        public ShoppingCart()
        {
            CartItems = new ObservableCollection<CartItem>();
        }

        /// <summary>
        /// Checks if the product already is in the cart
        /// </summary>
        /// <param name="product"></param>
        /// <returns> Returns the CartItem of the product if it exists, else null </returns>
        public CartItem AlreadyInCart(Product product)
        {
            foreach (CartItem cartItem in CartItems)
            {
                if (cartItem.Product.Equals(product)) return cartItem;
            }

            return null;
        }

        /// <summary>
        /// Checks out the cart
        /// </summary>
        public void Checkout()
        {
            foreach (CartItem cartItem in CartItems)
            {
                cartItem.Checkout(); // Each CartItem is checked out
            }
            CartItems = new ObservableCollection<CartItem>(); // The list gets reset
            TotalPrice = GetTotalPrice(); // Total price is updated
            Debug.WriteLine("Cart was checked out");
        }

        public bool Empty()
        {
            return CartItems.Count == 0;
        }

        /// <summary>
        /// Adds a product to the cart
        /// </summary>
        /// <param name="product"> The product that will be added to the cart </param>
        public void AddItem(Product product)
        {
            if (!product.InStock()) return; // If the product isn't in stoct, cancel
            CartItem cartItem = AlreadyInCart(product); // Checks if the Product already is in the cart

            if (cartItem != null) // If the product already is in the cart increase the CartItem with one
            {
                if(cartItem.InStock()) cartItem.AddAnother();
                Debug.WriteLine("Added one more of the item \"" + cartItem.Product.Name + "\" to the cart, there's now a total of " + cartItem.NumItemsInCart);
            }
            else // else make a new CartItem for the product and add it to CartItems
            {
                CartItems.Add(new CartItem(product));
                Debug.WriteLine(product.Name + " was added to the shopping cart");
            }

            TotalPrice = GetTotalPrice(); // Update the price
                 
        }

        /// <summary>
        /// Calculates the total price of the shopping cart
        /// </summary>
        /// <returns> The total price of the shopping cart </returns>
        public decimal GetTotalPrice()
        {
            decimal totalPrice = new decimal();
            foreach (CartItem cartItem in CartItems)
            {
                totalPrice += cartItem.GetTotalPrice();
            }
            return totalPrice;
        }

        /// <summary>
        /// Removes all items from the shopping cart
        /// </summary>
        public void RemoveAllItems()
        {
            CartItems = new ObservableCollection<CartItem>();
            TotalPrice = GetTotalPrice();
            Debug.WriteLine("Removed all items from the shopping cart");
        }

        /// <summary>
        /// Removes an item from the cart 
        /// </summary>
        /// <param name="product"> The product that should be removed</param>
        /// <param name="removeAll"> If True remove all items else only remove one</param>
        public void RemoveItem(Product product, bool removeAll)
        {
            CartItem cartItem = AlreadyInCart(product);
            if(cartItem == null) return; // If the product isn't in the cart, cancel
            if (removeAll || cartItem.NumItemsInCart < 2) // If removeAll or there is only one item of the product in the cart, remove the product from the shopping cart 
            {
                CartItems.Remove(cartItem);
                Debug.WriteLine("Removed all " + cartItem.NumItemsInCart + " \"" + cartItem.Product.Name + "\" items from the shopping cart");
            } else // else remove only one from the cartitem
                cartItem.RemoveOne();
            TotalPrice = GetTotalPrice();
        }

        /// <summary>
        /// Used for printing the receipt
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (CartItems.Count == 0) return "Cart is empty \n";
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("----------------------------------------");
            stringBuilder.AppendLine("Receipt");
            stringBuilder.AppendLine("----------------------------------------");
            foreach (var product in CartItems)
            {
                stringBuilder.AppendLine(product.ToString());
            }
            stringBuilder.AppendLine("----------------------------------------");
            stringBuilder.AppendLine($"Total {TotalPrice,34}");
            stringBuilder.AppendLine("----------------------------------------");

            return stringBuilder.ToString();
        }
    }
}
