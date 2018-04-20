using MediaShop.ViewModels;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

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
            get { return _sales; }
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
        private string _productID;
        public string ProductID
        {
            get { return _productID; }
            set
            {
                if (value == _productID) return;
                _productID = value;
                RaisePropertyChangedEvent("ProductID");
            }
        }

        /// <summary>
        /// The name of the product
        /// </summary>
        private string _productName;
        public string ProductName
        {
            get { return _productName; }
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
        private string _filePath;
        public ProductSales(string id, string name)
        {
            ProductID = id;
            ProductName = name;
            _filePath = Properties.Settings.Default.salesFolder + ProductID + ".csv";
            Sales = new List<ProductSale>();
            if (File.Exists(_filePath)) // If there alreade exists a file for this product, load it. Else make a empty list
            {
                LoadSales();
            }
        }

        /// <summary>
        /// Used to make it easy to return stats for a time period
        /// </summary>
        public struct ProductInfo
        {
            public int ItemsSold { get; set; }
            public decimal MoneyMade { get; set; }
        }

        /// <summary>
        /// Makes a SaleStruct with the sum of the sales in the theSales list
        /// </summary>
        /// <param name="theSales"> The list that will be summed </param>
        /// <returns></returns>
        private ProductInfo MakeProductInfo(List<ProductSale> theSales)
        {
            int itemsSold = 0;
            decimal moneyMade = 0;
            foreach (var sale in theSales)
            {
                itemsSold += sale.NumItems;
                moneyMade += sale.TotalPrice;
            }
            return new ProductInfo() { ItemsSold = itemsSold, MoneyMade = moneyMade };
        }

        /// <summary>
        /// Returns a SaleStruct with the properties set to all time stats
        /// </summary>
        public ProductInfo AllTime
        {
            get { return MakeProductInfo(Sales); }
        }

        /// <summary>
        /// Returns a SaleStruct with the properties set to this month's stats
        /// </summary>
        public ProductInfo ThisMonth
        {
            get
            {
                DateTime today = DateTime.Now;
                return MakeProductInfo(Sales.Where(s => s.SaleDate.Month == today.Month && s.SaleDate.Year == today.Year).ToList());
            }
        }


        /// <summary>
        /// Returns a SaleStruct with the properties set to this year's stats
        /// </summary>
        public ProductInfo ThisYear
        {
            get
            {
                DateTime today = DateTime.Now;
                return MakeProductInfo(Sales.Where(s => s.SaleDate.Year == today.Year).ToList());
            }
        }


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
        public void DeleteSales()
        {
            File.Delete(_filePath);
        }

        /// <summary>
        /// Loads sales from the file that _filePath points to
        /// </summary>
        /// <returns> A list of sales </returns>
        private void LoadSales()
        {
            TextFieldParser parser = new TextFieldParser(_filePath);
            List<ProductSale> productSales = new List<ProductSale>();
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";");

            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                int numItems;
                decimal price;
                DateTime dateTime;
                if (fields != null && fields.Length == 3)
                {
                    int.TryParse(fields[0], out numItems);
                    decimal.TryParse(fields[1], NumberStyles.Any, new CultureInfo("sv-SE"), out price);
                    DateTime.TryParse(fields[2], out dateTime);

                    ProductSale productSale = new ProductSale
                    {
                        NumItems = numItems,
                        Price = price,
                        SaleDate = dateTime
                    };
                    AddSale(productSale);
                }
            }
        }

        /// <summary>
        /// Converts the sale to csv-format
        /// </summary>
        /// <param name="saleToConvert"></param>
        /// <returns></returns>
        private string ConvertToCsv(ProductSale saleToConvert)
        {
            return string.Format("{0};{1};{2}",
                saleToConvert.NumItems.ToString(),
                saleToConvert.Price.ToString(),
                saleToConvert.SaleDate.ToString());
                
        }

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
