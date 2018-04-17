﻿using MediaShop.ViewModels;
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
        private Product _product;
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
            _product = product;
            _filePath = Properties.Settings.Default.salesFolder + _product.ID;

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
