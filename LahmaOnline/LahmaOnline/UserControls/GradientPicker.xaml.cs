using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LahmaOnline.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GradientPicker : ContentView
    {
        #region StartColorButtonProperty
            public static BindableProperty StartColorButtonProperty = BindableProperty.Create(
                                                propertyName: "StartColor",
                                                returnType: typeof(Color),
                                                declaringType: typeof(GradientPicker),
                                                defaultValue: Color.White,
                                                defaultBindingMode: BindingMode.OneWay,
                                                propertyChanged: StartColorButtonPropertyChanged);

        public Color StartColor
        {
            get { return (Color)base.GetValue(StartColorButtonProperty); }
            set { base.SetValue(StartColorButtonProperty, value); }
        }

        private static void StartColorButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (GradientPicker)bindable;
            if (control == null) return;
            control.OnPropertyChanged(nameof(control.StartColor));
            if ((Color)newValue != null)
                control.GradientBox.StartColor = (Color)newValue;
        }

        #endregion
        #region EndColorProperty  
        public static BindableProperty EndColorButtonProperty = BindableProperty.Create(
                                                propertyName: "EndColor",
                                                returnType: typeof(Color),
                                                declaringType: typeof(GradientPicker),
                                                defaultValue: Color.White,
                                                defaultBindingMode: BindingMode.OneWay,
                                                propertyChanged: EndColorButtonPropertyChanged);

        public Color EndColor
        {
            get { return (Color)base.GetValue(EndColorButtonProperty); }
            set { base.SetValue(EndColorButtonProperty, value); }
        }
        private static void EndColorButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (GradientPicker)bindable;
            if (control == null) return;

            if ((Color)newValue != null)
            {
                var color = (Color)newValue;
                control.OnPropertyChanged(nameof(control.EndColor));
                control.GradientBox.EndColor = color;
            }
            else
                return;
        }
        #endregion
        #region HeightRequestPickerProperty
        public static BindableProperty HeightRequestPickerProperty = BindableProperty.Create(
                                                propertyName: "HeightRequestPicker",
                                                returnType: typeof(double),
                                                declaringType: typeof(GradientPicker),
                                                defaultValue: 1.0,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: HeightRequestPickerPropertyChanged);

        public double HeightRequestPicker
        {
            get { return (double)base.GetValue(HeightRequestPickerProperty); }
            set { base.SetValue(HeightRequestPickerProperty, value); }
        }

        private static void HeightRequestPickerPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (GradientPicker)bindable;
            if (control == null) return;
            control.OnPropertyChanged(nameof(control.HeightRequestPicker));
            control.FrameContainer.HeightRequest = (double)newValue;
            control.ImgArrow.HeightRequest = control.ImgArrow.WidthRequest = ((double)newValue) * 0.15;
        }
        #endregion
        #region WidthRequestPickerProperty
        public static BindableProperty WidthRequestPickerProperty = BindableProperty.Create(
                                                propertyName: "WidthRequestPicker",
                                                returnType: typeof(double),
                                                declaringType: typeof(GradientPicker),
                                                defaultValue: 1.0,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: WidthRequestPickerPropertyChanged);

        public double WidthRequestPicker
        {
            get { return (double)base.GetValue(WidthRequestPickerProperty); }
            set { base.SetValue(WidthRequestPickerProperty, value); }
        }

        private static void WidthRequestPickerPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (GradientPicker)bindable;
            if (control == null) return;
            control.OnPropertyChanged(nameof(control.WidthRequestPicker));
            control.FrameContainer.WidthRequest = (double)newValue;
            control.PickerChoice.WidthRequest = (double)newValue * 125/169.0;            
            control.GradientBox.WidthRequest = ((double)newValue) * 3 / 169.0;
            control.PickerChoice.Margin = new Thickness((double)newValue * 10 / 169.0,0,0,0);

            if (control.StackLayout.FlowDirection == FlowDirection.LeftToRight)
                control.ImgArrow.Margin = new Thickness(0, 0, ((double)newValue) * 10 / 169.0, 0);
            else
                control.ImgArrow.Margin = new Thickness(((double)newValue) * 10 / 169.0, 0, 0, 0);
        }
        #endregion
        #region RaduisMarginProperty
        public static BindableProperty RaduisMarginProperty = BindableProperty.Create(
                                                propertyName: "RaduisMargin",
                                                returnType: typeof(int),
                                                declaringType: typeof(GradientPicker),
                                                defaultValue: 0,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: RaduisMarginPropertyChanged);

        public int RaduisMargin
        {
            get { return (int)base.GetValue(RaduisMarginProperty); }
            set { base.SetValue(RaduisMarginProperty, value); }
        }

        private static void RaduisMarginPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (GradientPicker)bindable;
            if (control == null) return;
            control.OnPropertyChanged(nameof(control.RaduisMargin));
            control.FrameContainer.CornerRadius = int.Parse(newValue.ToString());
        }

        #endregion
        #region ItemSourceProperty
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(GradientPicker), Enumerable.Empty<object>(), BindingMode.OneWay, propertyChanged: OnItemsSourceChanged);

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (bindable as GradientPicker);
            var picker = control.PickerChoice;
            if (picker != null)
            {
                if (picker.ItemsSource == null)
                    picker.Items.Clear();
                if (newValue == null) return;
                picker.ItemsSource = (IList)newValue;
                if (!string.IsNullOrEmpty(control.DisplayMember))
                {
                    picker.ItemDisplayBinding = new Binding(control.DisplayMember);
                }
            }
        }

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public string DisplayMember { get; set; }
        #endregion
        #region EventHandlerItemSelect
        // This is the event handler that will be set from your XAML
        public event EventHandler SelectItem;
        // Called every time the image is clicked
        public void OnSelectItem(object sender, EventArgs e)
        {
            if (SelectItem != null)
            {
                // Call the custom event handler (assuming one has been set)
                //
                // You could always subclass EventArgs to send something more useful than
                // EventArgs.Empty here but more often than not that's not necessary
                this.SelectItem(this, EventArgs.Empty);
            }
        }
        #endregion
        #region ItemSelectProprty

        public static BindableProperty SelectedItemProperty = BindableProperty.Create(
                                        propertyName: "SelectedItem",
                                        returnType: typeof(object),
                                        declaringType: typeof(GradientPicker),
                                        defaultValue: null,
                                        defaultBindingMode: BindingMode.TwoWay,
                                        propertyChanged: OnSelectedItemPropertyChanged);
        public object SelectedItem
        {
            get { return this.PickerChoice.SelectedItem; }
            set { base.SetValue(SelectedItemProperty, value); }
        }
        private static void OnSelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (GradientPicker)bindable;
            if (control == null) return;
            control.OnPropertyChanged(nameof(control.SelectedItem));
        }

        #endregion
        #region ImgArrowProprty
        public static BindableProperty ImageArrowProprty = BindableProperty.Create(
                                                propertyName: "ImageArrow",
                                                returnType: typeof(string),
                                                declaringType: typeof(GradientPicker),
                                                defaultValue: string.Empty,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: ImgArrowProprtyChanged);
        public string ImageArrow
        {
            get { return base.GetValue(ImageArrowProprty).ToString(); }
            set { base.SetValue(ImageArrowProprty, value); }
        }

        private static void ImgArrowProprtyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (GradientPicker)bindable;
            if (control == null) return;
            control.OnPropertyChanged(nameof(control.ImageArrow));
            if ((string)newValue != null)
                control.ImgArrow.Source = newValue.ToString();
        }
        #endregion
        #region TitlePickerChoice
        public string TitlePickerChoice
        {
            get { return (string)this.PickerChoice.Title; }
            set { this.PickerChoice.Title = value; }
        }
        #endregion
        #region SizeFontButtonProperty
        public static BindableProperty SizeFontButtonProperty = BindableProperty.Create(
                                                propertyName: "SizeFontButton",
                                                returnType: typeof(double),
                                                declaringType: typeof(GradientPicker),
                                                defaultValue: Font.Default.FontSize,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: SizeFontButtonPropertyChanged);

        public double SizeFontButton
        {
            get { return (double)base.GetValue(SizeFontButtonProperty); }
            set { base.SetValue(SizeFontButtonProperty, value); }
        }

        private static void SizeFontButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (GradientPicker)bindable;
            if (control == null) return;
            control.OnPropertyChanged(nameof(control.SizeFontButton));
            control.PickerChoice.FontSize = (double)newValue;
        }
        #endregion
        #region FamilyFontButtonProperty
        public static BindableProperty FamilyFontButtonProperty = BindableProperty.Create(
                                                propertyName: "FamilyFontButton",
                                                returnType: typeof(string),
                                                declaringType: typeof(GradientPicker),
                                                defaultValue: Font.Default.FontFamily,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: FamilyFontButtonPropertyChanged);

        public string FamilyFontButton
        {
            get { return (string)base.GetValue(FamilyFontButtonProperty); }
            set { base.SetValue(FamilyFontButtonProperty, value); }
        }

        private static void FamilyFontButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (GradientPicker)bindable;
            if (control == null) return;
            control.OnPropertyChanged(nameof(control.FamilyFontButton));
            if ((string)newValue != null)
                control.PickerChoice.FontFamily = (string)newValue;
        }
        #endregion
        #region FontAttributesButtonProperty
        public static BindableProperty FontAttributesButtonProperty = BindableProperty.Create(
                                                propertyName: "FontAttributesButton",
                                                returnType: typeof(FontAttributes),
                                                declaringType: typeof(GradientPicker),
                                                defaultValue: Font.Default.FontAttributes,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: FontAttributesButtonPropertyChanged);

        public FontAttributes FontAttributesButton
        {
            get { return (FontAttributes)base.GetValue(FontAttributesButtonProperty); }
            set { base.SetValue(FontAttributesButtonProperty, value); }
        }

        private static void FontAttributesButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (GradientPicker)bindable;
            if (control == null) return;
            control.OnPropertyChanged(nameof(control.FontAttributesButton));
                control.PickerChoice.FontAttributes = (FontAttributes)newValue;
        }
        #endregion
        #region TextColorButtonProperty
        public static BindableProperty TextColorButtonProperty = BindableProperty.Create(
                                                propertyName: "TextColorButton",
                                                returnType: typeof(Color),
                                                declaringType: typeof(GradientPicker),
                                                defaultValue: Color.Black,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: TextColorButtonPropertyChanged);

        public Color TextColorButton
        {
            get { return (Color)base.GetValue(TextColorButtonProperty); }
            set { base.SetValue(TextColorButtonProperty, value); }
        }

        private static void TextColorButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (GradientPicker)bindable;
            if (control == null) return;
            control.OnPropertyChanged(nameof(control.TextColorButton));
            if ((Color)newValue != null)
                control.PickerChoice.TextColor = (Color)newValue;
        }
        #endregion
        #region IsEnableGPicker
        public static BindableProperty IsEnableGPickerProperty = BindableProperty.Create(
                                                propertyName: "IsEnableGPicker",
                                                returnType: typeof(bool),
                                                declaringType: typeof(GradientPicker),
                                                defaultValue: true,
                                                defaultBindingMode: BindingMode.OneWay,
                                                propertyChanged: OnIsEnableGPickerPropertyChanged);
        public bool IsEnableGPicker
        {
            get { return (bool)base.GetValue(IsEnableGPickerProperty); }
            set { base.SetValue(IsEnableGPickerProperty, value);}
        }
        private static void OnIsEnableGPickerPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (GradientPicker)bindable;
            if (control == null) return;
            control.OnPropertyChanged(nameof(control.IsEnableGPicker));
            control.GradientBox.IsVisible = (bool)newValue;
            control.PickerChoice.IsEnabled = (bool)newValue;
            control.StackLayout.BackgroundColor = (bool)newValue ? Color.White : Color.FromHex("#E5ECED");
        }
        #endregion
        public GradientPicker()
        {
            InitializeComponent();
        }

        private void PickerChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItem = this.PickerChoice.SelectedItem;
        }
    }
}