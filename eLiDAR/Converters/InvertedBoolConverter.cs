using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace eLiDAR.Converters
{

    public class InvertedBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool returnValue = (bool)value;
            return !returnValue;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}