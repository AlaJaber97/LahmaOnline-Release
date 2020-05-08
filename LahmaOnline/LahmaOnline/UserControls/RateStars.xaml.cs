using LahmaOnline.StyleViews.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RateStars : ContentView
    {
        private ImageSource FillStarImageSource = null;
        private ImageSource EmptyStarImageSource = null;
        #region ValueRate
        public static readonly BindableProperty ValueRateProperty = BindableProperty.Create(nameof(ValueRate), typeof(int), typeof(RateStars), -1, BindingMode.OneWay, propertyChanged: OnValueRateChanged);

        public int ValueRate
        {
            get { return (int)GetValue(ValueRateProperty); }
            set { SetValue(ValueRateProperty, value); }
        }
        private static void OnValueRateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (bindable as RateStars);
            if(newValue != oldValue && newValue != null)
                control.Init((int)newValue);
        }
        #endregion
        #region ImageProperty
        public static readonly BindableProperty FillStarImageProperty = BindableProperty.Create(nameof(FillStarImage), typeof(string), typeof(RateStars), "", BindingMode.OneWay);
        public static readonly BindableProperty EmptyStarImageProperty = BindableProperty.Create(nameof(EmptyStarImage), typeof(string), typeof(RateStars), "", BindingMode.OneWay);
        public string FillStarImage
        {
            get { return (string)this.GetValue(FillStarImageProperty); }
            set { this.SetValue(FillStarImageProperty, value); }
        }

        public string EmptyStarImage
        {
            get { return (string)this.GetValue(EmptyStarImageProperty); }
            set { this.SetValue(EmptyStarImageProperty, value); }
        }
        #endregion
        private void Init(int Rate)
        {
            StackPage.Children.Clear();
            if (Rate >= 0 && Rate < 6)
            {
                if (FillStarImageSource == null)
                    FillStarImageSource = ImageSource.FromFile(FillStarImage);
                if (EmptyStarImageSource == null)
                    EmptyStarImageSource = ImageSource.FromFile(EmptyStarImage);

                for (int i = 0; i < 5; i++)
                {
                    if (Rate-- > 0)
                    {
                        StackPage.Children.Add(new FFImageLoading.Svg.Forms.SvgCachedImage { Source = FillStarImageSource, WidthRequest = SetterStyle.Maintain_WidthAspectRatio(15), HeightRequest = SetterStyle.Maintain_WidthAspectRatio(15) });
                    }
                    else
                    {
                        StackPage.Children.Add(new FFImageLoading.Svg.Forms.SvgCachedImage { Source = EmptyStarImageSource, WidthRequest = SetterStyle.Maintain_WidthAspectRatio(15), HeightRequest = SetterStyle.Maintain_WidthAspectRatio(15) });
                    }
                }
            }
        }
        public RateStars()
        {
            InitializeComponent();
        }
    }
}