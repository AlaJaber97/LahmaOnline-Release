using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Text;

namespace LahmaOnline.CustomRenderer
{
    public class VGradientBoxView : BoxView
    {
        public static readonly BindableProperty RadiusProperty = BindableProperty.Create(nameof(Radius), typeof(double), typeof(VGradientBoxView), 0.0);
        public double Radius
        {
            get
            {
                return (double)GetValue(RadiusProperty);
            }
            set
            {
                SetValue(RadiusProperty, value);
            }
        }
        public Color StartColor
        {
            get;
            set;
        }

        public Color EndColor
        {
            get;
            set;
        }
    }

}
