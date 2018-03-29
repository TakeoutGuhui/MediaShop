using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using MediaShop.ViewModels;

namespace MediaShop.Models
{
    internal class Product : BaseViewModel
    {
        private int _productNumber;
        public int ProductNumber
        {
            get => _productNumber;
            set
            {
                if (value == _productNumber) return;
                _productNumber = value;
                RaisePropertyChangedEvent("ProductNumber");
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
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
            get => _price;
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
            get => _stock;
            set
            {
                if (value != _stock && value >= 0)
                {
                    _stock = value;
                    RaisePropertyChangedEvent("Stock");
                }
                    
            }
        }

        private string _artist;
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

        private string _genre;
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

        private string _comment;
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

        private string _publisher;
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

        private int _year;

        public int Year
        {
            get => _year;
            set
            {
                if(value == _year) return;
                _year = value;
                RaisePropertyChangedEvent("Year");
            }
        }

        public bool InStock()
        {
            return Stock > 0;
        }

        public override string ToString()
        {
            return $"{ProductNumber,-4} {Name,-15} {Price,-10} {Stock,-10}";
        }
    }
}
