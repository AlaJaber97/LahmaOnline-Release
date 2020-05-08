using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LahmaOnline.Converter
{
    public class Language : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           // if (culture.TwoLetterISOLanguageName == "en")
                return ((BLL.M.Mobile.Category)value).Name;
            //else
            //    return ((BLL.M.Mobile.Category)value).NameAr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
