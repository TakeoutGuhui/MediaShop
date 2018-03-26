using System;
using MediaShop.Models;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<Product> LoadProducts()
        {
            TextFieldParser parser = new TextFieldParser(_filePath);
            ObservableCollection<Product> products = new ObservableCollection<Product>();
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                if (fields != null && fields.Length == 4)
                {
                    int.TryParse(fields[0], out var productNumber);
                    string name = fields[1];
                    decimal.TryParse(fields[2], NumberStyles.Any, new CultureInfo("sv-SE"), out var price);
                    int.TryParse(fields[3], out var stock);
                    Product product = new Product() { ProductNumber = productNumber, Name = name, Price = price, Stock = stock};
                    products.Add(product);
                }
                
            }
            return products;
        }

        private static string ConvertToCsv(Product product)
        {
            //string price = product.Price.ToString(CultureInfo.InvariantCulture).Replace(",",".");
            return $"{product.ProductNumber};{product.Name};{product.Price};{product.Stock}";
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
