using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing.Printing;
using MediaShop.Models;
using System.Windows.Controls;

namespace MediaShop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ProductList.Instance.LoadProducts(); //Load the products when the program is starting
        }
    }
}
