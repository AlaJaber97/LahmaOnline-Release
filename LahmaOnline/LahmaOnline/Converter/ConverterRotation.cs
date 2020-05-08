using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LahmaOnline.Converter
{
    class ConverterRotation : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((FlowDirection)LahmaOnline.StyleViews.Styles.SetterStyle.ViewFlowDirection.Value == FlowDirection.LeftToRight)
                return 0;
            else
                return 180;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
