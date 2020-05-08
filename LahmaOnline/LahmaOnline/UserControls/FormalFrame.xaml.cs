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
    public partial class FormalFrame : ContentView
    {
        public Color ColorIndicator
        {
            set { BXindicator.BackgroundColor = value; }
        }

        public static BindableProperty TitleProperty = BindableProperty.Create(
                                                                                propertyName: nameof(Title),
                                                                                returnType: typeof(string),
                                                                                declaringType: typeof(FormalFrame),
                                                                                defaultValue: string.Empty,
                                                                                defaultBindingMode: BindingMode.OneWay,
                                                                                propertyChanged: TitlePropertyChanged);

        public string Title
        {
            get { return base.GetValue(TitleProperty).ToString(); }
            set { base.SetValue(TitleProperty, value); }
        }
        private static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == null || !oldValue.Equals(newValue))
            {
                var control = (FormalFrame)bindable;
                if (control == null) return;
                control.TitleFrame.Text = (string)newValue;
            }
        }

        public static BindableProperty ContentTextProperty = BindableProperty.Create(
                                                                        propertyName: nameof(ContentText),
                                                                        returnType: typeof(string),
                                                                        declaringType: typeof(FormalFrame),
                                                                        defaultValue: string.Empty,
                                                                        defaultBindingMode: BindingMode.OneWay,
                                                                        propertyChanged: ContentTextPropertyChanged);

        public string ContentText
        {
            get { return base.GetValue(ContentTextProperty).ToString(); }
            set { base.SetValue(ContentTextProperty, value); }
        }
        private static void ContentTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == null || !oldValue.Equals(newValue))
            {
                var control = (FormalFrame)bindable;
                if (control == null) return;
                control.ContentFrame.Text = (string)newValue;
            }
        }
        public FormalFrame()
        {
            InitializeComponent();
        }
    }
}