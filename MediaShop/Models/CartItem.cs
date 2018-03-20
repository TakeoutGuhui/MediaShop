using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Models
{
    class CartItem 
    {
        public Product Product;

        private int _numItemsInCart;
        public int NumItemsInCart
        {
            get => _numItemsInCart;
            private set
            {
                if (_numItemsInCart + value <= Product.Stock)
                {
                    _numItemsInCart = value;
                }
            }
        }

        public CartItem(Product product)
        {
            Product = product;
            NumItemsInCart = 1;
        }

        public void AddAnother()
        {
            Debug.WriteLine("Items before: " + NumItemsInCart);
            if (NumItemsInCart + 1 <= Product.Stock)
            {
                NumItemsInCart += 1;
            }
            Debug.WriteLine("Items after: " + NumItemsInCart);
        }

        public void Checkout()
        {
            Product.Stock -= NumItemsInCart;
        }

        public decimal TotalPrice()
        {
            return Product.Price* NumItemsInCart;
        }

        public override string ToString()
        {
            //return Product + " items in cart: " + NumItemsInCart;
            string products = NumItemsInCart + "st*" + Product.Price;
            return $"{Product.Name,-15} {products, -10} {Product.Price * NumItemsInCart,10}";
        }
    }
}
