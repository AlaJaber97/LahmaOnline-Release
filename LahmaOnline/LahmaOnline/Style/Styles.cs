
using Xamarin.Forms;
using CornerRadiusB = LahmaOnline.StyleViews.Styles.SetterStyle.CornerRadius._Button;
using CornerRadiusBX = LahmaOnline.StyleViews.Styles.SetterStyle.CornerRadius._BoxView;
using CornerRadiusF = LahmaOnline.StyleViews.Styles.SetterStyle.CornerRadius._Frame;
using FontB = LahmaOnline.StyleViews.Styles.SetterStyle.Font._Button;
using FontE = LahmaOnline.StyleViews.Styles.SetterStyle.Font._Entry;
using FontL = LahmaOnline.StyleViews.Styles.SetterStyle.Font._Label;
using FontS = LahmaOnline.StyleViews.Styles.SetterStyle.Font._Span;
using SStyle = LahmaOnline.StyleViews.Styles.SetterStyle;

namespace LahmaOnline.StyleViews.Styles
{
    public static class GeneralStyles
    {
        private static Style _PrimaryButtonStyle;
        public static Style PrimaryButtonStyle
        {
            get
            {
                if (_PrimaryButtonStyle == null)
                {
                    _PrimaryButtonStyle = new Style(typeof(Button))
                    {
                        Setters =
                        {
                            SStyle.GetBackgroundColorValue("#3C3934"),
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(50)},
                            SStyle.HorizontalOptionFillAndExpand,
                            SStyle.GetMargin(20),
                            CornerRadiusB.GetCornerRadiusValue(10),
                            FontB.FontColor.GetColorValue("#ffffff"),
                            FontB.FontSize.GetFontSize(14),
                            FontB.FontAttribute.Bold,
                            FontB.FontName.Renner900_Black,
                        }
                    };
                }
                return _PrimaryButtonStyle;
            }
        }

