using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LahmaOnline.Converter
{
    public class intToSize : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "1":
                    return MultiLanguage.MLResource.Large;
                case "2":
                    return MultiLanguage.MLResource.Medium;
                case "3":
                    return MultiLanguage.MLResource.Small;
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
