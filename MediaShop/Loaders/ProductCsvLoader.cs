using Microsoft.VisualBasic.FileIO;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;

using MediaShop.Models;

namespace MediaShop.Loaders
{
    class ProductCsvLoader : IProductLoader
    {
        private readonly string _filePath;
        public ProductCsvLoader(string filePath)
        {
            _filePath = filePath;
        }

        public ObservableCollection<Product> LoadProducts()
        {
            TextFieldParser parser = new TextFieldParser(_filePath);
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                if (fields != null && fields.Length == 9)
                {
                    int.TryParse(fields[0], out var productNumber);
                    string name = fields[1];
                    decimal.TryParse(fields[2], NumberStyles.Any, new CultureInfo("sv-SE"), out var price);
                    int.TryParse(fields[3], out var stock);
                    string artist = fields[4];
                    string publisher = fields[5];
                    string genre = fields[6];
                    int.TryParse(fields[7], out var year);
                    string comment = fields[8];
                    Product product = new Product() { ID = productNumber,
                                                      Name = name,
                                                      Price = price,
                                                      Stock = stock,
                                                      Artist = artist,
                                                      Publisher = publisher,
                                                      Genre = genre,
                                                      Year = year,
                                                      Comment = comment};
                    products.Add(product);
                }
                
            }
            return products;
        }

        private static string ConvertToCsv(Product product)
        {
            return $"{product.ID};{product.Name};{product.Price};{product.Stock};{product.Artist};{product.Publisher};{product.Genre};{product.Year};{product.Comment}";
        }

        public void SaveProducts(ObservableCollection<Product> products)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Product product in products)
            {
                string productLine = ConvertToCsv(product);
                stringBuilder.AppendLine(productLine);
            }
            File.WriteAllText(_filePath, stringBuilder.ToString());
        }
    }
}
