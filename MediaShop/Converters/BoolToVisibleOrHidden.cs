using MediaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MediaShop.Converters
{

        //Copied from https://www.rhyous.com/2011/02/22/binding-visibility-to-a-bool-value-in-wpf/
        class BoolToVisibleOrHidden : IValueConverter
        {
            #region Constructors
            /// <summary>
            /// The default constructor
            /// </summary>
            public BoolToVisibleOrHidden() { }
            #endregion

            #region IValueConverter Members
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                Product product = (Product)value;
                if (product != null)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                Visibility visibility = (Visibility)value;

                if (visibility == Visibility.Visible)
                    return new Product();
                else
                    return null;
            }
            #endregion
        }
    
}
