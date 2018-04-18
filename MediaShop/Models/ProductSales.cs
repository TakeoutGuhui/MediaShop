using MediaShop.ViewModels;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Models
{
    class ProductSales : BaseViewModel
    {
        public Product Product { get; set; }
        private List<ProductSale> _sales;
        public List<ProductSale> Sales
        {
            get { return _sales; }
            set
            {
                if (value == _sales) return;
                _sales = value;
                RaisePropertyChangedEvent("ProductSales");
            }
        }
        private string _filePath;
        public ProductSales(Product product)
        {
            Product = product;
            _filePath = Properties.Settings.Default.salesFolder + Product.ID + ".csv";

            if (File.Exists(_filePath))
            {
                _sales = LoadSales();
            }
            else
            {
                _sales = new List<ProductSale>();
            }
        }

        public int TotalItemsSold
        {
            get
            {
                int totalSales = 0;
                foreach  (ProductSale sale in _sales)
                {
                    totalSales += sale.NumItems;
                }
                return totalSales;
            }
        }

        public decimal TotalIncome
        {
            get
            {
                decimal total = 0;
                foreach (ProductSale sale in _sales)
                {
                    total += sale.Price * sale.NumItems;
                }
                return total;
            }
        }
        public struct SaleStruct
        {
            public int ItemsSold { get; set; }
            public decimal MoneyMade { get; set; }
        }

        public SaleStruct AllTime
        {
            get
            {
                int itemsSold = 0;
                decimal moneyMade = 0;
                foreach (var sale in Sales)
                {
                    itemsSold += sale.NumItems;
                    moneyMade += sale.Price * sale.NumItems;
                }
                return new SaleStruct() { ItemsSold = itemsSold, MoneyMade = moneyMade };
            }
        }

        public SaleStruct ThisMonth
        {
            get
            {
                DateTime today = DateTime.Now;
                int totalSold = 0;
                decimal TotalMoney = 0;
                
                var salesThisMonth = Sales.Where(s => s.SaleDate.Month == today.Month && s.SaleDate.Year == today.Year);
                foreach  (var sale in salesThisMonth)
                {
                    totalSold += sale.NumItems;
                    TotalMoney += sale.Price * sale.NumItems;
                }
                return new SaleStruct() { ItemsSold = totalSold, MoneyMade = TotalMoney };
            }
        }

        public SaleStruct ThisYear
        {
            get
            {
                DateTime today = DateTime.Now;
                int totalSold = 0;
                decimal TotalMoney = 0;

                var salesThisMonth = Sales.Where(s => s.SaleDate.Year == today.Year);
                foreach (ProductSale sale in salesThisMonth)
                {
                    totalSold += sale.NumItems;
                    TotalMoney += sale.Price * sale.NumItems;
                }
                return new SaleStruct() { ItemsSold = totalSold, MoneyMade = TotalMoney };
            }
        }


        public void AddSale(ProductSale sale)
        {
            _sales.Add(sale);
            SaveSales(_sales);
        }

        public void DeleteSales()
        {
            File.Delete(_filePath);
        }

        private List<ProductSale> LoadSales()
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
                    productSales.Add(productSale);
                }
            }
            return productSales;
        }

        private string ConvertToCsv(ProductSale sale)
        {
            return string.Format("{0};{1};{2}",
                sale.NumItems.ToString(),
                sale.Price.ToString(),
                sale.SaleDate.ToString());
                
        }

        public void SaveSales(List<ProductSale> sales)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ProductSale sale in sales)
            {
                string saleLine = ConvertToCsv(sale);
                stringBuilder.AppendLine(saleLine);
            }
            File.WriteAllText(_filePath, stringBuilder.ToString());
        }

        
    }
}
