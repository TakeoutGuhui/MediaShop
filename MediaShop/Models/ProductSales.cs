using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using MediaShop.Properties;
using MediaShop.ViewModels;
using Microsoft.VisualBasic.FileIO;

namespace MediaShop.Models
{
    /// <summary>
    /// Represents a product's sales
    /// </summary>
    class ProductSales : BaseViewModel
    {
        private List<ProductSale> _sales;
        public List<ProductSale> Sales
        {
            get => _sales;
            set
            {
                if (value == _sales) return;
                _sales = value;
                RaisePropertyChangedEvent("Sales");
            }
        }

        /// <summary>
        /// The id of the product
        /// </summary>
        private string _productId;
        public string ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId) return;
                _productId = value;
                RaisePropertyChangedEvent("ProductID");
            }
        }

        /// <summary>
        /// The name of the product
        /// </summary>
        private string _productName;
        public string ProductName
        {
            get => _productName;
            set
            {
                if (value == _productName) return;
                _productName = value;
                RaisePropertyChangedEvent("ProductName");
            }
        }

        /// <summary>
        /// Path to the file where the sales are saved
        /// </summary>
        private readonly string _filePath;
        public ProductSales(string id, string name)
        {
            ProductId = id;
            ProductName = name;
            _filePath = Settings.Default.SalesFolder + ProductId + ".csv";
            Sales = new List<ProductSale>();
            if (File.Exists(_filePath)) // If there alreade exists a file for this product, load it. Else keep the empty list
            {
                LoadSales();
            }
        }

        /// <summary>
        /// Used to make it easy to return stats for a time period
        /// </summary>
        public struct ProductInfo
        {
            public uint ItemsSold { get; set; }
            public decimal MoneyMade { get; set; }
        }

        /// <summary>
        /// Makes a SaleStruct with the sum of the sales in the theSales list
        /// </summary>
        /// <param name="theSales"> The list that will be summed </param>
        /// <returns></returns>
        private ProductInfo MakeProductInfo(List<ProductSale> theSales)
        {
            uint itemsSold = 0;
            decimal moneyMade = 0;
            foreach (var sale in theSales)
            {
                itemsSold += sale.NumItems;
                moneyMade += sale.TotalPrice;
            }
            return new ProductInfo { ItemsSold = itemsSold, MoneyMade = moneyMade };
        }

        /// <summary>
        /// Returns a SaleStruct with the properties set to all time stats
        /// </summary>
        public ProductInfo AllTime => MakeProductInfo(Sales);

        /// <summary>
        /// Returns a SaleStruct with the properties set to this month's stats
        /// </summary>
        public ProductInfo ThisMonth => MakeProductInfo(Sales.Where(s => s.SaleDate.Month == DateTime.Now.Month && s.SaleDate.Year == DateTime.Now.Year).ToList());

        /// <summary>
        /// Returns a SaleStruct with the properties set to this year's stats
        /// </summary>
        public ProductInfo ThisYear => MakeProductInfo(Sales.Where(s => s.SaleDate.Year == DateTime.Now.Year).ToList());

        /// <summary>
        /// Adds a sale and then saves the sales to the file
        /// </summary>
        /// <param name="sale"></param>
        public void AddSale(ProductSale sale)
        {
            Sales.Add(sale);
            Sales = Sales.OrderByDescending(x => x.SaleDate).ToList();
            SaveSales(_sales);
        }

        /// <summary>
        /// Deletes the sale file for this product
        /// </summary>
        public void DeleteSales() => File.Delete(_filePath);

        /// <summary>
        /// Loads sales from the file that _filePath points to
        /// </summary>
        /// <returns> A list of sales </returns>
        private void LoadSales()
        {
            TextFieldParser parser = new TextFieldParser(_filePath) {TextFieldType = FieldType.Delimited};
            parser.SetDelimiters(";");

            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                if (fields == null || fields.Length != 3) continue;
                uint.TryParse(fields[0], out var numItems);
                decimal.TryParse(fields[1], NumberStyles.Any, new CultureInfo("sv-SE"), out var price);
                DateTime.TryParse(fields[2], out var dateTime);

                ProductSale productSale = new ProductSale
                {
                    NumItems = numItems,
                    Price = price,
                    SaleDate = dateTime
                };
                AddSale(productSale);
            }
        }

        /// <summary>
        /// Converts the sale to csv-format
        /// </summary>
        /// <param name="saleToConvert"></param>
        /// <returns></returns>
        private static string ConvertToCsv(ProductSale saleToConvert) =>
            $"{saleToConvert.NumItems};{saleToConvert.Price};{saleToConvert.SaleDate}";

        /// <summary>
        /// Saves the sales to file
        /// </summary>
        /// <param name="salesToBeSaved"> The sales that will be saved to the file </param>
        public void SaveSales(List<ProductSale> salesToBeSaved)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ProductSale sale in salesToBeSaved)
            {
                string saleLine = ConvertToCsv(sale);
                stringBuilder.AppendLine(saleLine);
            }
            File.WriteAllText(_filePath, stringBuilder.ToString());
        }      
    }
}