        private static Style _ViewFlowDirection;
        public static Style ViewFlowDirection
        {
            get
            {
                if (_ViewFlowDirection == null)
                {
                    _ViewFlowDirection = new Style(typeof(Layout))
                    {
                        Setters =
                        {
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _ViewFlowDirection;
            }
        }
        private static Style _GridPadding20;
        public static Style GridPadding20
        {
            get
            {
                if (_GridPadding20 == null)
                {
                    _GridPadding20 = new Style(typeof(Grid))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(20)
                        }
                    };
                }
                return _GridPadding20;
            }
        }
        private static Style _BottomLine;
        public static Style BottomLine
        {
            get
            {
                if (_BottomLine == null)
                {
                    _BottomLine = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetBackgroundColorValue("#3A1D0F"),
                            SStyle.GetWidthRequest(120),
                            SStyle.GetHeightRequest(4),
                            SStyle.VerticalOptionEnd,
                            SStyle.HorizontalOptionCenter
                        }
                    };
                }
                return _BottomLine;
            }
        }
        private static Style _LabelTapTitle;
        public static Style LabelTapTitle
        {
            get
            {
                if (_LabelTapTitle == null)
                {
                    _LabelTapTitle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontSize.GetFontSize(20),
                            FontL.FontName.LucidaGrande,
                            FontL.FontColor.GetColorValue("#818BA3"),
                            SStyle.VerticalOptionCenter,
                            SStyle.HorizontalOptionCenter
                        }
                    };
                }
                return _LabelTapTitle;
            }
        }
        private static Style _GridPaddingBottom30;
        public static Style GridPaddingBottom30
        {
            get
            {
                if (_GridPaddingBottom30 == null)
                {
                    _GridPaddingBottom30 = new Style(typeof(Grid))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(bottom:85)
                        }
                    };
                }
                return _GridPaddingBottom30;
            }
        }
        #region Image Size
        private static Style _ActivityIndicator30X30;
        public static Style ActivityIndicator30X30
        {
            get
            {
                if (_ActivityIndicator30X30 == null)
                {
                    _ActivityIndicator30X30 = new Style(typeof(ActivityIndicator))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(30)},
                            SStyle.GetWidthRequest(30),
                        }
                    };
                }
                return _ActivityIndicator30X30;
            }
        }
        private static Style _ActivityIndicator120X120;
        public static Style ActivityIndicator120X120
        {
            get
            {
                if (_ActivityIndicator120X120 == null)
                {
                    _ActivityIndicator120X120 = new Style(typeof(ActivityIndicator))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(120)},
                            SStyle.GetWidthRequest(120),
                        }
                    };
                }
                return _ActivityIndicator120X120;
            }
        }
        private static Style _Image50X50Style;
        public static Style Image50X50Style
        {
            get
            {
                if (_Image50X50Style == null)
                {
                    _Image50X50Style = new Style(typeof(Image))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(120)},
                            SStyle.GetWidthRequest(120),
                        }
                    };
                }
                return _Image50X50Style;
            }
        }
        private static Style _Image35X35Style;
        public static Style Image35X35Style
        {
            get
            {
                if (_Image35X35Style == null)
                {
                    _Image35X35Style = new Style(typeof(Image))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(35)},
                            SStyle.GetWidthRequest(35),
                            //SStyle.GetMargin(0),
                        }
                    };
                }
                return _Image35X35Style;
            }
        }
        private static Style _Image25X25Style;
        public static Style Image25X25Style
        {
            get
            {
                if (_Image25X25Style == null)
                {
                    _Image25X25Style = new Style(typeof(Image))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(25)},
                            SStyle.GetWidthRequest(25),
                            //SStyle.GetMargin(0),
                        }
                    };
                }
                return _Image25X25Style;
            }
        }
        private static Style _Image20X20Margin10Style;
        public static Style Image20X20Margin10Style
        {
            get
            {
                if (_Image20X20Margin10Style == null)
                {
                    _Image20X20Margin10Style = new Style(typeof(Image))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(20)},
                            SStyle.GetWidthRequest(20),
                            SStyle.GetMargin(10),
                        }
                    };
                }
                return _Image20X20Margin10Style;
            }
        }
        private static Style _Image20X20WithMargin20Style;
        public static Style Image20X20WithMargin20Style
        {
            get
            {
                if (_Image20X20WithMargin20Style == null)
                {
                    _Image20X20WithMargin20Style = new Style(typeof(Image))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(20)},
                            SStyle.GetWidthRequest(20),
                            SStyle.GetMargin(20),
                        }
                    };
                }
                return _Image20X20WithMargin20Style;
            }
        }
        private static Style _BoxImage20X20Style;
        public static Style BoxImage20X20Style
        {
            get
            {
                if (_BoxImage20X20Style == null)
                {
                    _BoxImage20X20Style = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(20)},
                            SStyle.GetWidthRequest(20),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _BoxImage20X20Style;
            }
        }
        private static Style _Image20X20Style;
        public static Style Image20X20Style
        {
            get
            {
                if (_Image20X20Style == null)
                {
                    _Image20X20Style = new Style(typeof(Image))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(20)},
                            SStyle.GetWidthRequest(20),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _Image20X20Style;
            }
        }
        private static Style _Image15X15Style;
        public static Style Image15X15Style
        {
            get
            {
                if (_Image15X15Style == null)
                {
                    _Image15X15Style = new Style(typeof(Image))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(15)},
                            SStyle.GetWidthRequest(15),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _Image15X15Style;
            }
        }
        private static Style _Image250X250Style;
        public static Style Image250X250Style
        {
            get
            {
                if (_Image250X250Style == null)
                {
                    _Image250X250Style = new Style(typeof(Image))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(250)},
                            SStyle.GetWidthRequest(250),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _Image250X250Style;
            }
        }
        private static Style _Image10X10Style;
        public static Style Image10X10Style
        {
            get
            {
                if (_Image10X10Style == null)
                {
                    _Image10X10Style = new Style(typeof(Image))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(10)},
                            SStyle.GetWidthRequest(10),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _Image10X10Style;
            }
        }
        private static Style _FFImage50X50Style;
        public static Style FFImage50X50Style
        {
            get
            {
                if (_FFImage50X50Style == null)
                {
                    _FFImage50X50Style = new Style(typeof(FFImageLoading.Svg.Forms.SvgCachedImage))
                    {
                        Setters =
                        {
                            new Setter{Property = FFImageLoading.Svg.Forms.SvgCachedImage.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(120)},
                            new Setter{Property = FFImageLoading.Svg.Forms.SvgCachedImage.WidthRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(120)},
                        }
                    };
                }
                return _FFImage50X50Style;
            }
        }
        private static Style _FFImage35X35Style;
        public static Style FFImage35X35Style
        {
            get
            {
                if (_FFImage35X35Style == null)
                {
                    _FFImage35X35Style = new Style(typeof(FFImageLoading.Svg.Forms.SvgCachedImage))
                    {
                        Setters =
                        {
                            new Setter{Property = FFImageLoading.Svg.Forms.SvgCachedImage.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(35)},
                            SStyle.GetWidthRequest(35),
                            //SStyle.GetMargin(0),
                        }
                    };
                }
                return _FFImage35X35Style;
            }
        }
        private static Style _FFImage25X25Style;
        public static Style FFImage25X25Style
        {
            get
            {
                if (_FFImage25X25Style == null)
                {
                    _FFImage25X25Style = new Style(typeof(FFImageLoading.Svg.Forms.SvgCachedImage))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(25)},
                            SStyle.GetWidthRequest(25),
                            //SStyle.GetMargin(0),
                        }
                    };
                }
                return _FFImage25X25Style;
            }
        }
        private static Style _FFImage20X20Margin10Style;
        public static Style FFImage20X20Margin10Style
        {
            get
            {
                if (_FFImage20X20Margin10Style == null)
                {
                    _FFImage20X20Margin10Style = new Style(typeof(FFImageLoading.Svg.Forms.SvgCachedImage))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(20)},
                            SStyle.GetWidthRequest(20),
                            SStyle.GetMargin(10),
                        }
                    };
                }
                return _FFImage20X20Margin10Style;
            }
        }
        private static Style _FFImage20X20WithMargin20Style;
        public static Style FFImage20X20WithMargin20Style
        {
            get
            {
                if (_FFImage20X20WithMargin20Style == null)
                {
                    _FFImage20X20WithMargin20Style = new Style(typeof(FFImageLoading.Svg.Forms.SvgCachedImage))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(20)},
                            SStyle.GetWidthRequest(20),
                            SStyle.GetMargin(20),
                        }
                    };
                }
                return _FFImage20X20WithMargin20Style;
            }
        }
        private static Style _FFImage20X20Style;
        public static Style FFImage20X20Style
        {
            get
            {
                if (_FFImage20X20Style == null)
                {
                    _FFImage20X20Style = new Style(typeof(FFImageLoading.Svg.Forms.SvgCachedImage))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(20)},
                            SStyle.GetWidthRequest(20),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _FFImage20X20Style;
            }
        }
        private static Style _FFImage15X15Style;
        public static Style FFImage15X15Style
        {
            get
            {
                if (_FFImage15X15Style == null)
                {
                    _FFImage15X15Style = new Style(typeof(FFImageLoading.Svg.Forms.SvgCachedImage))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(15)},
                            SStyle.GetWidthRequest(15),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _FFImage15X15Style;
            }
        }
        private static Style _FFImage250X250Style;
        public static Style FFImage250X250Style
        {
            get
            {
                if (_FFImage250X250Style == null)
                {
                    _FFImage250X250Style = new Style(typeof(FFImageLoading.Svg.Forms.SvgCachedImage))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(250)},
                            SStyle.GetWidthRequest(250),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _FFImage250X250Style;
            }
        }
        private static Style _FFImage10X10Style;
        public static Style FFImage10X10Style
        {
            get
            {
                if (_FFImage10X10Style == null)
                {
                    _FFImage10X10Style = new Style(typeof(FFImageLoading.Svg.Forms.SvgCachedImage))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(10)},
                            SStyle.GetWidthRequest(10),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _FFImage10X10Style;
            }
        }
        #endregion
        #region StackLayout Spacing
        private static Style _StackLayoutWithSpace3_Style;
        public static Style StackLayoutWithSpace3_Style
        {
            get
            {
                if (_StackLayoutWithSpace3_Style == null)
                {
                    _StackLayoutWithSpace3_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(0),
                            SStyle.GetStackLayoutSpacing(3),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithSpace3_Style;
            }
        }
        private static Style _StackLayoutWithSpace5_Style;
        public static Style StackLayoutWithSpace5_Style
        {
            get
            {
                if (_StackLayoutWithSpace5_Style == null)
                {
                    _StackLayoutWithSpace5_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(0),
                            SStyle.GetStackLayoutSpacing(5),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithSpace5_Style;
            }
        }
        private static Style _StackLayoutWithSpace10_Style;
        public static Style StackLayoutWithSpace10_Style
        {
            get
            {
                if (_StackLayoutWithSpace10_Style == null)
                {
                    _StackLayoutWithSpace10_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(0),
                            SStyle.GetStackLayoutSpacing(10),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithSpace10_Style;
            }
        }
        private static Style _StackLayoutWithPadding0x0x0x30_Style;
        public static Style StackLayoutWithPadding0x0x0x30_Style
        {
            get
            {
                if (_StackLayoutWithPadding0x0x0x30_Style == null)
                {
                    _StackLayoutWithPadding0x0x0x30_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(bottom:30),
                            SStyle.GetStackLayoutSpacing(5),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithPadding0x0x0x30_Style;
            }
        }
        private static Style _StackLayoutWithSpace15_Style;
        public static Style StackLayoutWithSpace15_Style
        {
            get
            {
                if (_StackLayoutWithSpace15_Style == null)
                {
                    _StackLayoutWithSpace15_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(0),
                            SStyle.GetStackLayoutSpacing(15),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithSpace15_Style;
            }
        }
        #endregion
        #region Home Page Style
        private static Style _ScrollViewPadding10x10x10x20;
        public static Style ScrollViewPadding10x10x10x20
        {
            get
            {
                if (_ScrollViewPadding10x10x10x20 == null)
                {
                    _ScrollViewPadding10x10x10x20 = new Style(typeof(ScrollView))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionFillAndExpand,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(10,10,10,20),
                        }
                    };
                }
                return _ScrollViewPadding10x10x10x20;
            }
        }
        private static Style _NavBarStyle;
        public static Style NavBarStyle
        {
            get
            {
                if (_NavBarStyle == null)
                {
                    _NavBarStyle = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.GetBackgroundColorValue("#3A1D0F"),
                            SStyle.VerticalOptionStart,
                            SStyle.GetHeightRequest(70),
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(20,0),
                            SStyle.GetStackLayoutSpacing(20),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _NavBarStyle;
            }
        }

        private static Style _LabelTitlePageStyle;
        public static Style LabelTitlePageStyle
        {
            get
            {
                if (_LabelTitlePageStyle == null)
                {
                    _LabelTitlePageStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionCenter,
                            SStyle.HorizontalOptionCenterAndExpand,
                            FontL.FontAlignmentHorizontal.Crenter,
                            FontL.FontAttribute.Bold,
                            FontL.FontName.Montserrat700_Bold,
                            FontL.FontSize.GetFontSize(20),
                            FontL.FontColor.GetColorValue("#FFFFFF"),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _LabelTitlePageStyle;
            }
        }
        private static Style _StackLayoutPageStyle;
        public static Style StackLayoutPageStyle
        {
            get
            {
                if (_StackLayoutPageStyle == null)
                {
                    _StackLayoutPageStyle = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionCenterAndExpand,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(10,10,5,0),
                            SStyle.GetStackLayoutSpacing(10),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutPageStyle;
            }
        }
        private static Style _LabelTitleWigdthsStyle;
        public static Style LabelTitleWigdthsStyle
        {
            get
            {
                if (_LabelTitleWigdthsStyle == null)
                {
                    _LabelTitleWigdthsStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionCenter,
                            SStyle.HorizontalOptionStartAndExpand,
                            FontL.FontAttribute.Bold,
                            FontL.FontName.SourceSansPro_Bold,
                            FontL.FontSize.GetFontSize(20),
                            FontL.FontColor.GetColorValue("#272A3F"),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _LabelTitleWigdthsStyle;
            }
        }
        private static Style _LabelViewAllStyle;
        public static Style LabelViewAllStyle
        {
            get
            {
                if (_LabelViewAllStyle == null)
                {
                    _LabelViewAllStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionCenter,
                            FontL.FontName.SourceSansPro_Regular,
                            FontL.FontSize.GetFontSize(15),
                            FontL.FontColor.GetColorValue("#3A1D0F"),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _LabelViewAllStyle;
            }
        }

        private static Style _CollectionViewHeightRequest120_Style;
        public static Style CollectionViewHeightRequest120_Style
        {
            get
            {
                if (_CollectionViewHeightRequest120_Style == null)
                {
                    _CollectionViewHeightRequest120_Style = new Style(typeof(CollectionView))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetHeightRequest(150),
                            SStyle.GetMargin(0),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _CollectionViewHeightRequest120_Style;
            }
        }
        private static Style _StackLayoutWithSpace10Padding5_Style;
        public static Style StackLayoutWithSpace10Padding5_Style
        {
            get
            {
                if (_StackLayoutWithSpace10Padding5_Style == null)
                {
                    _StackLayoutWithSpace10Padding5_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.HorizontalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(5),
                            SStyle.GetStackLayoutSpacing(10),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithSpace10Padding5_Style;
            }
        }
        private static Style _StackLayoutWithSpace5Padding5_Style;
        public static Style StackLayoutWithSpace5Padding5_Style
        {
            get
            {
                if (_StackLayoutWithSpace5Padding5_Style == null)
                {
                    _StackLayoutWithSpace5Padding5_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.HorizontalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(5),
                            SStyle.GetStackLayoutSpacing(5),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithSpace5Padding5_Style;
            }
        }
        private static Style _StackLayoutWithSpace10Padding15x30_Style;
        public static Style StackLayoutWithSpace10Padding15x30_Style
        {
            get
            {
                if (_StackLayoutWithSpace10Padding15x30_Style == null)
                {
                    _StackLayoutWithSpace10Padding15x30_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.HorizontalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(15,30),
                            SStyle.GetStackLayoutSpacing(10),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithSpace10Padding15x30_Style;
            }
        }
        private static Style _FrameImage80X80Style;
        public static Style FrameImage80X80Style
        {
            get
            {
                if (_FrameImage80X80Style == null)
                {
                    _FrameImage80X80Style = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.GetWidthRequest(80),
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(80)},
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(0),
                            CornerRadiusF.GetCornerRadiusValue(50),
                            SStyle.VerticalOptionStart,
                            SStyle.HorizontalOptionStart,
                        }
                    };
                }
                return _FrameImage80X80Style;
            }
        }
        private static Style _ImageWithMarign10;
        public static Style ImageWithMarign10
        {
            get
            {
                if (_ImageWithMarign10 == null)
                {
                    _ImageWithMarign10 = new Style(typeof(Image))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(10),
                            SStyle.VerticalOptionCenter,
                            SStyle.HorizontalOptionCenter
                        }
                    };
                }
                return _ImageWithMarign10;
            }
        }
        private static Style _LabelNameCategoreStyle;
        public static Style LabelNameCategoreStyle
        {
            get
            {
                if (_LabelNameCategoreStyle == null)
                {
                    _LabelNameCategoreStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontAttribute.Bold,
                            FontL.FontName.SourceSansPro_Bold,
                            FontL.FontSize.GetFontSize(15),
                            SStyle.GetWidthRequest(50),
                            FontL.FontColor.GetColorValue("#272A3F"),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _LabelNameCategoreStyle;
            }
        }
        private static Style _BoxLabel10X50Style;
        public static Style BoxLabel10X50Style
        {
            get
            {
                if (_BoxLabel10X50Style == null)
                {
                    _BoxLabel10X50Style = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetHeightRequest(10),
                            SStyle.GetWidthRequest(50),
                            SStyle.GetMargin(0),
                            SStyle.GetBackgroundColorValue("#CFCCC8"),
                            SStyle.HorizontalOptionStart,
                            SStyle.VerticalOptionCenter,

                        }
                    };
                }
                return _BoxLabel10X50Style;
            }
        }
        private static Style _BoxLabel10X40Style;
        public static Style BoxLabel10X40Style
        {
            get
            {
                if (_BoxLabel10X40Style == null)
                {
                    _BoxLabel10X40Style = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetHeightRequest(10),
                            SStyle.GetWidthRequest(40),
                            SStyle.GetMargin(0),
                            SStyle.GetBackgroundColorValue("#CFCCC8"),
                            SStyle.HorizontalOptionStart,
                            SStyle.VerticalOptionCenter,

                        }
                    };
                }
                return _BoxLabel10X40Style;
            }
        }
        private static Style _CollectionViewHeightRequest210_Style;
        public static Style CollectionViewHeightRequest210_Style
        {
            get
            {
                if (_CollectionViewHeightRequest210_Style == null)
                {
                    _CollectionViewHeightRequest210_Style = new Style(typeof(CollectionView))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetHeightRequest(210),
                            SStyle.GetMargin(0),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _CollectionViewHeightRequest210_Style;
            }
        }

        private static Style _FrameImage110X120Style;
        public static Style FrameImage110X120Style
        {
            get
            {
                if (_FrameImage110X120Style == null)
                {
                    _FrameImage110X120Style = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.GetWidthRequest(110),
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(120)},
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(0),
                            CornerRadiusF.GetCornerRadiusValue(10),

                        }
                    };
                }
                return _FrameImage110X120Style;
            }
        }
        private static Style _LabelNameProductStyle;
        public static Style LabelNameProductStyle
        {
            get
            {
                if (_LabelNameProductStyle == null)
                {
                    _LabelNameProductStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontAttribute.Bold,
                            FontL.FontName.SourceSansPro_Bold,
                            FontL.FontSize.GetFontSize(20),
                            FontL.FontColor.GetColorValue("#272A3F"),
                            SStyle.GetMargin(0),
                            SStyle.GetWidthRequest(105)
                        }
                    };
                }
                return _LabelNameProductStyle;
            }
        }
        private static Style _SpanCurrencyFont14_Style;
        public static Style SpanCurrencyFont14_Style
        {
            get
            {
                if (_SpanCurrencyFont14_Style == null)
                {
                    _SpanCurrencyFont14_Style = new Style(typeof(Span))
                    {
                        Setters =
                        {
                            FontS.FontAttribute.Bold,
                            FontS.FontName.SourceSansPro_Bold,
                            FontS.FontSize.GetFontSize(14),
                        }
                    };
                }
                return _SpanCurrencyFont14_Style;
            }
        }
        private static Style _SpanCategoryAndOriginFont18_Style;
        public static Style SpanCategoryAndOriginFont18_Style
        {
            get
            {
                if (_SpanCategoryAndOriginFont18_Style == null)
                {
                    _SpanCategoryAndOriginFont18_Style = new Style(typeof(Span))
                    {
                        Setters =
                        {
                            FontS.FontAttribute.Bold,
                            FontS.FontName.HelveticaNeue400_Regular,
                            FontS.FontSize.GetFontSize(16),
                        }
                    };
                }
                return _SpanCategoryAndOriginFont18_Style;
            }
        }
        private static Style _SpanPriceFont18_Style;
        public static Style SpanPriceFont18_Style
        {
            get
            {
                if (_SpanPriceFont18_Style == null)
                {
                    _SpanPriceFont18_Style = new Style(typeof(Span))
                    {
                        Setters =
                        {
                            FontS.FontAttribute.Bold,
                            FontS.FontName.SourceSansPro_Bold,
                            FontS.FontSize.GetFontSize(18),
                        }
                    };
                }
                return _SpanPriceFont18_Style;
            }
        }

        private static Style _BoxLabel10X60Style;
        public static Style BoxLabel10X60Style
        {
            get
            {
                if (_BoxLabel10X60Style == null)
                {
                    _BoxLabel10X60Style = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetHeightRequest(10),
                            SStyle.GetWidthRequest(60),
                            SStyle.GetMargin(0),
                            SStyle.GetBackgroundColorValue("#CFCCC8"),
                            SStyle.HorizontalOptionCenter,
                            SStyle.VerticalOptionCenter,

                        }
                    };
                }
                return _BoxLabel10X60Style;
            }
        }
        private static Style _BoxBGproductStyle;
        public static Style BoxBGproductStyle
        {
            get
            {
                if (_BoxBGproductStyle == null)
                {
                    _BoxBGproductStyle = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            CornerRadiusBX.GetCornerRadiusValue(10,10,0,0),
                            SStyle.GetBackgroundColorValue("#FFFFFF"),
                            SStyle.HorizontalOptionFillAndExpand,
                            SStyle.VerticalOptionFillAndExpand,

                        }
                    };
                }
                return _BoxBGproductStyle;
            }
        }
        private static Style _LabelNameProductBoldStyle;
        public static Style LabelNameProductBoldStyle
        {
            get
            {
                if (_LabelNameProductBoldStyle == null)
                {
                    _LabelNameProductBoldStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontAttribute.Bold,
                            FontL.FontName.SourceSansPro_Bold,
                            FontL.FontSize.GetFontSize(25),
                            FontL.FontColor.GetColorValue("#272A3F"),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _LabelNameProductBoldStyle;
            }
        }
        private static Style _SpanCurrencyFont18_Style;
        public static Style SpanCurrencyFont18_Style
        {
            get
            {
                if (_SpanCurrencyFont18_Style == null)
                {
                    _SpanCurrencyFont18_Style = new Style(typeof(Span))
                    {
                        Setters =
                        {
                            FontS.FontAttribute.Bold,
                            FontS.FontName.SourceSansPro_Bold,
                            FontS.FontSize.GetFontSize(18),
                        }
                    };
                }
                return _SpanCurrencyFont18_Style;
            }
        }
        private static Style _SpanTitleFont20_Style;
        public static Style SpanTitleFont20_Style
        {
            get
            {
                if (_SpanTitleFont20_Style == null)
                {
                    _SpanTitleFont20_Style = new Style(typeof(Span))
                    {
                        Setters =
                        {
                            FontS.FontName.SourceSansPro_Regular,
                            FontS.FontSize.GetFontSize(20),
                        }
                    };
                }
                return _SpanTitleFont20_Style;
            }
        }
        private static Style _LabelTitleFont22_Style;
        public static Style LabelTitleFont22_Style
        {
            get
            {
                if (_LabelTitleFont22_Style == null)
                {
                    _LabelTitleFont22_Style = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontName.SourceSansPro_Regular,
                            FontL.FontSize.GetFontSize(22),
                        }
                    };
                }
                return _LabelTitleFont22_Style;
            }
        }
        private static Style _SpanTitleFont22_Style;
        public static Style SpanTitleFont22_Style
        {
            get
            {
                if (_SpanTitleFont22_Style == null)
                {
                    _SpanTitleFont22_Style = new Style(typeof(Span))
                    {
                        Setters =
                        {
                            FontS.FontName.SourceSansPro_Regular,
                            FontS.FontSize.GetFontSize(22),
                        }
                    };
                }
                return _SpanTitleFont22_Style;
            }
        }
        private static Style _SpanPriceFont22_Style;
        public static Style SpanPriceFont22_Style
        {
            get
            {
                if (_SpanPriceFont22_Style == null)
                {
                    _SpanPriceFont22_Style = new Style(typeof(Span))
                    {
                        Setters =
                        {
                            FontS.FontAttribute.Bold,
                            FontS.FontName.SourceSansPro_Bold,
                            FontS.FontSize.GetFontSize(22),
                        }
                    };
                }
                return _SpanPriceFont22_Style;
            }
        }
        private static Style _BoxSparetedLineStyle;
        public static Style BoxSparetedLineStyle
        {
            get
            {
                if (_BoxSparetedLineStyle == null)
                {
                    _BoxSparetedLineStyle = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            SStyle.GetBackgroundColorValue("#4AC2C0BC"),
                            SStyle.GetWidthRequest(10),
                            SStyle.GetHeightRequest(1),

                        }
                    };
                }
                return _BoxSparetedLineStyle;
            }
        }
        private static Style _LabelWeightStyle;
        public static Style LabelWeightStyle
        {
            get
            {
                if (_LabelWeightStyle == null)
                {
                    _LabelWeightStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontAttribute.Bold,
                            FontL.FontName.SourceSansPro_Bold,
                            FontL.FontSize.GetFontSize(20),
                            FontL.FontColor.GetColorValue("#3A1D0F"),
                            SStyle.GetMargin(0),
                            SStyle.VerticalOptionCenter,
                            SStyle.HorizontalOptionStartAndExpand
                        }
                    };
                }
                return _LabelWeightStyle;
            }
        }
        private static Style _ImageButtonMinusPlusStyle;
        public static Style ImageButtonMinusPlusStyle
        {
            get
            {
                if (_ImageButtonMinusPlusStyle == null)
                {
                    _ImageButtonMinusPlusStyle = new Style(typeof(ImageButton))
                    {
                        Setters =
                        {
                            SStyle.GetWidthRequest(30),
                            new Setter{Property = VisualElement.HeightRequestProperty, Value = SetterStyle.Maintain_WidthAspectRatio(30)},
                            new Setter{Property = ImageButton.CornerRadiusProperty, Value = (int)SetterStyle.Maintain_WidthAspectRatio(5)},
                            new Setter{Property = ImageButton.BorderWidthProperty, Value = SetterStyle.Maintain_WidthAspectRatio(1)},
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(10),
                            SStyle.VerticalOptionCenter,
                            SStyle.HorizontalOptionStart,

                        }
                    };
                }
                return _ImageButtonMinusPlusStyle;
            }
        }
        private static Style _EntryWeightFont15Style;
        public static Style EntryWeightFont15Style
        {
            get
            {
                if (_EntryWeightFont15Style == null)
                {
                    _EntryWeightFont15Style = new Style(typeof(Entry))
                    {
                        Setters =
                        {
                            FontE.FontAttribute.Bold,
                            FontE.FontName.SourceSansPro_Bold,
                            FontE.FontAlignmentHorizontal.Crenter,
                            FontE.FontSize.GetFontSize(15),
                            SStyle.GetMargin(0),
                            SStyle.VerticalOptionEnd,
                        }
                    };
                }
                return _EntryWeightFont15Style;
            }
        }
        private static Style _EntryWeightStyle;
        public static Style EntryWeightStyle
        {
            get
            {
                if (_EntryWeightStyle == null)
                {
                    _EntryWeightStyle = new Style(typeof(Entry))
                    {
                        Setters =
                        {
                            FontE.FontAttribute.Bold,
                            FontE.FontName.SourceSansPro_Bold,
                            FontE.FontAlignmentHorizontal.Crenter,
                            FontE.FontSize.GetFontSize(20),
                            SStyle.GetMargin(0),
                            SStyle.VerticalOptionEnd,
                        }
                    };
                }
                return _EntryWeightStyle;
            }
        }
        private static Style _EditorNoteHegith200Style;
        public static Style EditorNoteHegith200Style
        {
            get
            {
                if (_EditorNoteHegith200Style == null)
                {
                    _EditorNoteHegith200Style = new Style(typeof(Editor))
                    {
                        Setters =
                        {
                            FontE.FontName.HelveticaNeue400_Regular,
                            FontE.FontSize.GetFontSize(15),
                            SStyle.GetMargin(4,2,2,2),
                            SStyle.GetHeightRequest(200)
                        }
                    };
                }
                return _EditorNoteHegith200Style;
            }
        }
        private static Style _EditorNoteHegith100Style;
        public static Style EditorNoteHegith100Style
        {
            get
            {
                if (_EditorNoteHegith100Style == null)
                {
                    _EditorNoteHegith100Style = new Style(typeof(Editor))
                    {
                        Setters =
                        {
                            FontE.FontName.HelveticaNeue400_Regular,
                            FontE.FontSize.GetFontSize(15),
                            SStyle.GetMargin(4,2,2,2),
                            SStyle.GetHeightRequest(100)
                        }
                    };
                }
                return _EditorNoteHegith100Style;
            }
        }
        private static Style _EditorNoteStyle;
        public static Style EditorNoteStyle
        {
            get
            {
                if (_EditorNoteStyle == null)
                {
                    _EditorNoteStyle = new Style(typeof(Editor))
                    {
                        Setters =
                        {
                            FontE.FontName.HelveticaNeue400_Regular,
                            FontE.FontSize.GetFontSize(15),
                            SStyle.GetMargin(4,2,2,2),
                        }
                    };
                }
                return _EditorNoteStyle;
            }
        }
        private static Style _ButtonSubmitStyleWithBord;
        public static Style ButtonSubmitStyleWithBord
        {
            get
            {
                if (_ButtonSubmitStyleWithBord == null)
                {
                    _ButtonSubmitStyleWithBord = new Style(typeof(Button))
                    {
                        Setters =
                        {
                            new Setter{Property = Button.BorderWidthProperty, Value = SStyle.Maintain_WidthAspectRatio(1)},
                            FontB.FontColor.GetColorValue("#FFFFFF"),
                            FontB.FontSize.GetFontSize(15),
                            CornerRadiusB.GetCornerRadiusValue(10),
                            SStyle.GetMargin(30,0),
                            SStyle.VerticalOptionStart,
                            SStyle.HorizontalOptionFillAndExpand
                        }
                    };
                }
                return _ButtonSubmitStyleWithBord;
            }
        }
        private static Style _ButtonMoveStyle;
        public static Style ButtonMoveStyle
        {
            get
            {
                if (_ButtonMoveStyle == null)
                {
                    _ButtonMoveStyle = new Style(typeof(Button))
                    {
                        Setters =
                        {
                            FontB.FontColor.GetColorValue("#FFFFFF"),
                            FontB.FontSize.GetFontSize(15),
                            CornerRadiusB.GetCornerRadiusValue(10),
                            SStyle.GetMargin(20,5,20,10),
                            SStyle.VerticalOptionStart,
                            SStyle.HorizontalOptionFillAndExpand
                        }
                    };
                }
                return _ButtonMoveStyle;
            }
        }
        private static Style _ButtonSubmitStyle;
        public static Style ButtonSubmitStyle
        {
            get
            {
                if (_ButtonSubmitStyle == null)
                {
                    _ButtonSubmitStyle = new Style(typeof(Button))
                    {
                        Setters =
                        {
                            FontB.FontColor.GetColorValue("#FFFFFF"),
                            FontB.FontSize.GetFontSize(15),
                            CornerRadiusB.GetCornerRadiusValue(10),
                            SStyle.GetMargin(30,0),
                            SStyle.VerticalOptionStart,
                            SStyle.HorizontalOptionFillAndExpand
                        }
                    };
                }
                return _ButtonSubmitStyle;
            }
        }
        private static Style _GButtonSubmitStyle;
        public static Style GButtonSubmitStyle
        {
            get
            {
                if (_GButtonSubmitStyle == null)
                {
                    _GButtonSubmitStyle = new Style(typeof(Button))
                    {
                        Setters =
                        {
                            FontB.FontColor.GetColorValue("#FFFFFF"),
                            FontB.FontSize.GetFontSize(15),
                            CornerRadiusB.GetCornerRadiusValue(10),
                            SStyle.GetMargin(30,0),
                            SStyle.VerticalOptionStart,
                            SStyle.HorizontalOptionFillAndExpand
                        }
                    };
                }
                return _GButtonSubmitStyle;
            }
        }
        private static Style _GridBottomBarStyle;
        public static Style GridBottomBarStyle
        {
            get
            {
                if (_GridBottomBarStyle == null)
                {
                    _GridBottomBarStyle = new Style(typeof(Grid))
                    {
                        Setters =
                        {
                            SStyle.GetHeightRequest(75),
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(30,5),
                            SStyle.VerticalOptionEnd,
                            SStyle.HorizontalOptionFillAndExpand
                        }
                    };
                }
                return _GridBottomBarStyle;
            }
        }
        private static Style _LabelTitleIconUnActiveBottomBarStyle;
        public static Style LabelTitleIconUnActiveBottomBarStyle
        {
            get
            {
                if (_LabelTitleIconUnActiveBottomBarStyle == null)
                {
                    _LabelTitleIconUnActiveBottomBarStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontName.SourceSansPro_Bold,
                            FontL.FontSize.GetFontSize(12),
                            FontL.FontColor.GetColorValue("#272A3F"),
                            FontL.FontAlignmentHorizontal.Crenter,
                            SStyle.GetMargin(0),
                            new Setter{Property = Label.IsVisibleProperty, Value=false }
                        }
                    };
                }
                return _LabelTitleIconUnActiveBottomBarStyle;
            }
        }
        private static Style _LabelTitleIconActiveBottomBarStyle;
        public static Style LabelTitleIconActiveBottomBarStyle
        {
            get
            {
                if (_LabelTitleIconActiveBottomBarStyle == null)
                {
                    _LabelTitleIconActiveBottomBarStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontAttribute.Bold,
                            FontL.FontName.SourceSansPro_Bold,
                            FontL.FontSize.GetFontSize(15),
                            FontL.FontColor.GetColorValue("#3A1D0F"),
                            FontL.FontAlignmentHorizontal.Crenter,
                            SStyle.GetMargin(0),
                            new Setter{Property = Label.IsVisibleProperty, Value=true }
                        }
                    };
                }
                return _LabelTitleIconActiveBottomBarStyle;
            }
        }

        #endregion
        #region Profile Page Style
        private static Style _BoxViewWidth1;
        public static Style BoxViewWidth1
        {
            get
            {
                if (_BoxViewWidth1 == null)
                {
                    _BoxViewWidth1 = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(5),
                            SStyle.GetWidthRequest(1),
                        }
                    };
                }
                return _BoxViewWidth1;
            }
        }
        private static Style _BoxViewShadowStyle;
        public static Style BoxViewShadowStyle
        {
            get
            {
                if (_BoxViewShadowStyle == null)
                {
                    _BoxViewShadowStyle = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            CornerRadiusBX.GetCornerRadiusValue(0,0,25,25),
                        }
                    };
                }
                return _BoxViewShadowStyle;
            }
        }
        private static Style _BoxViewBGheaderStyle;
        public static Style BoxViewBGheaderStyle
        {
            get
            {
                if (_BoxViewBGheaderStyle == null)
                {
                    _BoxViewBGheaderStyle = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0,0,0,1),
                            CornerRadiusBX.GetCornerRadiusValue(0,0,25,25),
                        }
                    };
                }
                return _BoxViewBGheaderStyle;
            }
        }
        private static Style _StackSpasing25Style;
        public static Style StackSpasing25Style
        {
            get
            {
                if (_StackSpasing25Style == null)
                {
                    _StackSpasing25Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(20,10),
                            SStyle.GetStackLayoutSpacing(25)
                        }
                    };
                }
                return _StackSpasing25Style;
            }
        }
        private static Style _StackHeaderStyle;
        public static Style StackHeaderStyle
        {
            get
            {
                if (_StackHeaderStyle == null)
                {
                    _StackHeaderStyle = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(20,10,20,10),
                            SStyle.GetStackLayoutSpacing(0)
                        }
                    };
                }
                return _StackHeaderStyle;
            }
        }
        private static Style _FrameImage100X100Style;
        public static Style FrameImage100X100Style
        {
            get
            {
                if (_FrameImage100X100Style == null)
                {
                    _FrameImage100X100Style = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.GetWidthRequest(100),
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(100)},
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(0),
                            CornerRadiusF.GetCornerRadiusValue(60),
                            SStyle.VerticalOptionStart,
                            SStyle.HorizontalOptionStart,
                        }
                    };
                }
                return _FrameImage100X100Style;
            }
        }
        private static Style _FrameImage30x30Style;
        public static Style FrameImage30x30Style
        {
            get
            {
                if (_FrameImage30x30Style == null)
                {
                    _FrameImage30x30Style = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.GetPadding(0),
                            SStyle.GetMargin(0),
                            new Setter{Property = VisualElement.HeightRequestProperty, Value = SStyle.Maintain_WidthAspectRatio(30) },
                            SStyle.GetWidthRequest(30),
                            CornerRadiusF.GetCornerRadiusValue(15),
                        }
                    };
                }
                return _FrameImage30x30Style;
            }
        }
        private static Style _LabelTitleOptionStyle;
        public static Style LabelTitleOptionStyle
        {
            get
            {
                if (_LabelTitleOptionStyle == null)
                {
                    _LabelTitleOptionStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            FontL.FontAttribute.Bold,
                            FontL.FontColor.GetColorValue("#272A3F"),
                            FontL.FontName.SourceSansPro_SemiBold,
                            FontL.FontSize.GetFontSize(20),
                            SStyle.HorizontalOptionStartAndExpand,
                            SStyle.VerticalOptionCenter
                        }
                    };
                }
                return _LabelTitleOptionStyle;
            }
        }
        private static Style _LabelNameUserStyle;
        public static Style LabelNameUserStyle
        {
            get
            {
                if (_LabelNameUserStyle == null)
                {
                    _LabelNameUserStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            FontL.FontAttribute.Bold,
                            FontL.FontColor.GetColorValue("#272A3F"),
                            FontL.FontName.SourceSansPro_Bold,
                            FontL.FontSize.GetFontSize(20),
                            FontL.FontAlignmentHorizontal.Crenter,
                        }
                    };
                }
                return _LabelNameUserStyle;
            }
        }
        private static Style _LabelEmailUserStyle;
        public static Style LabelEmailUserStyle
        {
            get
            {
                if (_LabelEmailUserStyle == null)
                {
                    _LabelEmailUserStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            FontL.FontAttribute.Bold,
                            FontL.FontColor.GetColorValue("#6E7990"),
                            FontL.FontName.SourceSansPro_Bold,
                            FontL.FontSize.GetFontSize(17),
                            FontL.FontAlignmentHorizontal.Crenter,
                        }
                    };
                }
                return _LabelEmailUserStyle;
            }
        }
        private static Style _LabelNumberPhoneUserStyle;
        public static Style LabelNumberPhoneUserStyle
        {
            get
            {
                if (_LabelNumberPhoneUserStyle == null)
                {
                    _LabelNumberPhoneUserStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            FontL.FontColor.GetColorValue("#6E7990"),
                            FontL.FontName.SourceSansPro_Regular,
                            FontL.FontSize.GetFontSize(15),
                            FontL.FontAlignmentHorizontal.Crenter,
                        }
                    };
                }
                return _LabelNumberPhoneUserStyle;
            }
        }
        private static Style _StackLayoutWithSpace15Padding20_10_Style;
        public static Style StackLayoutWithSpace15Padding20_10_Style
        {
            get
            {
                if (_StackLayoutWithSpace15Padding20_10_Style == null)
                {
                    _StackLayoutWithSpace15Padding20_10_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(20,10),
                            SStyle.GetStackLayoutSpacing(15),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithSpace15Padding20_10_Style;
            }
        }
        private static Style _StackLayoutWithSpace10Padding20_10_Style;
        public static Style StackLayoutWithSpace10Padding20_10_Style
        {
            get
            {
                if (_StackLayoutWithSpace10Padding20_10_Style == null)
                {
                    _StackLayoutWithSpace10Padding20_10_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(10),
                            SStyle.GetStackLayoutSpacing(5),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithSpace10Padding20_10_Style;
            }
        }
        private static Style _StackLayoutWithSpace15Padding20_0_Style;
        public static Style StackLayoutWithSpace15Padding20_0_Style
        {
            get
            {
                if (_StackLayoutWithSpace15Padding20_0_Style == null)
                {
                    _StackLayoutWithSpace15Padding20_0_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(20,0),
                            SStyle.GetStackLayoutSpacing(15),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithSpace15Padding20_0_Style;
            }
        }
        #endregion

        #region Product List Page Style
        private static Style _SpanUnitPrice;
        public static Style SpanUnitPrice
        {
            get
            {
                if (_SpanUnitPrice == null)
                {
                    _SpanUnitPrice = new Style(typeof(Span))
                    {
                        Setters =
                        {
                            FontS.FontColor.GetColorValue("#272A3F"),
                            FontS.FontName.HelveticaNeue400_Regular,
                            FontS.FontSize.GetFontSize(13),
                        }
                    };
                }
                return _SpanUnitPrice;
            }
        }
        private static Style _LabelUnitPrice;
        public static Style LabelUnitPrice
        {
            get
            {
                if (_LabelUnitPrice == null)
                {
                    _LabelUnitPrice = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontColor.GetColorValue("#272A3F"),
                            FontL.FontName.HelveticaNeue400_Regular,
                            FontL.FontSize.GetFontSize(13),
                        }
                    };
                }
                return _LabelUnitPrice;
            }
        }
        private static Style _LabelItemCategory;
        public static Style LabelItemCategory
        {
            get
            {
                if (_LabelItemCategory == null)
                {
                    _LabelItemCategory = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontColor.GetColorValue("#95A0B6"),
                            FontL.FontName.Avenir500_Medium,
                            FontL.FontAttribute.Bold,
                            FontL.FontSize.GetFontSize(17),
                            SStyle.GetMargin(10)
                        }
                    };
                }
                return _LabelItemCategory;
            }
        }
        private static Style _LabelFont10Subtitel;
        public static Style LabelFont10Subtitel
        {
            get
            {
                if (_LabelFont10Subtitel == null)
                {
                    _LabelFont10Subtitel = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontColor.GetColorValue("#000000"),
                            FontL.FontName.Avenir500_Medium,
                            FontL.FontAttribute.Bold,
                            FontL.FontSize.GetFontSize(15),
                        }
                    };
                }
                return _LabelFont10Subtitel;
            }
        }
        private static Style _CheckBoxLabelFont15Subtitel;
        public static Style CheckBoxLabelFont15Subtitel
        {
            get
            {
                if (_CheckBoxLabelFont15Subtitel == null)
                {
                    _CheckBoxLabelFont15Subtitel = new Style(typeof(Plugin.InputKit.Shared.Controls.CheckBox))
                    {
                        Setters =
                        {
                            new Setter{Property = Plugin.InputKit.Shared.Controls.CheckBox.TextFontSizeProperty, Value = SStyle.Maintain_WidthAspectRatio(20)},

                        }
                    };
                }
                return _CheckBoxLabelFont15Subtitel;
            }
        }
        private static Style _LabelFont15Subtitel;
        public static Style LabelFont15Subtitel
        {
            get
            {
                if (_LabelFont15Subtitel == null)
                {
                    _LabelFont15Subtitel = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontColor.GetColorValue("#707070"),
                            FontL.FontName.Avenir500_Medium,
                            FontL.FontAttribute.Bold,
                            FontL.FontSize.GetFontSize(20),
                        }
                    };
                }
                return _LabelFont15Subtitel;
            }
        }
        private static Style _BoxLabelH310Style;
        public static Style BoxLabelH310Style
        {
            get
            {
                if (_BoxLabelH310Style == null)
                {
                    _BoxLabelH310Style = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetHeightRequest(310),
                            SStyle.GetMargin(50,0),
                            SStyle.GetBackgroundColorValue("#FFFFFF"),
                            CornerRadiusBX.GetCornerRadiusValue(20),
                            SStyle.HorizontalOptionFill,
                            SStyle.VerticalOptionEnd,
                        }
                    };
                }
                return _BoxLabelH310Style;
            }
        }
        private static Style _FrameImage150X150Style;
        public static Style FrameImage150X150Style
        {
            get
            {
                if (_FrameImage150X150Style == null)
                {
                    _FrameImage150X150Style = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.GetWidthRequest(150),
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(150)},
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(0),
                            CornerRadiusF.GetCornerRadiusValue(10),

                        }
                    };
                }
                return _FrameImage150X150Style;
            }
        }

        private static Style _StackLayoutWithPadding5x5_Style;
        public static Style StackLayoutWithPadding5x5_Style
        {
            get
            {
                if (_StackLayoutWithPadding5x5_Style == null)
                {
                    _StackLayoutWithPadding5x5_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(5,5),
                            SStyle.GetStackLayoutSpacing(0),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithPadding5x5_Style;
            }
        }
        private static Style _StackLayoutWithSpacing10Padding20x10x10x10_Style;
        public static Style StackLayoutWithSpacing10Padding20x10x10x10_Style
        {
            get
            {
                if (_StackLayoutWithSpacing10Padding20x10x10x10_Style == null)
                {
                    _StackLayoutWithSpacing10Padding20x10x10x10_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(20,10,10,10),
                            SStyle.GetStackLayoutSpacing(10),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithSpacing10Padding20x10x10x10_Style;
            }
        }
        private static Style _GridWithMargin30x0Style;
        public static Style GridWithMargin30x0Style
        {
            get
            {
                if (_GridWithMargin30x0Style == null)
                {
                    _GridWithMargin30x0Style = new Style(typeof(Grid))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(30,0),
                            SStyle.GetPadding(0),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _GridWithMargin30x0Style;
            }
        }
        private static Style _FrameRaduis5Padding2_Style;
        public static Style FrameRaduis5Padding2_Style
        {
            get
            {
                if (_FrameRaduis5Padding2_Style == null)
                {
                    _FrameRaduis5Padding2_Style = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(2),
                            CornerRadiusF.GetCornerRadiusValue(5)
                        }
                    };
                }
                return _FrameRaduis5Padding2_Style;
            }
        }

        private static Style _FrameRaduis5Padding5_Style;
        public static Style FrameRaduis5Padding5_Style
        {
            get
            {
                if (_FrameRaduis5Padding5_Style == null)
                {
                    _FrameRaduis5Padding5_Style = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(5),
                            CornerRadiusF.GetCornerRadiusValue(5)
                        }
                    };
                }
                return _FrameRaduis5Padding5_Style;
            }
        }
        private static Style _FrameRaduis10Padding5_Style;
        public static Style FrameRaduis10Padding5_Style
        {
            get
            {
                if (_FrameRaduis10Padding5_Style == null)
                {
                    _FrameRaduis10Padding5_Style = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(5),
                            CornerRadiusF.GetCornerRadiusValue(10)
                        }
                    };
                }
                return _FrameRaduis10Padding5_Style;
            }
        }
        private static Style _FrameRaduis10Padding0x0x3x0_Style;
        public static Style FrameRaduis10Padding0x0x3x0_Style
        {
            get
            {
                if (_FrameRaduis10Padding0x0x3x0_Style == null)
                {
                    _FrameRaduis10Padding0x0x3x0_Style = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(0,0,3,0),
                            CornerRadiusF.GetCornerRadiusValue(10)
                        }
                    };
                }
                return _FrameRaduis10Padding0x0x3x0_Style;
            }
        }
        private static Style _FrameRaduis10_Style;
        public static Style FrameRaduis10_Style
        {
            get
            {
                if (_FrameRaduis10_Style == null)
                {
                    _FrameRaduis10_Style = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(0),
                            CornerRadiusF.GetCornerRadiusValue(10)
                        }
                    };
                }
                return _FrameRaduis10_Style;
            }
        }
        private static Style _BoxViewRaduis10Margin0x75x0x0_Style;
        public static Style BoxViewRaduis10Margin0x75x0x0_Style
        {
            get
            {
                if (_BoxViewRaduis10Margin0x75x0x0_Style == null)
                {
                    _BoxViewRaduis10Margin0x75x0x0_Style = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0,75,0,0),
                            CornerRadiusBX.GetCornerRadiusValue(20)
                        }
                    };
                }
                return _BoxViewRaduis10Margin0x75x0x0_Style;
            }
        }

        private static Style _StackLayoutWithSpace10Margin0x150x0x0Padding15x30_Style;
        public static Style StackLayoutWithSpace10Margin0x150x0x0Padding15x30_Style
        {
            get
            {
                if (_StackLayoutWithSpace10Margin0x150x0x0Padding15x30_Style == null)
                {
                    _StackLayoutWithSpace10Margin0x150x0x0Padding15x30_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.HorizontalOptionStart,
                            SStyle.GetMargin(0,175,0,0),
                            SStyle.GetPadding(15,30),
                            SStyle.GetStackLayoutSpacing(10),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithSpace10Margin0x150x0x0Padding15x30_Style;
            }
        }
        private static Style _PickerSelectChoiceBasic;
        public static Style PickerSelectChoiceBasic
        {
            get
            {
                if (_PickerSelectChoiceBasic == null)
                {
                    _PickerSelectChoiceBasic = new Style(typeof(UserControls.GradientPicker))
                    {
                        Setters =
                        {
                            new Setter{Property = UserControls.GradientPicker.HeightRequestPickerProperty, Value = SStyle.Maintain_WidthAspectRatio(305)*45/305},
                            new Setter{Property = UserControls.GradientPicker.WidthRequestPickerProperty, Value = SStyle.Maintain_WidthAspectRatio(305)},
                            new Setter{Property = UserControls.GradientPicker.RaduisMarginProperty, Value = SStyle.Maintain_HeightAspectRatio(10)},

                            new Setter{Property = UserControls.GradientPicker.SizeFontButtonProperty, Value = FontL.FontSize.GetFontSize(17).Value },
                            new Setter{Property = UserControls.GradientPicker.FamilyFontButtonProperty, Value = FontL.FontName.SourceSansPro_SemiBold.Value},
                            new Setter{Property = UserControls.GradientPicker.FontAttributesButtonProperty, Value = FontAttributes.Bold},
                            new Setter{Property = UserControls.GradientPicker.TextColorButtonProperty, Value= Color.FromHex("#95A0B6")},
                            SStyle.GetMargin(0),

                        }
                    };
                }
                return _PickerSelectChoiceBasic;
            }
        }
        private static Style _PickerSelectChoiceRaduis10;
        public static Style PickerSelectChoiceRaduis10
        {
            get
            {
                if (_PickerSelectChoiceRaduis10 == null)
                {
                    _PickerSelectChoiceRaduis10 = new Style(typeof(UserControls.GradientPicker))
                    {
                        Setters =
                        {
                            new Setter{Property = UserControls.GradientPicker.HeightRequestPickerProperty, Value = SStyle.Maintain_WidthAspectRatio(305)*55/305},
                            new Setter{Property = UserControls.GradientPicker.WidthRequestPickerProperty, Value = SStyle.Maintain_WidthAspectRatio(305)},
                            new Setter{Property = UserControls.GradientPicker.RaduisMarginProperty, Value = SStyle.Maintain_HeightAspectRatio(7)},

                            new Setter{Property = UserControls.GradientPicker.SizeFontButtonProperty, Value = FontL.FontSize.GetFontSize(17).Value },
                            new Setter{Property = UserControls.GradientPicker.FamilyFontButtonProperty, Value = FontL.FontName.SourceSansPro_SemiBold.Value},
                            new Setter{Property = UserControls.GradientPicker.FontAttributesButtonProperty, Value = FontAttributes.Bold},
                            new Setter{Property = UserControls.GradientPicker.TextColorButtonProperty, Value= Color.FromHex("#95A0B6")},
                            SStyle.GetMargin(0,0,0,10),

                        }
                    };
                }
                return _PickerSelectChoiceRaduis10;
            }
        }
        private static Style _PickerSelectChoice;
        public static Style PickerSelectChoice
        {
            get
            {
                if (_PickerSelectChoice == null)
                {
                    _PickerSelectChoice = new Style(typeof(UserControls.GradientPicker))
                    {
                        Setters =
                        {
                            new Setter{Property = UserControls.GradientPicker.HeightRequestPickerProperty, Value = SStyle.Maintain_WidthAspectRatio(305)*55/305},
                            new Setter{Property = UserControls.GradientPicker.WidthRequestPickerProperty, Value = SStyle.Maintain_WidthAspectRatio(305)},
                            new Setter{Property = UserControls.GradientPicker.RaduisMarginProperty, Value = SStyle.Maintain_HeightAspectRatio(4)},

                            new Setter{Property = UserControls.GradientPicker.SizeFontButtonProperty, Value = FontL.FontSize.GetFontSize(17).Value },
                            new Setter{Property = UserControls.GradientPicker.FamilyFontButtonProperty, Value = FontL.FontName.SourceSansPro_SemiBold.Value},
                            new Setter{Property = UserControls.GradientPicker.FontAttributesButtonProperty, Value = FontAttributes.Bold},
                            new Setter{Property = UserControls.GradientPicker.TextColorButtonProperty, Value= Color.FromHex("#95A0B6")},
                            SStyle.GetMargin(0,0,0,10),

                        }
                    };
                }
                return _PickerSelectChoice;
            }
        }
        private static Style _InputChoiceGroupSpase10Padding5;
        public static Style InputChoiceGroupSpase10Padding5
        {
            get
            {
                if (_InputChoiceGroupSpase10Padding5 == null)
                {
                    _InputChoiceGroupSpase10Padding5 = new Style(typeof(Plugin.InputKit.Shared.Controls.RadioButtonGroupView))
                    {
                        Setters =
                        {
                            new Setter{Property = StackLayout.SpacingProperty, Value = SStyle.GetStackLayoutSpacing(10).Value },
                            new Setter{Property = Plugin.InputKit.Shared.Controls.RadioButtonGroupView.PaddingProperty, Value= SStyle.GetPadding(5).Value},
                            SStyle.GetMargin(0),

                        }
                    };
                }
                return _InputChoiceGroupSpase10Padding5;
            }
        }
        private static Style _InputChoiceFont20;
        public static Style InputChoiceFont20
        {
            get
            {
                if (_InputChoiceFont20 == null)
                {
                    _InputChoiceFont20 = new Style(typeof(Plugin.InputKit.Shared.Controls.RadioButton))
                    {
                        Setters =
                        {
                            new Setter{Property = Plugin.InputKit.Shared.Controls.RadioButton.TextFontSizeProperty, Value = FontL.FontSize.GetFontSize(20).Value },
                            new Setter{Property = Plugin.InputKit.Shared.Controls.RadioButton.TextColorProperty, Value= Color.FromHex("#272A3F")},
                            SStyle.GetMargin(0),

                        }
                    };
                }
                return _InputChoiceFont20;
            }
        }
        private static Style _InputChoice;
        public static Style InputChoice
        {
            get
            {
                if (_InputChoice == null)
                {
                    _InputChoice = new Style(typeof(Plugin.InputKit.Shared.Controls.RadioButton))
                    {
                        Setters =
                        {
                            new Setter{Property = Plugin.InputKit.Shared.Controls.RadioButton.TextFontSizeProperty, Value = FontL.FontSize.GetFontSize(17).Value },
                            new Setter{Property = Plugin.InputKit.Shared.Controls.RadioButton.TextColorProperty, Value= Color.FromHex("#95A0B6")},
                            SStyle.GetMargin(0),

                        }
                    };
                }
                return _InputChoice;
            }
        }
        private static Style _StackLayoutWithPadding2_Style;
        public static Style StackLayoutWithPadding2_Style
        {
            get
            {
                if (_StackLayoutWithPadding2_Style == null)
                {
                    _StackLayoutWithPadding2_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.HorizontalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(2),
                            SStyle.GetStackLayoutSpacing(0),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithPadding2_Style;
            }
        }
        private static Style _StackLayoutWithPadding20x5_Style;
        public static Style StackLayoutWithPadding20x5_Style
        {
            get
            {
                if (_StackLayoutWithPadding20x5_Style == null)
                {
                    _StackLayoutWithPadding20x5_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.HorizontalOptionStart,
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(20,5),
                            SStyle.GetStackLayoutSpacing(0),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithPadding20x5_Style;
            }
        }
        private static Style _FrameMW40P3R30_Style;
        public static Style FrameMW40P3R30_Style
        {
            get
            {
                if (_FrameMW40P3R30_Style == null)
                {
                    _FrameMW40P3R30_Style = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.HorizontalOptionStart,
                            SStyle.GetMargin(0),
                            new Setter{ Property = VisualElement.MinimumWidthRequestProperty, Value = SStyle.Maintain_WidthAspectRatio(40) },
                            SStyle.GetPadding(3),
                            CornerRadiusF.GetCornerRadiusValue(30),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _FrameMW40P3R30_Style;
            }
        }
        private static Style _BoxViewR20x20x0x0_Style;
        public static Style BoxViewR20x20x0x0_Style
        {
            get
            {
                if (_BoxViewR20x20x0x0_Style == null)
                {
                    _BoxViewR20x20x0x0_Style = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.HorizontalOptionStart,
                            SStyle.GetMargin(0),
                            CornerRadiusBX.GetCornerRadiusValue(20,20,0,0),
                        }
                    };
                }
                return _BoxViewR20x20x0x0_Style;
            }
        }
        #endregion

        #region Label About US
        private static Style _LabelTextPriceInvoiceStyle;
        public static Style LabelTextPriceInvoiceStyle
        {
            get
            {
                if (_LabelTextPriceInvoiceStyle == null)
                {
                    _LabelTextPriceInvoiceStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontName.Renner500_Medium,
                            FontL.FontSize.GetFontSize(30),
                            FontL.FontColor.GetColorValue("#272A3F"),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _LabelTextPriceInvoiceStyle;
            }
        }
        private static Style _LabelTextAboutUsStyle;
        public static Style LabelTextAboutUsStyle
        {
            get
            {
                if (_LabelTextAboutUsStyle == null)
                {
                    _LabelTextAboutUsStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontName.Renner500_Medium,
                            FontL.FontSize.GetFontSize(20),
                            FontL.FontColor.GetColorValue("#272A3F"),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _LabelTextAboutUsStyle;
            }
        }
        private static Style _LabelAboutUSStyle;
        public static Style LabelAboutUSStyle
        {
            get
            {
                if (_LabelAboutUSStyle == null)
                {
                    _LabelAboutUSStyle = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            FontL.FontColor.GetColorValue("#272A3F"),
                            FontL.FontAttribute.Bold,
                            FontL.FontName.HelveticaNeue400_Regular,
                            FontL.FontSize.GetFontSize(45),
                            FontL.FontAlignmentHorizontal.Crenter,
                        }
                    };
                }
                return _LabelAboutUSStyle;
            }
        }
        private static Style _BoxLabelH300Maring0x0x0x1Style;
        public static Style BoxLabelH300Maring0x0x0x1Style
        {
            get
            {
                if (_BoxLabelH300Maring0x0x0x1Style == null)
                {
                    _BoxLabelH300Maring0x0x0x1Style = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0,0,0,2),
                            SStyle.GetHeightRequest(300),
                            CornerRadiusBX.GetCornerRadiusValue(0,0,25,25),
                        }
                    };
                }
                return _BoxLabelH300Maring0x0x0x1Style;
            }
        }
        private static Style _BoxLabelH300Style;
        public static Style BoxLabelH300Style
        {
            get
            {
                if (_BoxLabelH300Style == null)
                {
                    _BoxLabelH300Style = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            SStyle.GetHeightRequest(300),
                            CornerRadiusBX.GetCornerRadiusValue(0,0,25,25),
                        }
                    };
                }
                return _BoxLabelH300Style;
            }
        }
        #endregion

        #region ContactUs Page
        private static Style _StackLayoutWithMarginUp30_Style;
        public static Style StackLayoutWithMarginUp30_Style
        {
            get
            {
                if (_StackLayoutWithMarginUp30_Style == null)
                {
                    _StackLayoutWithMarginUp30_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0,-30,0,0),
                            SStyle.GetPadding(0),
                            SStyle.GetStackLayoutSpacing(0),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithMarginUp30_Style;
            }
        }
        private static Style _StackLayoutWithMarginUp40_Style;
        public static Style StackLayoutWithMarginUp40_Style
        {
            get
            {
                if (_StackLayoutWithMarginUp40_Style == null)
                {
                    _StackLayoutWithMarginUp40_Style = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.GetMargin(0,-40,0,0),
                            SStyle.GetPadding(35,0,35,10),
                            SStyle.GetStackLayoutSpacing(20),
                            SStyle.ViewFlowDirection,
                        }
                    };
                }
                return _StackLayoutWithMarginUp40_Style;
            }
        }
        private static Style _FramePadding10Marign0;
        public static Style FramePadding10Marign0
        {
            get
            {
                if (_FramePadding10Marign0 == null)
                {
                    _FramePadding10Marign0 = new Style(typeof(Frame))
                    {
                        Setters =
                        {

                            CornerRadiusF.GetCornerRadiusValue(0),
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(10),

                        }
                    };
                }
                return _FramePadding10Marign0;
            }
        }
        private static Style _FrameMaring30Raduis10;
        public static Style FrameMaring30Raduis10
        {
            get
            {
                if (_FrameMaring30Raduis10 == null)
                {
                    _FrameMaring30Raduis10 = new Style(typeof(Frame))
                    {
                        Setters =
                        {

                            CornerRadiusF.GetCornerRadiusValue(10),
                            SStyle.GetMargin(30),
                            SStyle.GetPadding(0),

                        }
                    };
                }
                return _FrameMaring30Raduis10;
            }
        }
        private static Style _FrameMaring15Raduis10;
        public static Style FrameMaring15Raduis10
        {
            get
            {
                if (_FrameMaring15Raduis10 == null)
                {
                    _FrameMaring15Raduis10 = new Style(typeof(Frame))
                    {
                        Setters =
                        {

                            CornerRadiusF.GetCornerRadiusValue(10),
                            SStyle.GetMargin(15),
                            SStyle.GetPadding(0),

                        }
                    };
                }
                return _FrameMaring15Raduis10;
            }
        }
        private static Style _FrameMaring_15;
        public static Style FrameMaring_15
        {
            get
            {
                if (_FrameMaring_15 == null)
                {
                    _FrameMaring_15 = new Style(typeof(Frame))
                    {
                        Setters =
                        {

                            CornerRadiusF.GetCornerRadiusValue(0),
                            SStyle.GetMargin(bottom:-10),
                            SStyle.GetPadding(0),

                        }
                    };
                }
                return _FrameMaring_15;
            }
        }
        private static Style _FrameContactInUp;
        public static Style FrameContactInUp
        {
            get
            {
                if (_FrameContactInUp == null)
                {
                    _FrameContactInUp = new Style(typeof(Frame))
                    {
                        Setters =
                        {

                            CornerRadiusF.GetCornerRadiusValue(20),
                            SStyle.GetMargin(35,-55,35,0),
                            SStyle.GetPadding(14),

                        }
                    };
                }
                return _FrameContactInUp;
            }
        }
        private static Style _FrameContactUs;
        public static Style FrameContactUs
        {
            get
            {
                if (_FrameContactUs == null)
                {
                    _FrameContactUs = new Style(typeof(Layout))
                    {
                        Setters =
                        {

                            CornerRadiusF.GetCornerRadiusValue(20),
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(14),

                        }
                    };
                }
                return _FrameContactUs;
            }
        }
        private static Style _LabelTitleSize25;
        public static Style LabelTitleSize25
        {
            get
            {
                if (_LabelTitleSize25 == null)
                {
                    _LabelTitleSize25 = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            FontL.FontAttribute.Italic,
                            FontL.FontColor.GetColorValue("#272A3F"),
                            FontL.FontName.SourceSansPro_SemiBold,
                            FontL.FontSize.GetFontSize(25),
                            SStyle.HorizontalOptionStartAndExpand,
                            SStyle.VerticalOptionCenter
                        }
                    };
                }
                return _LabelTitleSize25;
            }
        }
        private static Style _FFImage32X32Style;
        public static Style FFImage32X32Style
        {
            get
            {
                if (_FFImage32X32Style == null)
                {
                    _FFImage32X32Style = new Style(typeof(FFImageLoading.Svg.Forms.SvgCachedImage))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(32)},
                            SStyle.GetWidthRequest(32),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _FFImage32X32Style;
            }
        }
        private static Style _Image32X32Style;
        public static Style Image32X32Style
        {
            get
            {
                if (_Image32X32Style == null)
                {
                    _Image32X32Style = new Style(typeof(Image))
                    {
                        Setters =
                        {
                            new Setter{Property = VisualElement.HeightRequestProperty,Value = SStyle.Maintain_WidthAspectRatio(32)},
                            SStyle.GetWidthRequest(32),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _Image32X32Style;
            }
        }
        #endregion

        #region Delivery Areas

        private static Style _BoxLabelDeliveryAreasStyle;
        public static Style BoxLabelDeliveryAreasStyle
        {
            get
            {
                if (_BoxLabelDeliveryAreasStyle == null)
                {
                    _BoxLabelDeliveryAreasStyle = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(20,8,20,0),
                            SStyle.GetHeightRequest(110),
                            SStyle.GetWidthRequest(6),
                            CornerRadiusBX.GetCornerRadiusValue(0),
                        }
                    };
                }
                return _BoxLabelDeliveryAreasStyle;
            }
        }
        private static Style _FrameDeliveryAreas;
        public static Style FrameDeliveryAreas
        {
            get
            {
                if (_FrameDeliveryAreas == null)
                {
                    _FrameDeliveryAreas = new Style(typeof(Layout))
                    {
                        Setters =
                        {

                            CornerRadiusF.GetCornerRadiusValue(20),
                            SStyle.GetMargin(8,0,8,10),
                            SStyle.GetPadding(14),

                        }
                    };
                }
                return _FrameDeliveryAreas;
            }
        }
        #endregion
        #region Menu Page Style
        private static Style _ButtonImageMarign0Padding10Raduis5;
        public static Style ButtonImageMarign0Padding10Raduis5
        {
            get
            {
                if (_ButtonImageMarign0Padding10Raduis5 == null)
                {
                    _ButtonImageMarign0Padding10Raduis5 = new Style(typeof(ImageButton))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(10),
                        }
                    };
                }
                return _ButtonImageMarign0Padding10Raduis5;
            }
        }
        private static Style _FrameOptionMenuMarign0Padding10Raduis5H50W50;
        public static Style FrameOptionMenuMarign0Padding10Raduis5H50W50
        {
            get
            {
                if (_FrameOptionMenuMarign0Padding10Raduis5H50W50 == null)
                {
                    _FrameOptionMenuMarign0Padding10Raduis5H50W50 = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.VerticalOptionStart,
                            SStyle.HorizontalOptionStart,
                            new Setter{Property = VisualElement.HeightRequestProperty, Value = SStyle.Maintain_WidthAspectRatio(100)},
                            SStyle.GetWidthRequest(100),
                            SStyle.GetMargin(5),
                            SStyle.GetPadding(10),
                            CornerRadiusF.GetCornerRadiusValue(5),
                        }
                    };
                }
                return _FrameOptionMenuMarign0Padding10Raduis5H50W50;
            }
        }
        private static Style _FrameHitLocation;
        public static Style FrameHitLocation
        {
            get
            {
                if (_FrameHitLocation == null)
                {
                    _FrameHitLocation = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(10),
                            SStyle.GetPadding(20),
                            CornerRadiusF.GetCornerRadiusValue(30),
                        }
                    };
                }
                return _FrameHitLocation;
            }
        }
        private static Style _FrameOptionMenuMarign0Padding10Raduis5;
        public static Style FrameOptionMenuMarign0Padding10Raduis5
        {
            get
            {
                if (_FrameOptionMenuMarign0Padding10Raduis5 == null)
                {
                    _FrameOptionMenuMarign0Padding10Raduis5 = new Style(typeof(Frame))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(5),
                            SStyle.GetPadding(10),
                            CornerRadiusF.GetCornerRadiusValue(5),
                        }
                    };
                }
                return _FrameOptionMenuMarign0Padding10Raduis5;
            }
        }
        private static Style _StackLayoutPadding20x20x20x0;
        public static Style StackLayoutPadding20x20x20x0
        {
            get
            {
                if (_StackLayoutPadding20x20x20x0 == null)
                {
                    _StackLayoutPadding20x20x20x0 = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(20,20,20,0),
                            SStyle.GetStackLayoutSpacing(0),
                            SStyle.ViewFlowDirection
                        }
                    };
                }
                return _StackLayoutPadding20x20x20x0;
            }
        }
        private static Style _StackLayoutMargin0_15_0_0Padding20x10Spacing5;
        public static Style StackLayoutMargin0_15_0_0Padding20x10Spacing5
        {
            get
            {
                if (_StackLayoutMargin0_15_0_0Padding20x10Spacing5 == null)
                {
                    _StackLayoutMargin0_15_0_0Padding20x10Spacing5 = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.GetPadding(20,10),
                            SStyle.GetMargin(0,-15,0,0),
                            SStyle.GetStackLayoutSpacing(5),
                            SStyle.ViewFlowDirection
                        }
                    };
                }
                return _StackLayoutMargin0_15_0_0Padding20x10Spacing5;
            }
        }
        private static Style _StackLayoutPadding20x10Spacing5;
        public static Style StackLayoutPadding20x10Spacing5
        {
            get
            {
                if (_StackLayoutPadding20x10Spacing5 == null)
                {
                    _StackLayoutPadding20x10Spacing5 = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.GetPadding(20,10),
                            SStyle.GetMargin(0),
                            SStyle.GetStackLayoutSpacing(5),
                            SStyle.ViewFlowDirection
                        }
                    };
                }
                return _StackLayoutPadding20x10Spacing5;
            }
        }
        private static Style _StackLayoutPadding20x10Spasing10;
        public static Style StackLayoutPadding20x10Spasing10
        {
            get
            {
                if (_StackLayoutPadding20x10Spasing10 == null)
                {
                    _StackLayoutPadding20x10Spasing10 = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(20,10),
                            SStyle.GetStackLayoutSpacing(10),
                            SStyle.ViewFlowDirection
                        }
                    };
                }
                return _StackLayoutPadding20x10Spasing10;
            }
        }
        private static Style _StackLayoutMaring20x10Spacing5;
        public static Style StackLayoutMaring20x10Spacing5
        {
            get
            {
                if (_StackLayoutMaring20x10Spacing5 == null)
                {
                    _StackLayoutMaring20x10Spacing5 = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(20,10),
                            SStyle.GetPadding(0),
                            SStyle.GetStackLayoutSpacing(5),
                            SStyle.ViewFlowDirection
                        }
                    };
                }
                return _StackLayoutMaring20x10Spacing5;
            }
        }
        private static Style _StackLayoutMaring20x10x20x30Spacing5;
        public static Style StackLayoutMaring20x10x20x30Spacing5
        {
            get
            {
                if (_StackLayoutMaring20x10x20x30Spacing5 == null)
                {
                    _StackLayoutMaring20x10x20x30Spacing5 = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(20,10,20,30),
                            SStyle.GetPadding(0),
                            SStyle.GetStackLayoutSpacing(5),
                            SStyle.ViewFlowDirection
                        }
                    };
                }
                return _StackLayoutMaring20x10x20x30Spacing5;
            }
        }
        private static Style _LabeNameOptionMenu;
        public static Style LabeNameOptionMenu
        {
            get
            {
                if (_LabeNameOptionMenu == null)
                {
                    _LabeNameOptionMenu = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontColor.GetColorValue("#818793"),
                            FontL.FontName.Montserrat400_Regular,
                            FontL.FontSize.GetFontSize(13),
                            FontL.FontAlignmentHorizontal.Crenter,
                        }
                    };
                }
                return _LabeNameOptionMenu;
            }
        }
        #endregion
        #region Login Page Style
        private static Style _StackPadding20Spacing10;
        public static Style StackPadding20Spacing10
        {
            get
            {
                if (_StackPadding20Spacing10 == null)
                {
                    _StackPadding20Spacing10 = new Style(typeof(StackLayout))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            SStyle.GetPadding(20),
                            SStyle.GetStackLayoutSpacing(10),
                            SStyle.ViewFlowDirection
                        }
                    };
                }
                return _StackPadding20Spacing10;
            }
        }
        private static Style _BX_BGEntry;
        public static Style BX_BGEntry
        {
            get
            {
                if (_BX_BGEntry == null)
                {
                    _BX_BGEntry = new Style(typeof(BoxView))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            CornerRadiusBX.GetCornerRadiusValue(10)
                        }
                    };
                }
                return _BX_BGEntry;
            }
        }
        private static Style _EntryCustomMargin5x0;
        public static Style EntryCustomMargin5x0
        {
            get
            {
                if (_EntryCustomMargin5x0 == null)
                {
                    _EntryCustomMargin5x0 = new Style(typeof(Entry))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(5,0),
                            FontE.FontSize.GetFontSize(15),
                            FontE.FontName.SourceSansPro_Regular,
                            FontE.FontColor.GetColorValue("#272A3F")
                        }
                    };
                }
                return _EntryCustomMargin5x0;
            }
        }
        private static Style _DatePickerFont10Margin4x2x2x2;
        public static Style DatePickerFont10Margin4x2x2x2
        {
            get
            {
                if (_DatePickerFont10Margin4x2x2x2 == null)
                {
                    _DatePickerFont10Margin4x2x2x2 = new Style(typeof(DatePicker))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(4,2,2,2),
                            new Setter{Property = DatePicker.FontSizeProperty, Value = SStyle.Maintain_WidthAspectRatio(15)},
                            new Setter{Property = DatePicker.FontFamilyProperty,
                                       Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-Regular.ttf#SourceSansPro Regular" : "SourceSansPro Regular"},
                            new Setter{Property = DatePicker.TextColorProperty, Value = Color.FromHex("#272A3F")},
                        }
                    };
                }
                return _DatePickerFont10Margin4x2x2x2;
            }
        }
        private static Style _PickerFont10Margin4x2x2x2;
        public static Style PickerFont10Margin4x2x2x2
        {
            get
            {
                if (_PickerFont10Margin4x2x2x2 == null)
                {
                    _PickerFont10Margin4x2x2x2 = new Style(typeof(Picker))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(4,2,2,2),
                            new Setter{Property = Picker.FontSizeProperty, Value = SStyle.Maintain_WidthAspectRatio(15)},
                            new Setter{Property = Picker.FontFamilyProperty,
                                       Value = Device.RuntimePlatform == Device.Android ? "SourceSansPro-Regular.ttf#SourceSansPro Regular" : "SourceSansPro Regular"},
                            new Setter{Property = Picker.TextColorProperty, Value = Color.FromHex("#272A3F")},
                        }
                    };
                }
                return _PickerFont10Margin4x2x2x2;
            }
        }
        private static Style _ButtonFont10Raduis5;
        public static Style ButtonFont10Raduis5
        {
            get
            {
                if (_ButtonFont10Raduis5 == null)
                {
                    _ButtonFont10Raduis5 = new Style(typeof(Button))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            SStyle.GetHeightRequest(56),
                            SStyle.GetPadding(0,0,-3,0),
                            FontB.FontSize.GetFontSize(15),
                            FontB.FontName.SourceSansPro_Regular,
                            CornerRadiusB.GetCornerRadiusValue(5),
                        }
                    };
                }
                return _ButtonFont10Raduis5;
            }
        }
        private static Style _ButtonFont10;
        public static Style ButtonFont10
        {
            get
            {
                if (_ButtonFont10 == null)
                {
                    _ButtonFont10 = new Style(typeof(Button))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(0),
                            SStyle.GetHeightRequest(56),
                            SStyle.GetPadding(0,0,-3,0),
                            FontB.FontSize.GetFontSize(15),
                            FontB.FontName.SourceSansPro_Regular,
                        }
                    };
                }
                return _ButtonFont10;
            }
        }
        private static Style _EntryCustomFont10Margin4x2x2x2;
        public static Style EntryCustomFont10Margin4x2x2x2
        {
            get
            {
                if (_EntryCustomFont10Margin4x2x2x2 == null)
                {
                    _EntryCustomFont10Margin4x2x2x2 = new Style(typeof(Entry))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(4,2,2,2),
                            FontE.FontSize.GetFontSize(15),
                            SStyle.VerticalOptionCenter,
                            FontE.FontName.SourceSansPro_Regular,
                            FontE.FontColor.GetColorValue("#272A3F"),
                            FontE.FontAlignmentHorizontal.Start
                        }
                    };
                }
                return _EntryCustomFont10Margin4x2x2x2;
            }
        }
        private static Style _EntryCustomMargin4x2x2x2;
        public static Style EntryCustomMargin4x2x2x2
        {
            get
            {
                if (_EntryCustomMargin4x2x2x2 == null)
                {
                    _EntryCustomMargin4x2x2x2 = new Style(typeof(Entry))
                    {
                        Setters =
                        {
                            SStyle.GetMargin(4,2,2,2),
                            FontE.FontSize.GetFontSize(15),
                            FontE.FontName.SourceSansPro_Regular,
                            FontE.FontColor.GetColorValue("#272A3F")
                        }
                    };
                }
                return _EntryCustomMargin4x2x2x2;
            }
        }
        private static Style _LabelFont13Subtitel;
        public static Style LabelFont13Subtitel
        {
            get
            {
                if (_LabelFont13Subtitel == null)
                {
                    _LabelFont13Subtitel = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontName.Avenir500_Medium,
                            FontL.FontAttribute.Bold,
                            FontL.FontSize.GetFontSize(13),
                        }
                    };
                }
                return _LabelFont13Subtitel;
            }
        }
        private static Style _ButtonRaduis10Marign30x0;
        public static Style ButtonRaduis10Marign30x0
        {
            get
            {
                if (_ButtonRaduis10Marign30x0 == null)
                {
                    _ButtonRaduis10Marign30x0 = new Style(typeof(Button))
                    {
                        Setters =
                        {
                            FontB.FontSize.GetFontSize(23),
                            SStyle.GetMargin(30,5),
                            CornerRadiusB.GetCornerRadiusValue(10),
                            new Setter{ Property = Button.BorderWidthProperty, Value = SStyle.Maintain_WidthAspectRatio(1) }
                        }
                    };
                }
                return _ButtonRaduis10Marign30x0;
            }
        }
        private static Style _CButtonMarign0Height80;
        public static Style CButtonMarign0Height80
        {
            get
            {
                if (_CButtonMarign0Height80 == null)
                {
                    _CButtonMarign0Height80 = new Style(typeof(Button))
                    {
                        Setters =
                        {
                            FontB.FontSize.GetFontSize(23),
                            SStyle.GetMargin(0),
                            CornerRadiusB.GetCornerRadiusValue(0),
                        }
                    };
                }
                return _CButtonMarign0Height80;
            }
        }
        private static Style _CButtonRaduis10Marign30x0;
        public static Style CButtonRaduis10Marign30x0
        {
            get
            {
                if (_CButtonRaduis10Marign30x0 == null)
                {
                    _CButtonRaduis10Marign30x0 = new Style(typeof(Button))
                    {
                        Setters =
                        {
                            FontB.FontSize.GetFontSize(23),
                            SStyle.GetMargin(30,0),
                            CornerRadiusB.GetCornerRadiusValue(10),
                        }
                    };
                }
                return _CButtonRaduis10Marign30x0;
            }
        }
        private static Style _LabelFontSize35;
        public static Style LabelFontSize35
        {
            get
            {
                if (_LabelFontSize35 == null)
                {
                    _LabelFontSize35 = new Style(typeof(Label))
                    {
                        Setters =
                        {
                            FontL.FontName.SourceSansPro_Bold,
                            FontL.FontSize.GetFontSize(35),
                            SStyle.GetMargin(0),
                        }
                    };
                }
                return _LabelFontSize35;
            }
        }
        #endregion
    }
}