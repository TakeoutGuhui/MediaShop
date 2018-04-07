using System.Diagnostics;
using System.Windows.Input;

using MediaShop.Commands;
using MediaShop.Models;
using System.Windows.Data;

namespace MediaShop.ViewModels
{
    class ShopViewModel : BaseViewModel
    {
        public ProductList ProductList { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public CollectionViewSource ProductViewSource {get;set;}

        public Product SelectedProduct { get; set; }
        public CartItem SelectCartItem { get; set; }
        public ShopViewModel(ProductList productList)
        {
            ProductList = productList;
            ShoppingCart = new ShoppingCart();
            ProductViewSource = new CollectionViewSource();
            ProductViewSource.Source = ProductList.Products;        
        }

        public ICommand AddToCartCommand { get { return new DelegateCommand(AddToCart); } }

        private void AddToCart()
        {
            Debug.WriteLine("DOUBLE CLICK!");
            if (SelectedProduct != null)
            {
                ShoppingCart.AddItem(SelectedProduct);
            }
            
        }

        public ICommand RemoveFromCartCommand { get { return new DelegateCommand(RemoveFromCart); } }

        private void RemoveFromCart()
        {
            Debug.WriteLine("Renoe");
            if (SelectCartItem != null)
            {
                ShoppingCart.RemoveItem(SelectCartItem.Product, false);
            }
        }

        public ICommand CheckoutCommand { get { return new DelegateCommand(Checkout); } }
        private void Checkout()
        {
            ShoppingCart.Checkout();
            ProductList.SaveProducts();
        }
    }
    
}
