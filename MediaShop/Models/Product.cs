namespace MediaShop.Models
{
    internal class Product
    {
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product(int productNumber, string name, decimal price, int stock)
        {
            ProductNumber = productNumber;
            Name = name;
            Price = price;
            Stock = stock;
        }

        public bool InStock()
        {
            return Stock > 0;
        }

        public override string ToString()
        {
            return $"Id: {ProductNumber} \n Name: {Name} \n Price: {Price} \n Stock: {Stock} \n";
        }
    }
}
