using System.Diagnostics;
using System.Linq;
using MediaShop.Models;

namespace MediaShop.ViewModels
{
    class ShopViewModel : BaseViewModel
    {
        public ProductList ProductList { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public ShopViewModel(ProductList productList)
        {
            ProductList = productList;
            ShoppingCart = new ShoppingCart();
            
            ShoppingCart.AddItem(ProductList.Products.ElementAt(0));
            ShoppingCart.AddItem(ProductList.Products.ElementAt(0));
            ShoppingCart.AddItem(productList.Products.ElementAt(1));
            ShoppingCart.AddItem(productList.Products.ElementAt(1));
            ShoppingCart.AddItem(ProductList.Products.ElementAt(2));
            ShoppingCart.AddItem(ProductList.Products.ElementAt(2));
            ShoppingCart.AddItem(ProductList.Products.ElementAt(3));
            ShoppingCart.AddItem(ProductList.Products.ElementAt(3));
            ShoppingCart.AddItem(ProductList.Products.ElementAt(4));
            
            Debug.WriteLine(ShoppingCart);

            ShoppingCart.RemoveItem(productList.Products.ElementAt(1), false);
            ShoppingCart.RemoveItem(ProductList.Products.ElementAt(2), true);
            
            Debug.WriteLine(ShoppingCart);

            ShoppingCart.Checkout();
            
            Debug.WriteLine(ShoppingCart);
        }
    }
}
