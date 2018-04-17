﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using MediaShop.ViewModels;

namespace MediaShop.Models
{
    internal class Product : BaseViewModel
    {
        private string _id;
        public string ID
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                RaisePropertyChangedEvent("ID");
            }
        }
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                if(value == _name) return;
                _name = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                if(value == _price) return;
                _price = value;
                RaisePropertyChangedEvent("Price");
            }
        }

        private int _stock;
        public int Stock
        {
            get { return _stock; }
            set
            {
                if (value != _stock && value >= 0)
                {
                    _stock = value;
                    RaisePropertyChangedEvent("Stock");
                }
                    
            }
        }

        private string _artist = "";
        public string Artist
        {
            get { return _artist; }
            set
            {
                if(value == _artist) return;
                _artist = value;
                RaisePropertyChangedEvent("Artist");
            }
        }

        private string _genre = "";
        public string Genre
        {
            get { return _genre; }
            set
            {
                if (value == _genre) return;
                _genre = value;
                RaisePropertyChangedEvent("Genre");
            }
        }

        private string _publisher = "";
        public string Publisher
        {
            get { return _publisher; }
            set
            {
                if(value == _publisher) return;
                _publisher = value;
                RaisePropertyChangedEvent("Publisher");
            }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set
            {
                if(value == _year) return;
                _year = value;
                RaisePropertyChangedEvent("Year");
            }
        }

        private string _comment = "";
        public string Comment
        {
            get { return _comment; }
            set
            {
                if (value == _comment) return;
                _comment = value;
                RaisePropertyChangedEvent("Comment");
            }
        }

        private ProductSales _productSales;
        public ProductSales ProductSales
        {
            get { return _productSales; }
            set 
            {
                if (value == _productSales) return;
                _productSales = value;
                RaisePropertyChangedEvent("ProductSales");
            }
        }

        public bool InStock()
        {
            return Stock > 0;
        }

        public override string ToString()
        {
            return string.Format("{0,-4} {1,-15} {2,-10} {3,-10}", ID, Name, Price, Stock);
        }
    }
}
