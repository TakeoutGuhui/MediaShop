using MediaShop.ViewModels;

namespace MediaShop.Models
{
    internal class Product : BaseViewModel
    {
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

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
        /*
        public Product(int productNumber, string name, decimal price, int stock)
        {
            ProductNumber = productNumber;
            Name = name;
            Price = price;
            Stock = stock;
        }
        */

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
