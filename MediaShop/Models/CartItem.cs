﻿using MediaShop.ViewModels;
using System;

namespace MediaShop.Models
{
    class CartItem : BaseViewModel
    {
        public Product Product { get; set; }

        private int _numItemsInCart;
        public int NumItemsInCart
        {
            get { return _numItemsInCart; }
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
            get { return _totalPrice; }
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
        }

        public void AddAnother()
        {
            if (Product.InStock())
            {
                NumItemsInCart += 1;
            }
        }

        public void RemoveOne()
        {
            if (NumItemsInCart > 1)
            {
                NumItemsInCart -= 1;
            }
        }

        public bool InStock()
        {
            return NumItemsInCart < Product.Stock;
        }

        public void Checkout()
        {
            Product.Stock -= NumItemsInCart;
            Product.ProductSales.AddSale(new ProductSale { NumItems = NumItemsInCart, Price = Product.Price, SaleDate = DateTime.Now });
        }

        public decimal GetTotalPrice()
        {
            return Product.Price * NumItemsInCart;
        }

        public override string ToString()
        {
            string products = NumItemsInCart + "st*" + Product.Price;
            //return $"{Product.Name,-15} {products, -13} {Product.Price * NumItemsInCart,10}";
            return string.Format("{0,-15} {1,-13} {2,10}", Product.Name, products, Product.Price * NumItemsInCart);
        }
    }
}
