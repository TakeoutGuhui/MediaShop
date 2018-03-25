using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using MediaShop.Commands;
using MediaShop.Models;

namespace MediaShop.ViewModels
{
    class ShopViewModel : BaseViewModel
    {
        public ProductList ProductList { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public Product SelectedProduct { get; set; }
        public ShopViewModel(ProductList productList)
        {
            
            ProductList = productList;
            ShoppingCart = new ShoppingCart();


            ProductListEditor editor = new ProductListEditor(productList);
            /*
            editor.AddProduct(new Product(1, "Funkar!", 23.90m, 32));
            
            
            ShoppingCart.AddItem(ProductList.Products.ElementAt(0));
            ShoppingCart.AddItem(ProductList.Products.ElementAt(0));
            ShoppingCart.AddItem(productList.Products.ElementAt(1));
            ShoppingCart.AddItem(productList.Products.ElementAt(1));
            ShoppingCart.AddItem(ProductList.Products.ElementAt(2));
            ShoppingCart.AddItem(ProductList.Products.ElementAt(2));
            ShoppingCart.AddItem(ProductList.Products.ElementAt(3));
            ShoppingCart.AddItem(ProductList.Products.ElementAt(3));
            ShoppingCart.AddItem(ProductList.Products.ElementAt(4));
            ShoppingCart.AddItem(productList.Products.ElementAt(5));
            
            Debug.WriteLine(ProductList);
            Debug.WriteLine(ShoppingCart);

            ShoppingCart.RemoveItem(productList.Products.ElementAt(1), false);
            ShoppingCart.RemoveItem(ProductList.Products.ElementAt(2), true);
            
            Debug.WriteLine(ShoppingCart);

            //ShoppingCart.Checkout();
            
            Debug.WriteLine(ShoppingCart);
            */
        }
        public ICommand AddToCartCommand => new DelegateCommand(AddToCart);

        private void AddToCart()
        {
            Debug.WriteLine("DOUBLE CLICK!");
            if (SelectedProduct != null)
            {
                ShoppingCart.AddItem(SelectedProduct);
            }
        }
    }
    
}
