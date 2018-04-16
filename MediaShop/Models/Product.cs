using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using MediaShop.ViewModels;

namespace MediaShop.Models
{
    internal class Product : BaseViewModel
    {
        private static readonly HashSet<int> TakenIDs = new HashSet<int>();
        private static readonly HashSet<string> TakenNames = new HashSet<string>();


        public static bool ValidId(string id){ return Regex.IsMatch(id, "[^0-9]+"); }
        private int _id;
        public int ID
        {
            get { return _id; }
            set
            {


                if (value == _id) return;
                if (TakenIDs.Contains(value))
                {
                    _id = -1;
                }
                else
                {
                    if (TakenIDs.Contains(_id))
                    {
                        TakenIDs.Remove(_id);
                    }
                    _id = value;
                    TakenIDs.Add(_id);
                }
                RaisePropertyChangedEvent("ID");

            }
        }

        public static bool ValidName(string name) { return true; }
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                if(value == _name) return;
                if (TakenNames.Contains(value))
                {
                    _name = "";
                }
                else
                {
                    if (TakenNames.Contains(_name))
                    {
                        TakenNames.Remove(_name);
                    }
                    _name = value;
                    TakenNames.Add(_name);
                }
                RaisePropertyChangedEvent("Name");
            }
        }

        public static bool ValidPrice(string price) { return true; }
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

        public static bool ValidStock(string price) { return Regex.IsMatch(price, "[^0-9]+");  }
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

        public static bool ValidArtist(string artist) { return true; }
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

        public static bool ValidGenre(string genre){ return true; }
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

        public static bool ValidPublisher(string publisher){ return true; }
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

        public static bool ValidYear(string year) { return true; }
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

        public static bool ValidComment(string comment) { return true; }
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
