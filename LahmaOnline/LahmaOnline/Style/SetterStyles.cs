using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LahmaOnline.StyleViews.Styles
{
    public static class SetterStyle
    {
        public static Setter ViewFlowDirection = new Setter
        {
            Property = VisualElement.FlowDirectionProperty, Value = FlowDirection.LeftToRight
        };

        #region HeightAspectRatio
        static double? _HeightAspectRatio = null;
        public static double? HeightAspectRatio
        {
            get
            {
                if (_HeightAspectRatio == null)
                    _HeightAspectRatio = App.ScreenHeight / (double)App.HightScreenUseInDesign;
                return _HeightAspectRatio;
            }
        }

        public static double Maintain_HeightAspectRatio(double value)
        {
            return value * HeightAspectRatio.Value;
        }
        #endregion
        #region WidthAspectRatio
        static double? _WidthAspectRatio = null;
        public static double? WidthAspectRatio
        {
            get
            {
                if (_WidthAspectRatio == null)
                    _WidthAspectRatio = App.ScreenWidth / (double)App.WidthScreenUseInDesign;
                return _WidthAspectRatio;
            }
        }
        public static double Maintain_WidthAspectRatio(double value)
        {
            return value * WidthAspectRatio.Value;
        }
        #endregion

        public static Setter GetMargin(double size)
        {
            return new Setter { Property = View.MarginProperty, Value = new Thickness(Maintain_HeightAspectRatio(size)) };
        }
        public static Setter GetMargin(double left = 0, double top = 0, double right = 0, double bottom = 0)
        {
            return new Setter { Property = View.MarginProperty, Value = new Thickness(Maintain_WidthAspectRatio(left), Maintain_HeightAspectRatio(top), Maintain_WidthAspectRatio(right), Maintain_HeightAspectRatio(bottom)) };
        }
        public static Setter GetMargin(double Horizontal = 0, double Vertical = 0)
        {
            return new Setter { Property = View.MarginProperty, Value = new Thickness(Maintain_WidthAspectRatio(Horizontal), Maintain_HeightAspectRatio(Vertical)) };
        }

        public static Setter GetPadding(double size)
        {
            return new Setter { Property = Layout.PaddingProperty, Value = new Thickness(Maintain_HeightAspectRatio(size)) };
        }
        public static Setter GetPadding(double left = 0, double top = 0, double right = 0, double bottom = 0)
        {
            return new Setter { Property = Layout.PaddingProperty, Value = new Thickness(Maintain_WidthAspectRatio(left), Maintain_HeightAspectRatio(top), Maintain_WidthAspectRatio(right), Maintain_HeightAspectRatio(bottom)) };
        }
        public static Setter GetPadding(double Horizontal = 0, double Vertical = 0)
        {
            return new Setter { Property = Layout.PaddingProperty, Value = new Thickness(Maintain_WidthAspectRatio(Horizontal), Maintain_HeightAspectRatio(Vertical)) };
        }

        #region Opacity
        public static Setter GetOpacity(double value = 0)
        {
            return new Setter { Property = VisualElement.OpacityProperty, Value = value };
        }
        #endregion
        #region Setter Style Font
        public static class Font
        {
            public static class _Label
            {
                public static class FontSize
                {
                    public static Setter GetFontSize(double size = 0)
                    {
                        return new Setter { Property = Label.FontSizeProperty, Value = Maintain_HeightAspectRatio(size) };
                    }
                }
                public static class FontAttribute
                {
                    public static readonly Setter Bold = new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold };
                    public static readonly Setter Italic = new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Italic };

                }
                public static class FontColor
                {
                    public static Setter GetColorValue(string hexCode)
                    {
                        return new Setter() { Property = Label.TextColorProperty, Value = Color.FromHex(hexCode) };
                    }
                }
                public static class FontAlignmentHorizontal
                {
                    public static readonly Setter Start = new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Start };
                    public static readonly Setter Crenter = new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center };
                    public static readonly Setter End = new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.End };
                }
                public static class FontAlignmentVertical
                {
                    public static readonly Setter Start = new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Start };
                    public static readonly Setter Crenter = new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center };
                    public static readonly Setter End = new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.End };
                }
                public static class FontName
                {
                    public static readonly Setter Avenir900_Heavy =
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "Avenir-Heavy.ttf#Avenir Heavy" : "Avenir Heavy"
                        };
                    public static readonly Setter SourceSansPro_Black =
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-Black.ttf#SourceSansPro Black" : "SourceSansPro Black"
                        };
                    public static readonly Setter SourceSansPro_Bold =
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-Bold.ttf#SourceSansPro Bold" : "SourceSansPro Bold"
                        };
                    public static readonly Setter SourceSansPro_Regular =
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-Regular.ttf#SourceSansPro Regular" : "SourceSansPro Regular"
                        };
                    public static readonly Setter SourceSansPro_SemiBold =
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-SemiBold.ttf#Source Sans Pro SemiBold" : "Source Sans Pro SemiBold"
                        };
                    public static readonly Setter Renner500_Medium =
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "renner-medium.otf#Renner* Medium" : "Renner* Medium"
                        };
                    public static readonly Setter Renner400_Book =
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "renner-book.otf#Renner*" : "Renner*"
                        };
                    public static readonly Setter Renner900_Black =
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "renner-black.otf#Renner* Black" : "Renner* Black"
                        };
                    public static readonly Setter HelveticaNeue400_Black =
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "HelveticaNeue-Black.otf#Helvetica Neue" : "Helvetica Neue"
                        };
                    public static readonly Setter HelveticaNeue400_Regular =
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "HelveticaNeue-Medium.otf#Helvetica Neue" : "Helvetica Neue"
                        };
                    public static readonly Setter Montserrat700_Bold =
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "Montserrat-Bold.otf#Montserrat Bold" : "Montserrat Bold"
                        };
                    public static readonly Setter Montserrat400_Regular =
                        new Setter
                        {
                            Property = Label.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "Montserrat-Regular.otf#Montserrat Regular" : "Montserrat Regular"
                        };
                    public static readonly Setter Avenir900_Black =
                         new Setter
                         {
                             Property = Label.FontFamilyProperty,
                             Value = Device.RuntimePlatform == Device.Android ? "AvenirLTStd-Black.otf#AvenirLTStd Black" : "AvenirLTStd Black"
                         };
                    public static readonly Setter Avenir500_Medium =
                         new Setter
                         {
                             Property = Label.FontFamilyProperty,
                             Value = Device.RuntimePlatform == Device.Android ? "AvenirLTStd-Medium.otf#AvenirLTStd Medium" : "AvenirLTStd Medium"
                         };
                    public static readonly Setter LucidaGrande =
                         new Setter
                         {
                             Property = Label.FontFamilyProperty,
                             Value = Device.RuntimePlatform == Device.Android ? "LucidaGrande.ttf#LucidaGrande" : "LucidaGrande"
                         };
                    public static readonly Setter PTSerif =
                         new Setter
                         {
                             Property = Label.FontFamilyProperty,
                             Value = Device.RuntimePlatform == Device.Android ? "PT_Serif-Web-Regular.ttf#LucidaGrande" : "PT_Serif Web Regular"
                         };
                }
            }
            public static class _Button
            {
                public static class FontSize
                {
                    public static Setter GetFontSize(double size = 0)
                    {
                        return new Setter { Property = Button.FontSizeProperty, Value = Maintain_HeightAspectRatio(size) };
                    }
                }
                public static class FontAttribute
                {
                    public static readonly Setter Bold = new Setter { Property = Button.FontAttributesProperty, Value = FontAttributes.Bold };
                    public static readonly Setter Italic = new Setter { Property = Button.FontAttributesProperty, Value = FontAttributes.Italic };
                }
                public static class FontColor
                {
                    public static Setter GetColorValue(string hexCode)
                    {
                        return new Setter() { Property = Button.TextColorProperty, Value = Color.FromHex(hexCode) };
                    }
                }
                public static class FontName
                {
                    public static readonly Setter SourceSansPro_Bold =
                        new Setter
                        {
                            Property = Button.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-Bold.ttf#SourceSansPro Bold" : "SourceSansPro Bold"
                        };

                    public static readonly Setter SourceSansPro_Regular =
                        new Setter
                        {
                            Property = Button.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-Regular.ttf#SourceSansPro Regular" : "SourceSansPro Regular"
                        };
                    public static readonly Setter SourceSansPro_SemiBold =
                        new Setter
                        {
                            Property = Button.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-SemiBold.ttf#Source Sans Pro SemiBold" : "Source Sans Pro SemiBold"
                        };

                    public static readonly Setter Renner500_Medium =
                        new Setter
                        {
                            Property = Button.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "renner-medium.otf#Renner* Medium" : "Renner* Medium"
                        };
                    public static readonly Setter Renner400_Book =
                        new Setter
                        {
                            Property = Button.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "renner-book.otf#Renner*" : "Renner*"
                        };
                    public static readonly Setter Renner900_Black =
                        new Setter
                        {
                            Property = Button.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "renner-black.otf#Renner* Black" : "Renner* Black"
                        };
                    public static readonly Setter HelveticaNeue400_Black =
                        new Setter
                        {
                            Property = Button.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "HelveticaNeue-Black.otf#Helvetica Neue" : "Helvetica Neue"
                        };
                    public static readonly Setter Montserrat400_Regular =
                        new Setter
                        {
                            Property = Button.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "Montserrat-Regular.otf#Montserrat Regular" : "Montserrat Bold"
                        };
                    public static readonly Setter Montserrat700_Bold =
                        new Setter
                        {
                            Property = Button.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "Montserrat-Bold.otf#Montserrat Bold" : "Montserrat Bold"
                        };
                    public static readonly Setter Avenir900_Heavy =
                        new Setter
                        {
                            Property = Button.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "Avenir-Heavy.ttf#Avenir Heavy" : "Avenir Heavy"
                        };
                    public static readonly Setter Avenir900_Black =
                         new Setter
                         {
                             Property = Button.FontFamilyProperty,
                             Value = Device.RuntimePlatform == Device.Android ? "AvenirLTStd-Black.otf#AvenirLTStd Black" : "AvenirLTStd Black"
                         };
                    public static readonly Setter Avenir500_Medium =
                         new Setter
                         {
                             Property = Button.FontFamilyProperty,
                             Value = Device.RuntimePlatform == Device.Android ? "AvenirLTStd-Medium.otf#AvenirLTStd Medium" : "AvenirLTStd Medium"
                         };
                    public static readonly Setter LucidaGrande =
                     new Setter
                     {
                         Property = Label.FontFamilyProperty,
                         Value = Device.RuntimePlatform == Device.Android ? "LucidaGrande.ttf#LucidaGrande" : "LucidaGrande"
                     };

                }
            }
            public static class _Entry
            {
                public static class FontSize
                {
                    public static Setter GetFontSize(double size = 0)
                    {
                        return new Setter { Property = Entry.FontSizeProperty, Value = Maintain_WidthAspectRatio(size) };
                    }
                }
                public static class FontAttribute
                {
                    public static readonly Setter Bold = new Setter { Property = Entry.FontAttributesProperty, Value = FontAttributes.Bold };
                    public static readonly Setter Italic = new Setter { Property = Entry.FontAttributesProperty, Value = FontAttributes.Italic };
                }
                public class FontAlignmentHorizontal
                {
                    public static readonly Setter Start = new Setter { Property = Entry.HorizontalTextAlignmentProperty, Value = TextAlignment.Start };
                    public static readonly Setter Crenter = new Setter { Property = Entry.HorizontalTextAlignmentProperty, Value = TextAlignment.Center };
                    public static readonly Setter End = new Setter { Property = Entry.HorizontalTextAlignmentProperty, Value = TextAlignment.End };
                }
                public class PlaceholderColor
                {
                    public static Setter GetColorValue(string hexCode)
                    {
                        return new Setter() { Property = Entry.PlaceholderColorProperty, Value = Color.FromHex(hexCode) };
                    }

                    public static readonly Setter HEX_333333 = new Setter { Property = Entry.PlaceholderColorProperty, Value = Color.FromHex("#333333") };
                }
                public static class FontColor
                {
                    public static Setter GetColorValue(string hexCode)
                    {
                        return new Setter() { Property = Label.TextColorProperty, Value = Color.FromHex(hexCode) };
                    }
                }

                public static class FontName
                {
                    public static readonly Setter SourceSansPro_Black =
                        new Setter
                        {
                            Property = Entry.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-Black.ttf#SourceSansPro Black" : "SourceSansPro Black"
                        };
                    public static readonly Setter SourceSansPro_Bold =
                        new Setter
                        {
                            Property = Entry.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-Bold.ttf#SourceSansPro Bold" : "SourceSansPro Bold"
                        };
                    public static readonly Setter SourceSansPro_Regular =
                        new Setter
                        {
                            Property = Entry.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-Regular.ttf#SourceSansPro Regular" : "SourceSansPro Regular"
                        };
                    public static readonly Setter SourceSansPro_SemiBold =
                        new Setter
                        {
                            Property = Entry.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-SemiBold.ttf#Source Sans Pro SemiBold" : "Source Sans Pro SemiBold"
                        };

                    public static readonly Setter RennerMedium =
                        new Setter
                        {
                            Property = Entry.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "renner-medium.otf#Renner* Medium" : "Renner* Medium"
                        };
                    public static readonly Setter RennerBook =
                        new Setter
                        {
                            Property = Entry.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "renner-book.otf#Renner*" : "Renner*"
                        };
                    public static readonly Setter RennerBlack =
                        new Setter
                        {
                            Property = Entry.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "renner-black.otf#Renner* Black" : "Renner* Black"
                        };
                    public static readonly Setter RennerNeue =
                        new Setter
                        {
                            Property = Entry.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "HelveticaNeue-Black.otf#Helvetica Neue" : "Helvetica Neue"
                        };
                    public static readonly Setter HelveticaNeue =
                        new Setter
                        {
                            Property = Entry.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "HelveticaNeue-Black.otf#Helvetica Neue" : "Helvetica Neue"
                        };
                    public static readonly Setter HelveticaNeue400_Regular =
                        new Setter
                        {
                            Property = Button.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "HelveticaNeue-Medium.otf#Helvetica Neue" : "Helvetica Neue"
                        };

                    public static readonly Setter MontserratRegular =
                        new Setter
                        {
                            Property = Entry.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "Montserrat-Regular.otf#Montserrat Regular" : "Montserrat Bold"
                        };
                }
            }
            public static class _Span
            {
                public static class FontSize
                {
                    public static Setter GetFontSize(double size = 0)
                    {
                        return new Setter { Property = Span.FontSizeProperty, Value = Maintain_HeightAspectRatio(size) };
                    }
                }
                public static class FontAttribute
                {
                    public static readonly Setter Bold = new Setter { Property = Span.FontAttributesProperty, Value = FontAttributes.Bold };
                    public static readonly Setter Italic = new Setter { Property = Span.FontAttributesProperty, Value = FontAttributes.Italic };

                }
                public static class FontColor
                {
                    public static Setter GetColorValue(string hexCode)
                    {
                        return new Setter() { Property = Span.TextColorProperty, Value = Color.FromHex(hexCode) };
                    }
                }
                public static class FontName
                {
                    public static readonly Setter Avenir900_Heavy =
                        new Setter
                        {
                            Property = Span.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "Avenir-Heavy.ttf#Avenir Heavy" : "Avenir Heavy"
                        };
                    public static readonly Setter SourceSansPro_Bold =
                        new Setter
                        {
                            Property = Span.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-Bold.ttf#SourceSansPro Bold" : "SourceSansPro Bold"
                        };

                    public static readonly Setter SourceSansPro_Regular =
                        new Setter
                        {
                            Property = Span.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-Regular.ttf#SourceSansPro Regular" : "SourceSansPro Regular"
                        };
                    public static readonly Setter SourceSansPro_SemiBold =
                        new Setter
                        {
                            Property = Span.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-SemiBold.ttf#Source Sans Pro SemiBold" : "Source Sans Pro SemiBold"
                        };
                    public static readonly Setter Renner500_Medium =
                        new Setter
                        {
                            Property = Span.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "renner-medium.otf#Renner* Medium" : "Renner* Medium"
                        };
                    public static readonly Setter Renner400_Book =
                        new Setter
                        {
                            Property = Span.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "renner-book.otf#Renner*" : "Renner*"
                        };
                    public static readonly Setter Renner900_Black =
                        new Setter
                        {
                            Property = Span.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "renner-black.otf#Renner* Black" : "Renner* Black"
                        };
                    public static readonly Setter HelveticaNeue400_Black =
                        new Setter
                        {
                            Property = Span.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "HelveticaNeue-Black.otf#Helvetica Neue" : "Helvetica Neue"
                        };
                    public static readonly Setter HelveticaNeue400_Regular =
                        new Setter
                        {
                            Property = Span.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "HelveticaNeue-Medium.otf#Helvetica Neue" : "Helvetica Neue"
                        };

                    public static readonly Setter Montserrat400_Regular =
                        new Setter
                        {
                            Property = Span.FontFamilyProperty,
                            Value = Device.RuntimePlatform == Device.Android ? "Montserrat-Regular.otf#Montserrat Regular" : "Montserrat Bold"
                        };
                    public static readonly Setter Avenir900_Black =
                         new Setter
                         {
                             Property = Span.FontFamilyProperty,
                             Value = Device.RuntimePlatform == Device.Android ? "AvenirLTStd-Black.otf#AvenirLTStd Black" : "AvenirLTStd Black"
                         };
                }
            }
        }
        #endregion

        #region Setter Style Horizontal Option
        public static readonly Setter HorizontalOptionStart = new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Start };
        public static readonly Setter HorizontalOptionStartAndExpand = new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.StartAndExpand };

        public static readonly Setter HorizontalOptionCenter = new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Center };
        public static readonly Setter HorizontalOptionCenterAndExpand = new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.CenterAndExpand };

        public static readonly Setter HorizontalOptionEnd = new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.End };
        public static readonly Setter HorizontalOptionEndAndExpand = new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.EndAndExpand };

        public static readonly Setter HorizontalOptionFill = new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.Fill };
        public static readonly Setter HorizontalOptionFillAndExpand = new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand };

        #endregion
        #region Setter Style Vertical Option
        public static readonly Setter VerticalOptionStart = new Setter { Property = View.VerticalOptionsProperty, Value = LayoutOptions.Start };
        public static readonly Setter VerticalOptionStartAndExpand = new Setter { Property = View.VerticalOptionsProperty, Value = LayoutOptions.StartAndExpand };

        public static readonly Setter VerticalOptionCenter = new Setter { Property = View.VerticalOptionsProperty, Value = LayoutOptions.Center };
        public static readonly Setter VerticalOptionCenterAndExpand = new Setter { Property = View.VerticalOptionsProperty, Value = LayoutOptions.CenterAndExpand };

        public static readonly Setter VerticalOptionEnd = new Setter { Property = View.VerticalOptionsProperty, Value = LayoutOptions.End };
        public static readonly Setter VerticalOptionEndAndExpand = new Setter { Property = View.VerticalOptionsProperty, Value = LayoutOptions.EndAndExpand };

        public static readonly Setter VerticalOptionFill = new Setter { Property = View.VerticalOptionsProperty, Value = LayoutOptions.Fill };
        public static readonly Setter VerticalOptionFillAndExpand = new Setter { Property = View.VerticalOptionsProperty, Value = LayoutOptions.FillAndExpand };

        #endregion
        #region Setter Style Background
        public static Setter GetBackgroundColorValue(string hexCode)
        {
            return new Setter() { Property = VisualElement.BackgroundColorProperty, Value = Color.FromHex(hexCode) };
        }
        #endregion
        #region Setter Style Corner Radius
        public static class CornerRadius
        {
            public static class _Button
            {
                public static Setter GetCornerRadiusValue(int CR)
                {
                    return new Setter() { Property = Button.CornerRadiusProperty, Value = Convert.ToInt32(Maintain_HeightAspectRatio(CR)) };
                }
            }
            public static class _ImageButton
            {
                public static Setter GetCornerRadiusValue(int CR)
                {
                    return new Setter() { Property = ImageButton.CornerRadiusProperty, Value = Convert.ToInt32(Maintain_HeightAspectRatio(CR)) };
                }
            }

            public static class _Frame
            {
                public static Setter GetCornerRadiusValue(int CR)
                {
                    return new Setter() { Property = Frame.CornerRadiusProperty, Value = Convert.ToInt32(Maintain_HeightAspectRatio(CR)) };
                }
            }
            public static class _BoxView
            {
                public static Setter GetCornerRadiusValue(int CR)
                {
                    return new Setter() { Property = BoxView.CornerRadiusProperty, Value = GetCornerRadiusValue(CR,CR,CR,CR).Value };
                }
                public static Setter GetCornerRadiusValue(double topLeft, double topRight, double bottomLeft, double bottomRight)
                {
                    topLeft = Maintain_HeightAspectRatio(topLeft);
                    topRight = Maintain_HeightAspectRatio(topRight);
                    bottomLeft = Maintain_HeightAspectRatio(bottomLeft);
                    bottomRight = Maintain_HeightAspectRatio(bottomRight);
                    return new Setter() { Property = BoxView.CornerRadiusProperty, Value = new Xamarin.Forms.CornerRadius(topLeft, topRight, bottomLeft, bottomRight) };
                }
            }
        }
        #endregion
        #region Setter Style Height Request
        public static Setter GetHeightRequest(double height = 0)
        {
            return new Setter { Property = VisualElement.HeightRequestProperty, Value = Maintain_HeightAspectRatio(height) };
        }

        #endregion
        #region Setter Style Width Request
        public static Setter GetWidthRequest(double width = 0)
        {
            return new Setter { Property = VisualElement.WidthRequestProperty, Value = Maintain_WidthAspectRatio(width) };
        }
        #endregion
        #region Setter Style Borde Color
        public static class BorderColor
        {
            public static class _Frame
            {
                public static Setter GetColorValue(string hexCode)
                {
                    return new Setter() { Property = Frame.BorderColorProperty, Value = Color.FromHex(hexCode) };
                }
            }
            public static class _Button
            {
                public static Setter GetColorValue(string hexCode)
                {
                    return new Setter() { Property = Button.BorderColorProperty, Value = Color.FromHex(hexCode) };
                }
            }

        }

        #endregion
        #region Setter Style RowSpacing
        public static Setter GetRowSpacing(double space = 0)
        {
            return new Setter { Property = Grid.RowSpacingProperty, Value = Maintain_HeightAspectRatio(space) };
        }
        public static Setter GetColumnSpacing(double space = 0)
        {
            return new Setter { Property = Grid.ColumnSpacingProperty, Value = Maintain_WidthAspectRatio(space) };
        }
        public static Setter GetStackLayoutSpacing(double space = 0)
        {
            return new Setter { Property = StackLayout.SpacingProperty, Value = Maintain_WidthAspectRatio(space) };
        }
        #endregion
    }
}