using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using MediaShop.ViewModels;

namespace MediaShop.Models
{
    /// <summary>
    /// Class that represents a product
    /// </summary>
    internal class Product : BaseViewModel
    {
        /// <summary>
        /// The properties below represents the properties of a product
        /// </summary>
        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                if (ProductList.Instance.Products.Contains(this) && ProductList.Instance.IsIdTaken(value)) return; // If the productlist contains this product and the ID is already taken, return
                if (value == _id) return; // If the new ID is already taken, cancel
                _id = value;
                RaisePropertyChangedEvent("ID");

            }
        }
        private string _name = "";
        public string Name
        {
            get => _name;
            set
            {
                if (ProductList.Instance.Products.Contains(this) && ProductList.Instance.IsNameTaken(value)) return; // If the productlist contains this product and the Name is already taken, return
                if (value == _name) return; // if the new Name is already taken, cancel
                _name = value;
                RaisePropertyChangedEvent("Name");
            }
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                if(value == _price || value < 0) return;
                _price = value;
                RaisePropertyChangedEvent("Price");
            }
        }

        private uint _stock;
        public uint Stock
        {
            get => _stock;
            set
            {
                if (value != _stock)
                {
                    _stock = value;
                    RaisePropertyChangedEvent("Stock");
                }
                    
            }
        }

        private string _artist = "";
        public string Artist
        {
            get => _artist;
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
            get => _genre;
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
            get => _publisher;
            set
            {
                if(value == _publisher) return;
                _publisher = value;
                RaisePropertyChangedEvent("Publisher");
            }
        }

        private uint _year;
        public uint Year
        {
            get => _year;
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
            get => _comment;
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
            get => _productSales;
            set 
            {
                if (value == _productSales) return;
                _productSales = value;
                RaisePropertyChangedEvent("ProductSales");
            }
        }

        /// <summary>
        /// Checks if the product is in stock
        /// </summary>
        /// <returns> True if the product is in stock </returns>
        public bool InStock() => Stock > 0;

        /// <summary>
        /// Adds the parameter stock to this product's stock
        /// </summary>
        /// <param name="stock"> stock to be added</param>
        public void AddStock(uint stock)
        {
            Stock += stock;
        }

        public void Update(Product p)
        {
            Price = p.Price;
            Year = p.Year;
            Artist = p.Artist;
            Comment = p.Comment;
            Publisher = p.Publisher;
            Genre = p.Genre;
            Stock = p.Stock;
        }

        public override string ToString() => $"{Id,-4} {Name,-15} {Price,-10} {Stock,-10}";
    }
}
