using MediaShop.Models;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace MediaShop.Loaders
{
    class ProductCsvLoader : IProductLoader
    {
        private readonly string _filePath;
        public ProductCsvLoader(string filePath)
        {
            _filePath = filePath;
        }

        public List<Product> LoadProducts()
        {
            TextFieldParser parser = new TextFieldParser(_filePath);
            List<Product> products = new List<Product>();
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                if (fields != null && fields.Length == 4)
                {
                    Product product = new Product(int.Parse(fields[0]), fields[1], decimal.Parse(fields[2], CultureInfo.InvariantCulture), int.Parse(fields[3]));
                    products.Add(product);
                }
                
            }
            return products;
        }

        private static string ConvertToCsv(Product product)
        {
            string price = product.Price.ToString(CultureInfo.InvariantCulture).Replace(",",".");
            return $"{product.ProductNumber},{product.Name},{price},{product.Stock}";
        }

        public void SaveProducts(List<Product> products)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Product product in products)
            {
                string productLine = ConvertToCsv(product);
                stringBuilder.Append(productLine);
                stringBuilder.AppendLine();
            }
            File.WriteAllText(_filePath, stringBuilder.ToString());
        }
    }
}
