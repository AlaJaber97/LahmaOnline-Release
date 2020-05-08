using System;
using System.Linq;
using System.Threading.Tasks;
using Android.Support.V4.View;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;

using AViews = Android.Views;
using AWidget = Android.Widget;
using System.Collections.Specialized;
using System.Collections.Generic;
using Android.Content;
using Android.App;

using Android.Views;
using Android.Views.InputMethods;
using Object = Java.Lang.Object;
using Android.Runtime;
using Android.Util;
using Android.Graphics;
using Android.OS;
using Java.Lang;
using Android.Support.V4.Content;
using Mobile.Droid.CarouselViewStuff;
using LahmaOnline.CustomRenderer;
using LahmaOnline.Droid;

[assembly: ExportRenderer(typeof(CarouselViewControl), typeof(LahmaOnline.Droid.CustomRenderer.CarouselViewRenderer))]
namespace LahmaOnline.Droid.CustomRenderer
{
    public class CarouselViewRenderer : ViewRenderer<CarouselViewControl, AViews.View>
    {
        Context _context;

        bool orientationChanged;

        AViews.View nativeView;
        ViewPager viewPager;
        CirclePageIndicator indicators;

        AWidget.LinearLayout prevBtn;
        AWidget.LinearLayout nextBtn;

        bool _disposed;

        bool isChangingPosition;

        bool isKeyboardVisible;
        readonly SoftKeyboardService keyboardService;

        //public bool IsTimer { get; set; }

        public CarouselViewRenderer(Context context) : base(context)
        {
            _context = context;

            // KeyboardService code
            var activity = _context as Activity;
            if (activity != null)
                keyboardService = new SoftKeyboardService(activity);

        }

        protected override void OnElementChanged(ElementChangedEventArgs<CarouselViewControl> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                // Instantiate the native control and assign it to the Control property with
                // the SetNativeControl method (called when Height BP changes)
                orientationChanged = true;
            }

            if (e.OldElement != null)
            {
                // Unsubscribe from event handlers and cleanup any resources

                if (Element == null) return;

                Element.SizeChanged -= Element_SizeChanged;
                if (Element.ItemsSource != null && Element.ItemsSource is INotifyCollectionChanged)
                    ((INotifyCollectionChanged)Element.ItemsSource).CollectionChanged -= ItemsSource_CollectionChanged;

                // KeyboardService code
                Xamarin.Forms.Application.Current.MainPage.SizeChanged -= MainPage_SizeChanged;
                keyboardService.VisibilityChanged -= KeyboardService_VisibilityChanged;
            }

            if (e.NewElement != null)
            {
                Element.SizeChanged += Element_SizeChanged;

                // Configure the control and subscribe to event handlers
                if (Element.ItemsSource != null && Element.ItemsSource is INotifyCollectionChanged)
                    ((INotifyCollectionChanged)Element.ItemsSource).CollectionChanged += ItemsSource_CollectionChanged;

                // KeyboardService code
                Xamarin.Forms.Application.Current.MainPage.SizeChanged += MainPage_SizeChanged;
                keyboardService.VisibilityChanged += KeyboardService_VisibilityChanged;

            }
            //IsTimer = true;
        }


        async void ItemsSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // NewItems contains the item that was added.
            // If NewStartingIndex is not -1, then it contains the index where the new item was added.
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                InsertPage(Element?.ItemsSource.GetItem(e.NewStartingIndex), e.NewStartingIndex);
            }

            // OldItems contains the item that was removed.
            // If OldStartingIndex is not -1, then it contains the index where the old item was removed.
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                await RemovePage(e.OldStartingIndex);
            }

            // OldItems contains the moved item.
            // OldStartingIndex contains the index where the item was moved from.
            // NewStartingIndex contains the index where the item was moved to.
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                // Fix for #168 Android NullReferenceException
                var Source = ((PageAdapter)viewPager?.Adapter)?.Source;

                if (Element == null || viewPager == null || viewPager?.Adapter == null || Source == null) return;

                Source.RemoveAt(e.OldStartingIndex);
                Source.Insert(e.NewStartingIndex, e.OldItems[e.OldStartingIndex]);
                viewPager.Adapter?.NotifyDataSetChanged();

                SetArrowsVisibility();

                Element.SendPositionSelected();
                Element.PositionSelectedCommand?.Execute(null);
            }

            // NewItems contains the replacement item.
            // NewStartingIndex and OldStartingIndex are equal, and if they are not -1,
            // then they contain the index where the item was replaced.
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                // Fix for #168 Android NullReferenceException
                var Source = ((PageAdapter)viewPager?.Adapter)?.Source;

                if (Element == null || viewPager == null || viewPager?.Adapter == null || Source == null) return;

                Source[e.OldStartingIndex] = e.NewItems[e.NewStartingIndex];
                viewPager.Adapter?.NotifyDataSetChanged();
            }

            // No other properties are valid.
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                if (Element == null || viewPager == null) return;

                SetPosition();
                viewPager.Adapter = new PageAdapter(Element);
                viewPager.SetCurrentItem(Element.Position, false);
                SetArrowsVisibility();
                indicators?.SetViewPager(viewPager);
                Element.SendPositionSelected();
                Element.PositionSelectedCommand?.Execute(null);
            }
        }

        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
        }

        private void KeyboardService_VisibilityChanged(object sender, SoftKeyboardEventArgs e)
        {
            // The OnGlobalLayout method is calledd multiple times, so we have to store the previous state
            // and only do anything if the keyboard visibility is changed
            if (isKeyboardVisible != e.IsVisible)
            {
                isKeyboardVisible = e.IsVisible;
            }
        }

        void Element_SizeChanged(object sender, EventArgs e)
        {
            if (Element == null) return;

            // To avoid page recreation caused by entry focus #136 (fix)
            var rect = this.Element.Bounds;

            if (rect.Height > 0)
            {
                //ElementWidth = rect.Width;
                //ElementHeight = rect.Height;
                SetNativeView();
                Element.SendPositionSelected();
                Element.PositionSelectedCommand?.Execute(null);
            }
        }

        protected override void OnAttachedToWindow()
        {
            if (Control == null)
                Element_SizeChanged(Element, null);

            base.OnAttachedToWindow();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element == null || viewPager == null) return;

            var rect = this.Element.Bounds;

            switch (e.PropertyName)
            {
                case "Orientation":
                    orientationChanged = true;
                    SetNativeView();
                    Element.SendPositionSelected();
                    Element.PositionSelectedCommand?.Execute(null);
                    break;
                case "BackgroundColor":
                    viewPager.SetBackgroundColor(Element.BackgroundColor.ToAndroid());
                    break;
                case "IsSwipeEnabled":
                    SetIsSwipeEnabled();
                    break;
                case "IndicatorsTintColor":
                    indicators?.SetFillColor(Element.IndicatorsTintColor.ToAndroid());
                    break;
                case "CurrentPageIndicatorTintColor":
                    indicators?.SetPageColor(Element.CurrentPageIndicatorTintColor.ToAndroid());
                    break;
                case "IndicatorsShape":
                    indicators?.SetStyle(Element.IndicatorsShape);
                    break;
                case "ShowIndicators":
                    SetIndicators();
                    break;
                case "ItemsSource":
                    SetPosition();
                    viewPager.Adapter = new PageAdapter(Element);
                    viewPager.SetCurrentItem(Element.Position, false);
                    SetArrowsVisibility();
                    indicators?.SetViewPager(viewPager);
                    Element.SendPositionSelected();
                    Element.PositionSelectedCommand?.Execute(null);
                    if (Element.ItemsSource != null && Element.ItemsSource is INotifyCollectionChanged)
                        ((INotifyCollectionChanged)Element.ItemsSource).CollectionChanged += ItemsSource_CollectionChanged;
                    break;
                case "ItemTemplate":
                    viewPager.Adapter = new PageAdapter(Element);
                    viewPager.SetCurrentItem(Element.Position, false);
                    indicators?.SetViewPager(viewPager);
                    Element.SendPositionSelected();
                    Element.PositionSelectedCommand?.Execute(null);
                    break;
                case "Position":
                    if (!isChangingPosition)
                    {
                        SetCurrentPage(Element.Position);
                    }
                    break;
                case "ShowArrows":
                    SetArrows();
                    break;
                case "ArrowsBackgroundColor":
                    if (prevBtn == null || nextBtn == null) return;
                    prevBtn.SetBackgroundColor(Element.ArrowsBackgroundColor.ToAndroid());
                    nextBtn.SetBackgroundColor(Element.ArrowsBackgroundColor.ToAndroid());
                    break;
                case "ArrowsTintColor":
                    var prevArrow = nativeView.FindViewById<AWidget.ImageView>(Resource.Id.prevArrow);
                    prevArrow.SetColorFilter(Element.ArrowsTintColor.ToAndroid());
                    var nextArrow = nativeView.FindViewById<AWidget.ImageView>(Resource.Id.nextArrow);
                    nextArrow.SetColorFilter(Element.ArrowsTintColor.ToAndroid());
                    break;
                case "ArrowsTransparency":
                    if (prevBtn == null || nextBtn == null) return;
                    prevBtn.Alpha = Element.ArrowsTransparency;
                    nextBtn.Alpha = Element.ArrowsTransparency;
                    break;
            }
        }

        #region adapter callbacks

        bool setCurrentPageCalled;
        int pageScrolledCount;
        ScrollDirection direction;

        void ViewPager_PageScrolled(object sender, ViewPager.PageScrolledEventArgs e)
        {
            double percentCompleted;

            if (setCurrentPageCalled)
            {
                percentCompleted = pageScrolledCount * 100;
                pageScrolledCount++;
            }
            else
            {
                // e.PositionOffset is the %
                // if e.Position < currentPosition, it is scrolling to the left
                if (e.Position < Element.Position)
                {
                    percentCompleted = System.Math.Floor((1 - e.PositionOffset) * 100);
                    direction = Element.Orientation == CarouselViewOrientation.Horizontal ? ScrollDirection.Left : ScrollDirection.Up;
                }
                else
                {
                    percentCompleted = System.Math.Floor(e.PositionOffset * 100);
                    direction = Element.Orientation == CarouselViewOrientation.Horizontal ? ScrollDirection.Right : ScrollDirection.Down;
                }
            }

            // report % while the user is dragging or when SetCurrentPage has been called
            if (mViewPagerState == ViewPager.ScrollStateDragging || setCurrentPageCalled)
                Element.SendScrolled(percentCompleted, direction);

            // PageScrolled is called 2 times when SetCurrentPage is executed
            if (pageScrolledCount == 2)
            {
                setCurrentPageCalled = false;
                pageScrolledCount = 0;
            }
        }

        void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            // To avoid calling SetCurrentPage
            isChangingPosition = true;
            Element.Position = e.Position;
            isChangingPosition = false;
        }

        int mViewPagerState;

        void ViewPager_PageScrollStateChanged(object sender, ViewPager.PageScrollStateChangedEventArgs e)
        {
            // ScrollStateIdle = 0 : the pager is in Idle, settled state
            // ScrollStateDragging = 1 : the pager is currently being dragged by the user
            // ScrollStateSettling = 2 : the pager is in the process of settling to a final position

            mViewPagerState = e.State;

            // Call PositionSelected when scroll finish, after swiping finished and position > 0
            if (e.State == ViewPager.ScrollStateIdle)
            {
                SetArrowsVisibility();
                Element.SendPositionSelected();
                Element.PositionSelectedCommand?.Execute(null);
            }
        }

        #endregion

        void SetNativeView()
        {
            if (orientationChanged)
            {
                var inflater = AViews.LayoutInflater.From(_context);

                // Orientation BP
                if (Element.Orientation == CarouselViewOrientation.Horizontal)
                    nativeView = inflater.Inflate(Resource.Layout.horizontal_viewpager, null);
                else
                    nativeView = inflater.Inflate(Resource.Layout.vertical_viewpager, null);

                viewPager = nativeView.FindViewById<ViewPager>(Resource.Id.pager);

                orientationChanged = false;
            }

            viewPager.Adapter = new PageAdapter(Element);
            viewPager.SetCurrentItem(Element.Position, false);

            // InterPageSpacing BP
            var metrics = Resources.DisplayMetrics;
            var interPageSpacing = Element.InterPageSpacing * metrics.Density;
            viewPager.PageMargin = (int)interPageSpacing;

            // BackgroundColor BP
            viewPager.SetBackgroundColor(Element.BackgroundColor.ToAndroid());

            viewPager.PageSelected += ViewPager_PageSelected;
            viewPager.PageScrollStateChanged += ViewPager_PageScrollStateChanged;
            viewPager.PageScrolled += ViewPager_PageScrolled;

            // IsSwipeEnabled BP
            SetIsSwipeEnabled();

            // TapGestureRecognizer doesn't work when added to CarouselViewControl (Android) #66, #191, #200
            ((IViewPager)viewPager)?.SetElement(Element);

            SetNativeControl(nativeView);

            // ARROWS
            SetArrows();

            // INDICATORS
            indicators = nativeView.FindViewById<CirclePageIndicator>(Resource.Id.indicator);
            SetIndicators();
        }

        void SetIsSwipeEnabled()
        {
            ((IViewPager)viewPager)?.SetPagingEnabled(Element.IsSwipeEnabled);
        }

        void SetPosition()
        {
            isChangingPosition = true;
            if (Element.ItemsSource != null)
            {
                if (Element.Position > Element.ItemsSource.GetCount() - 1)
                    Element.Position = 0;
                if (Element.Position == -1)
                    Element.Position = 0;
            }
            else
            {
                Element.Position = 0;
            }
            isChangingPosition = false;

            if (indicators != null)
                indicators.mSnapPage = Element.Position;

            //Device.StartTimer(TimeSpan.FromSeconds(7), () =>
            //{
            //    if (IsTimer != false)
            //    {
            //        Device.BeginInvokeOnMainThread(() => Element.Position = (Element.Position != Element.ItemsSource?.GetCount() - 1) ? Element.Position + 1 : 0);
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //});
        }

        void SetArrows()
        {
            if (Element.ShowArrows)
            {
                if (prevBtn == null)
                {
                    prevBtn = nativeView.FindViewById<AWidget.LinearLayout>(Resource.Id.prev);
                    prevBtn.SetBackgroundColor(Element.ArrowsBackgroundColor.ToAndroid());
                    prevBtn.Visibility = Element.Position == 0 || Element.ItemsSource.GetCount() == 0 ? AViews.ViewStates.Gone : AViews.ViewStates.Visible;
                    prevBtn.Alpha = Element.ArrowsTransparency;

                    var prevArrow = nativeView.FindViewById<AWidget.ImageView>(Resource.Id.prevArrow);
                    prevArrow.SetColorFilter(Element.ArrowsTintColor.ToAndroid());

                    prevBtn.Click += PrevBtn_Click;
                }

                if (nextBtn == null)
                {
                    nextBtn = nativeView.FindViewById<AWidget.LinearLayout>(Resource.Id.next);
                    nextBtn.SetBackgroundColor(Element.ArrowsBackgroundColor.ToAndroid());
                    nextBtn.Visibility = Element.Position == Element.ItemsSource.GetCount() - 1 || Element.ItemsSource.GetCount() == 0 ? AViews.ViewStates.Gone : AViews.ViewStates.Visible;
                    nextBtn.Alpha = Element.ArrowsTransparency;

                    var nextArrow = nativeView.FindViewById<AWidget.ImageView>(Resource.Id.nextArrow);
                    nextArrow.SetColorFilter(Element.ArrowsTintColor.ToAndroid());

                    nextBtn.Click += NextBtn_Click;
                }
            }
            else
            {
                if (prevBtn == null || nextBtn == null) return;
                prevBtn.Visibility = AViews.ViewStates.Gone;
                nextBtn.Visibility = AViews.ViewStates.Gone;
            }
        }

        void PrevBtn_Click(object sender, EventArgs e)
        {
            if (Element.Position > 0)
            {
                Element.Position = Element.Position - 1;
                direction = Element.Orientation == CarouselViewOrientation.Horizontal ? ScrollDirection.Left : ScrollDirection.Up;
            }
        }

        void NextBtn_Click(object sender, EventArgs e)
        {
            if (Element.Position < Element.ItemsSource.GetCount() - 1)
            {
                Element.Position = Element.Position + 1;
                direction = Element.Orientation == CarouselViewOrientation.Horizontal ? ScrollDirection.Right : ScrollDirection.Down;
            }
        }

        void SetArrowsVisibility()
        {
            if (prevBtn == null || nextBtn == null) return;
            prevBtn.Visibility = Element.Position == 0 || Element.ItemsSource.GetCount() == 0 ? AViews.ViewStates.Gone : AViews.ViewStates.Visible;
            nextBtn.Visibility = Element.Position == Element.ItemsSource.GetCount() - 1 || Element.ItemsSource.GetCount() == 0 ? AViews.ViewStates.Gone : AViews.ViewStates.Visible;
        }

        void SetIndicators()
        {
            if (Element.ShowIndicators)
            {
                SetPosition();

                indicators?.SetViewPager(viewPager);

                // IndicatorsTintColor BP
                indicators?.SetFillColor(Element.IndicatorsTintColor.ToAndroid());

                // CurrentPageIndicatorTintColor BP
                indicators?.SetPageColor(Element.CurrentPageIndicatorTintColor.ToAndroid());

                // IndicatorsShape BP
                indicators?.SetStyle(Element.IndicatorsShape); // Rounded or Squared
            }
            else
            {
                indicators?.RemoveAllViews();
            }

            // ShowIndicators BP
            if (indicators != null)
                indicators.Visibility = Element.ShowIndicators ? AViews.ViewStates.Visible : AViews.ViewStates.Gone;
        }

        void InsertPage(object item, int position)
        {
            // Fix for #168 Android NullReferenceException
            var Source = ((PageAdapter)viewPager?.Adapter)?.Source;

            if (Element == null || viewPager == null || viewPager?.Adapter == null || Source == null) return;

            Source.Insert(position, item);

            viewPager.Adapter.NotifyDataSetChanged();

            SetArrowsVisibility();
            indicators?.SetViewPager(viewPager);

            Element.SendPositionSelected();
            Element.PositionSelectedCommand?.Execute(null);
        }

        async Task RemovePage(int position)
        {
            // Fix for #168 Android NullReferenceException
            var Source = ((PageAdapter)viewPager?.Adapter)?.Source;

            if (Element == null || viewPager == null || viewPager?.Adapter == null || Source == null) return;

            if (Source?.Count > 0)
            {
                isChangingPosition = true;

                // To remove current page
                if (position == Element.Position)
                {
                    var newPos = position - 1;
                    if (newPos == -1)
                        newPos = 0;

                    if (position == 0)
                        // Move to next page
                        viewPager.SetCurrentItem(1, Element.AnimateTransition);
                    else
                        // Move to previous page
                        viewPager.SetCurrentItem(newPos, Element.AnimateTransition);

                    // With a swipe transition
                    if (Element.AnimateTransition)
                        await Task.Delay(100);

                    Element.Position = newPos;
                }

                Source.RemoveAt(position);

                viewPager.Adapter.NotifyDataSetChanged();

                SetArrowsVisibility();
                indicators?.SetViewPager(viewPager);

                isChangingPosition = false;
            }
        }
        void SetCurrentPage(int position)
        {
            if (position < 0 || position > Element.ItemsSource?.GetCount() - 1)
                return;

            setCurrentPageCalled = true;

            if (Element == null || viewPager == null || Element.ItemsSource == null) return;

            if (Element.ItemsSource?.GetCount() > 0)
            {
                viewPager.SetCurrentItem(position, Element.AnimateTransition);

                SetArrowsVisibility();

                // Invoke PositionSelected when AnimateTransition is disabled
                if (!Element.AnimateTransition)
                {
                    Element.SendPositionSelected();
                    Element.PositionSelectedCommand?.Execute(null);
                }

                 
            }
        }

        #region adapter

        class PageAdapter : PagerAdapter
        {
            CarouselViewControl Element;

            public List<object> Source;

            public PageAdapter(CarouselViewControl element)
            {
                Element = element;
                Source = Element.ItemsSource != null ? new List<object>(Element.ItemsSource.GetList()) : null;
            }

            public override int Count
            {
                get
                {
                    return Source?.Count ?? 0;
                }
            }

            public override bool IsViewFromObject(AViews.View view, Java.Lang.Object @object)
            {
                return view == @object;
            }

            public override Java.Lang.Object InstantiateItem(AViews.ViewGroup container, int position)
            {
                Xamarin.Forms.View formsView = null;

                object bindingContext = null;

                if (Source != null && Source?.Count > 0)
                    bindingContext = Source.Cast<object>().ElementAt(position);

                var dt = bindingContext as DataTemplate;
                var view = bindingContext as Xamarin.Forms.View;

                // Support for List<DataTemplate> as ItemsSource
                if (dt != null)
                {
                    formsView = (Xamarin.Forms.View)dt.CreateContent();
                }
                else
                {
                    if (view != null)
                    {
                        formsView = view;
                    }
                    else
                    {
                        var selector = Element.ItemTemplate as DataTemplateSelector;
                        if (selector != null)
                            formsView = (Xamarin.Forms.View)selector.SelectTemplate(bindingContext, Element).CreateContent();
                        else
                            formsView = (Xamarin.Forms.View)Element.ItemTemplate.CreateContent();

                        formsView.BindingContext = bindingContext;
                    }
                }

                // HeightRequest fix
                formsView.Parent = this.Element;

                var nativeConverted = formsView.ToAndroid(new Rectangle(0, 0, Element.Width, Element.Height));
                nativeConverted.Tag = new Tag() { BindingContext = bindingContext }; //position;

                //nativeConverted.SaveEnabled = true;
                //nativeConverted.RestoreHierarchyState(mViewStates);

                //if (dt == null && view == null)
                //formsView.Parent = null;

                var pager = (ViewPager)container;
                pager.AddView(nativeConverted);

                return nativeConverted;
            }

            public override void DestroyItem(AViews.ViewGroup container, int position, Java.Lang.Object @object)
            {
                var pager = (ViewPager)container;
                var view = (AViews.View)@object;
                //view.SaveEnabled = true;
                //view.SaveHierarchyState(mViewStates);
                pager.RemoveView(view);
                //[Android] Out of memories(FFImageLoading + CarouselView) #279
                view.Dispose();
            }

            public override int GetItemPosition(Java.Lang.Object @object)
            {
                var tag = (Tag)((AViews.View)@object).Tag;
                var position = Source.IndexOf(tag.BindingContext);
                return position != -1 ? position : PositionNone;
            }
        }

        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                if (prevBtn != null)
                {
                    prevBtn.Click -= PrevBtn_Click;
                    prevBtn.Dispose();
                    prevBtn = null;
                }

                if (nextBtn != null)
                {
                    nextBtn.Click -= NextBtn_Click;
                    nextBtn.Dispose();
                    nextBtn = null;
                }

                if (indicators != null)
                {
                    indicators.Dispose();
                    indicators = null;
                }

                if (viewPager != null)
                {
                    viewPager.PageSelected -= ViewPager_PageSelected;
                    viewPager.PageScrollStateChanged -= ViewPager_PageScrollStateChanged;

                    if (viewPager.Adapter != null)
                        viewPager.Adapter.Dispose();

                    viewPager.Dispose();
                    viewPager = null;
                }

                if (Element != null)
                {
                    Element.SizeChanged -= Element_SizeChanged;
                    if (Element.ItemsSource != null && Element.ItemsSource is INotifyCollectionChanged)
                        ((INotifyCollectionChanged)Element.ItemsSource).CollectionChanged -= ItemsSource_CollectionChanged;
                }

                _disposed = true;
            }

            try
            {
                //IsTimer = false;
                base.Dispose(disposing);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}

namespace Mobile.Droid.CarouselViewStuff
{
    internal class GlobalLayoutListener : Object, ViewTreeObserver.IOnGlobalLayoutListener
    {
        private static InputMethodManager _inputManager;
        private readonly SoftKeyboardService _softwareKeyboardService;

        private static void ObtainInputManager()
        {
            _inputManager = (InputMethodManager)Android.App.Application.Context.GetSystemService(Context.InputMethodService);
        }

        public GlobalLayoutListener(SoftKeyboardService softwareKeyboardService)
        {
            _softwareKeyboardService = softwareKeyboardService;
            ObtainInputManager();
        }

        public void OnGlobalLayout()
        {
            if (_inputManager.Handle == IntPtr.Zero)
            {
                ObtainInputManager();
            }

            _softwareKeyboardService.InvokeVisibilityChanged(new SoftKeyboardEventArgs(_inputManager.IsAcceptingText));
        }
    }

    public class SoftKeyboardEventArgs : EventArgs
    {
        public SoftKeyboardEventArgs(bool isVisible)
        {
            IsVisible = isVisible;
        }

        public bool IsVisible { get; private set; }
    }

    public class SoftKeyboardService : SoftKeyboardServiceBase
    {
        private readonly Activity _activity;
        private GlobalLayoutListener _globalLayoutListener;

        public SoftKeyboardService(Activity activity)
        {
            _activity = activity;
            if (_activity == null)
                throw new System.Exception("Activity can't be null!");
        }

        public override event EventHandler<SoftKeyboardEventArgs> VisibilityChanged
        {
            add
            {
                base.VisibilityChanged += value;
                CheckListener();
            }
            remove { base.VisibilityChanged -= value; }
        }

        private void CheckListener()
        {
            if (_globalLayoutListener == null)
            {
                _globalLayoutListener = new GlobalLayoutListener(this);
                _activity.Window.DecorView.ViewTreeObserver.AddOnGlobalLayoutListener(_globalLayoutListener);
            }
        }
    }

    public abstract class SoftKeyboardServiceBase
    {
        public virtual event EventHandler<SoftKeyboardEventArgs> VisibilityChanged;

        public void InvokeVisibilityChanged(SoftKeyboardEventArgs args)
        {
            VisibilityChanged?.Invoke(this, args);
        }
    }

    public static class ViewExtensions
    {
        public static AViews.View ToAndroid(this Xamarin.Forms.View view, Rectangle size)
        {
            //var vRenderer = RendererFactory.GetRenderer (view);

            // NullReferenceException during swiping #314 (ScrollView)
            if (Platform.GetRenderer(view) == null || Platform.GetRenderer(view)?.Tracker == null)
                Platform.SetRenderer(view, Platform.CreateRendererWithContext(view, MainActivity.Instance));

            var vRenderer = Platform.GetRenderer(view);

            var viewGroup = vRenderer.View;

            vRenderer.Tracker?.UpdateLayout();
            var layoutParams = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            viewGroup.LayoutParameters = layoutParams;
            view.Layout(size);
            viewGroup.Layout(0, 0, (int)view.WidthRequest, (int)view.HeightRequest);
            return viewGroup;
        }
    }

    public class Tag : Java.Lang.Object
    {
        public object BindingContext { get; set; }
    }

    public interface IViewPager
    {
        void SetPagingEnabled(bool enabled);
        void SetElement(CarouselViewControl element);
    }

    public class HorizontalViewPager : ViewPager, IViewPager
    {
        private bool isSwipeEnabled = true;
        private CarouselViewControl Element;

        public HorizontalViewPager(IntPtr intPtr, JniHandleOwnership jni) : base(intPtr, jni)
        {
        }

        public HorizontalViewPager(Context context) : base(context, null)
        {
        }

        public HorizontalViewPager(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (ev.Action == MotionEventActions.Up)
            {
                if (Element?.GestureRecognizers.GetCount() > 0)
                {
                    var gesture = Element.GestureRecognizers.First() as TapGestureRecognizer;
                    if (gesture != null)
                        gesture.Command?.Execute(gesture.CommandParameter);
                }
            }

            if (this.isSwipeEnabled)
            {
                return base.OnInterceptTouchEvent(ev);
            }

            return false;
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            if (this.isSwipeEnabled)
            {
                return base.OnTouchEvent(e);
            }

            return false;
        }

        public void SetPagingEnabled(bool enabled)
        {
            this.isSwipeEnabled = enabled;
        }

        public void SetElement(CarouselViewControl element)
        {
            this.Element = element;
        }
    }

    public class VerticalViewPager : ViewPager, IViewPager
    {
        private bool isSwipingEnabled = true;
        private CarouselViewControl Element;

        public VerticalViewPager(Context context) : base(context, null)
        {
        }

        public VerticalViewPager(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init();
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (ev.Action == MotionEventActions.Up)
            {
                if (Element?.GestureRecognizers.GetCount() > 0)
                {
                    var gesture = Element.GestureRecognizers.First() as Xamarin.Forms.TapGestureRecognizer;
                    if (gesture != null)
                        gesture.Command?.Execute(gesture.CommandParameter);
                }
            }

            if (this.isSwipingEnabled)
            {
                var toIntercept = base.OnInterceptTouchEvent(flipXY(ev));
                // Return MotionEvent to normal
                flipXY(ev);
                return toIntercept;
            }

            return false;
        }
        public override bool OnTouchEvent(MotionEvent ev)
        {
            if (this.isSwipingEnabled)
            {
                var toHandle = base.OnTouchEvent(flipXY(ev));
                //Return MotionEvent to normal
                flipXY(ev);
                return toHandle;
            }

            return false;
        }

        public void SetPagingEnabled(bool enabled)
        {
            this.isSwipingEnabled = enabled;
        }

        public void SetElement(CarouselViewControl element)
        {
            this.Element = element;
        }

        public override bool CanScrollHorizontally(int direction)
        {
            return true;
        }

        private MotionEvent flipXY(MotionEvent ev)
        {
            var width = Width;
            var height = Height;
            var x = (ev.GetY() / height) * width;
            var y = (ev.GetX() / width) * height;
            ev.SetLocation(x, y);
            return ev;
        }

        public override bool CanScrollVertically(int direction)
        {
            return base.CanScrollHorizontally(direction);
        }

        private void Init()
        {
            // Make page transit vertical
            SetPageTransformer(true, new VerticalPageTransformer());
            // Get rid of the overscroll drawing that happens on the left and right (the ripple)
            OverScrollMode = OverScrollMode.Never;
        }

        private class VerticalPageTransformer : Java.Lang.Object, ViewPager.IPageTransformer
        {
            public void TransformPage(Android.Views.View view, float position)
            {
                var pageWidth = view.Width;
                var pageHeight = view.Height;

                if (position < -1)
                {
                    // This page is way off-screen to the left.
                    view.Alpha = 0;
                }
                else if (position <= 1)
                {
                    view.Alpha = 1;
                    // Counteract the default slide transition
                    view.TranslationX = pageWidth * -position;
                    // set Y position to swipe in from top
                    float yPosition = position * pageHeight;
                    view.TranslationY = yPosition;
                }
                else
                {
                    // This page is way off-screen to the right.
                    view.Alpha = 0;
                }
            }
        }
    }

    public class CirclePageIndicator : ViewGroup, PageIndicator
    {
        const int HORIZONTAL = 0;
        const int VERTICAL = 1;
        private float mRadius;
        private Paint mPaintPageFill;
        private Paint mPaintStroke;
        private Paint mPaintFill;
        private ViewPager mViewPager;
        private ViewPager.IOnPageChangeListener mListener;
        private int mCurrentPage;
        public int mSnapPage;
        private int mCurrentOffset;
        private int mScrollState;
        private int mPageSize;
        private int mOrientation;
        private bool mCentered;
        private bool mSnap;

        private IndicatorsShape indicatorsStyle = IndicatorsShape.Circle;

        public CirclePageIndicator(Context context) : this(context, null)
        {
        }

        public CirclePageIndicator(Context context, IAttributeSet attrs) : this(context, attrs, LahmaOnline.Droid.Resource.Attribute.vpiCirclePageIndicatorStyle)
        {
        }

        public CirclePageIndicator(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
            //Load defaults from resources
            var res = Resources;
            int defaultPageColor = ContextCompat.GetColor(context, LahmaOnline.Droid.Resource.Color.default_circle_indicator_page_color);
            int defaultFillColor = ContextCompat.GetColor(context, LahmaOnline.Droid.Resource.Color.default_circle_indicator_fill_color);
            int defaultOrientation = res.GetInteger(LahmaOnline.Droid.Resource.Integer.default_circle_indicator_orientation);
            int defaultStrokeColor = ContextCompat.GetColor(context, LahmaOnline.Droid.Resource.Color.default_circle_indicator_stroke_color);
            float defaultStrokeWidth = res.GetDimension(LahmaOnline.Droid.Resource.Dimension.default_circle_indicator_stroke_width);
            float defaultRadius = res.GetDimension(LahmaOnline.Droid.Resource.Dimension.default_circle_indicator_radius);
            bool defaultCentered = res.GetBoolean(LahmaOnline.Droid.Resource.Boolean.default_circle_indicator_centered);
            bool defaultSnap = res.GetBoolean(LahmaOnline.Droid.Resource.Boolean.default_circle_indicator_snap);

            //Retrieve styles attributes
            var a = context.ObtainStyledAttributes(attrs, LahmaOnline.Droid.Resource.Styleable.CirclePageIndicator, defStyle, LahmaOnline.Droid.Resource.Style.Widget_CirclePageIndicator);

            mCentered = a.GetBoolean(LahmaOnline.Droid.Resource.Styleable.CirclePageIndicator_vpiCentered, defaultCentered);
            mOrientation = a.GetInt(LahmaOnline.Droid.Resource.Styleable.CirclePageIndicator_vpiOrientation, defaultOrientation);
            mPaintPageFill = new Paint(PaintFlags.AntiAlias);
            mPaintPageFill.SetStyle(Paint.Style.Fill);
            mPaintPageFill.Color = a.GetColor(LahmaOnline.Droid.Resource.Styleable.CirclePageIndicator_vpiPageColor, defaultPageColor);
            mPaintStroke = new Paint(PaintFlags.AntiAlias);
            mPaintStroke.SetStyle(Paint.Style.Stroke);
            mPaintFill = new Paint(PaintFlags.AntiAlias);
            mPaintFill.SetStyle(Paint.Style.Fill);
            mSnap = a.GetBoolean(LahmaOnline.Droid.Resource.Styleable.CirclePageIndicator_vpiSnap, defaultSnap);

            mRadius = a.GetDimension(LahmaOnline.Droid.Resource.Styleable.CirclePageIndicator_vpiRadius, defaultRadius);
            mPaintFill.Color = a.GetColor(LahmaOnline.Droid.Resource.Styleable.CirclePageIndicator_vpiFillColor, defaultFillColor);
            mPaintStroke.Color = a.GetColor(LahmaOnline.Droid.Resource.Styleable.CirclePageIndicator_vpiStrokeColor, defaultStrokeColor);
            mPaintStroke.StrokeWidth = a.GetDimension(LahmaOnline.Droid.Resource.Styleable.CirclePageIndicator_vpiStrokeWidth, defaultStrokeWidth);

            a.Recycle();

        }

        public void SetFillColor(Android.Graphics.Color fillColor)
        {
            mPaintPageFill.Color = fillColor;
            Invalidate();

        }

        public void SetPageColor(Android.Graphics.Color pageColor)
        {
            mPaintFill.Color = pageColor;
            Invalidate();
        }

        public void SetStyle(IndicatorsShape style)
        {
            indicatorsStyle = style;
            Invalidate();
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            if (mViewPager == null)
            {
                return;
            }
            // Fix for NullReferenceException on Android tabbed page #67
            int count = mViewPager?.Adapter?.Count ?? 0;
            if (count == 0)
            {
                return;
            }

            if (mCurrentPage >= count)
            {
                SetCurrentItem(count - 1);
                return;
            }

            int longSize;
            int longPaddingBefore;
            int longPaddingAfter;
            int shortPaddingBefore;
            if (mOrientation == HORIZONTAL)
            {
                longSize = Width;
                longPaddingBefore = PaddingLeft;
                longPaddingAfter = PaddingRight;
                shortPaddingBefore = PaddingTop;
            }
            else
            {
                longSize = Height;
                longPaddingBefore = PaddingTop;
                longPaddingAfter = PaddingBottom;
                shortPaddingBefore = PaddingLeft;
            }

            float threeRadius = mRadius * 4; // dots separation
            float shortOffset = shortPaddingBefore + mRadius;
            float longOffset = longPaddingBefore + mRadius;
            if (mCentered)
            {
                longOffset += ((longSize - longPaddingBefore - longPaddingAfter) / 2.0f) - ((count * threeRadius) / 2.0f);
            }

            float dX;
            float dY;

            float pageFillRadius = mRadius;
            if (mPaintStroke.StrokeWidth > 0)
            {
                pageFillRadius -= mPaintStroke.StrokeWidth / 2.0f;
            }

            //Draw stroked circles
            for (int iLoop = 0; iLoop < count; iLoop++)
            {
                float drawLong = longOffset + (iLoop * threeRadius);
                if (mOrientation == HORIZONTAL)
                {
                    dX = drawLong;
                    dY = shortOffset;
                }
                else
                {
                    dX = shortOffset;
                    dY = drawLong;
                }

                // Only paint fill if not completely transparent
                if (mPaintPageFill.Alpha > 0)
                {
                    switch (indicatorsStyle)
                    {
                        case IndicatorsShape.Square:
                            canvas.DrawRect(dX, dY, dX + (pageFillRadius * 2), dY + (pageFillRadius * 2), mPaintPageFill);
                            break;
                        default:
                            canvas.DrawCircle(dX, dY, pageFillRadius, mPaintPageFill);
                            break;
                    }
                }

                // Only paint stroke if a stroke width was non-zero
                /*if (pageFillRadius != mRadius)
				{
					switch (indicatorsStyle)
					{
						case IndicatorsShape.Square:
							canvas.DrawRect(dX, dY, dX + (this.mRadius * 2), dY + (this.mRadius * 2), mPaintStroke);
							break;
						default:
							canvas.DrawCircle(dX, dY, mRadius, mPaintStroke);
							break;
					}
				}*/
            }

            //Draw the filled circle according to the current scroll
            float cx = (mSnap ? mSnapPage : mCurrentPage) * threeRadius;
            if (!mSnap && (mPageSize != 0))
            {
                cx += (mCurrentOffset * 1.0f / mPageSize) * threeRadius;
            }
            if (mOrientation == HORIZONTAL)
            {
                dX = longOffset + cx;
                dY = shortOffset;
            }
            else
            {
                dX = shortOffset;
                dY = longOffset + cx;
            }

            switch (indicatorsStyle)
            {
                case IndicatorsShape.Square:
                    canvas.DrawRect(dX, dY, dX + (this.mRadius * 2), dY + (this.mRadius * 2), mPaintFill);
                    break;
                default:
                    canvas.DrawCircle(dX, dY, mRadius, mPaintFill);
                    break;
            }
        }

        public void SetViewPager(ViewPager view)
        {
            if (view.Adapter == null)
            {
                //throw new IllegalStateException ("ViewPager does not have adapter instance.");
            }
            mViewPager = view;
            mViewPager.AddOnPageChangeListener(this);//SetOnPageChangeListener (this);
            UpdatePageSize();
            Invalidate();
        }

        private void UpdatePageSize()
        {
            if (mViewPager != null)
            {
                mPageSize = (mOrientation == HORIZONTAL) ? mViewPager.Width : mViewPager.Height;
            }
        }

        public void SetViewPager(ViewPager view, int initialPosition)
        {
            SetViewPager(view);
            SetCurrentItem(initialPosition);
        }

        public void SetCurrentItem(int item)
        {
            if (mViewPager == null)
            {
                throw new IllegalStateException("ViewPager has not been bound.");
            }
            mViewPager.CurrentItem = item;
            mCurrentPage = item;
            Invalidate();
        }

        public void NotifyDataSetChanged()
        {
            Invalidate();
        }

        public void OnPageScrollStateChanged(int state)
        {
            mScrollState = state;

            if (mListener != null)
            {
                mListener.OnPageScrollStateChanged(state);
            }
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
            mCurrentPage = position;
            mCurrentOffset = positionOffsetPixels;
            UpdatePageSize();
            Invalidate();

            if (mListener != null)
            {
                mListener.OnPageScrolled(position, positionOffset, positionOffsetPixels);
            }
        }

        public void OnPageSelected(int position)
        {
            if (mSnap || mScrollState == ViewPager.ScrollStateIdle)
            {
                mCurrentPage = position;
                mSnapPage = position;
                Invalidate();
            }

            if (mListener != null)
            {
                mListener.OnPageSelected(position);
            }
        }

        public void SetOnPageChangeListener(ViewPager.IOnPageChangeListener listener)
        {
            mListener = listener;
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            if (mOrientation == HORIZONTAL)
            {
                SetMeasuredDimension(MeasureLong(widthMeasureSpec), MeasureShort(heightMeasureSpec));
            }
            else
            {
                SetMeasuredDimension(MeasureShort(widthMeasureSpec), MeasureLong(heightMeasureSpec));
            }
        }

        /**
	     * Determines the width of this view
	     *
	     * @param measureSpec
	     *            A measureSpec packed into an int
	     * @return The width of the view, honoring constraints from measureSpec
	     */
        private int MeasureLong(int measureSpec)
        {
            int result = 0;
            var specMode = MeasureSpec.GetMode(measureSpec);
            var specSize = MeasureSpec.GetSize(measureSpec);

            if ((specMode == MeasureSpecMode.Exactly) || (mViewPager == null) || (mViewPager.Adapter == null))
            {
                //We were told how big to be
                result = specSize;
            }
            else
            {
                //Calculate the width according the views count
                int count = mViewPager.Adapter.Count;
                result = (int)(PaddingLeft + PaddingRight
                        + (count * 2 * mRadius) + (count - 1) * mRadius + 1);
                //Respect AT_MOST value if that was what is called for by measureSpec
                if (specMode == MeasureSpecMode.AtMost)
                {
                    result = Java.Lang.Math.Min(result, specSize);
                }
            }
            return result;
        }

        /**
	     * Determines the height of this view
	     *
	     * @param measureSpec
	     *            A measureSpec packed into an int
	     * @return The height of the view, honoring constraints from measureSpec
	     */
        private int MeasureShort(int measureSpec)
        {
            int result = 0;
            var specMode = MeasureSpec.GetMode(measureSpec);
            var specSize = MeasureSpec.GetSize(measureSpec);

            if (specMode == MeasureSpecMode.Exactly)
            {
                //We were told how big to be
                result = specSize;
            }
            else
            {
                //Measure the height
                result = (int)(2 * mRadius + PaddingTop + PaddingBottom + 1);
                //Respect AT_MOST value if that was what is called for by measureSpec
                if (specMode == MeasureSpecMode.AtMost)
                {
                    result = Java.Lang.Math.Min(result, specSize);
                }
            }
            return result;
        }

        protected override void OnRestoreInstanceState(IParcelable state)
        {
            try
            {
                var bundle = state as Bundle;
                if (bundle != null)
                {
                    var superState = (IParcelable)bundle.GetParcelable("base");
                    if (superState != null)
                        base.OnRestoreInstanceState(superState);
                    mCurrentPage = bundle.GetInt("mCurrentPage", 0);
                    mSnapPage = bundle.GetInt("mCurrentPage", 0);
                }
            }
            catch
            {
                base.OnRestoreInstanceState(state);
                // Ignore, this needs to support IParcelable...
            }

            RequestLayout();
        }

        protected override IParcelable OnSaveInstanceState()
        {
            var superState = base.OnSaveInstanceState();
            var state = new Bundle();
            state.PutParcelable("base", superState);
            state.PutInt("mCurrentPage", mCurrentPage);

            return state;
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            //throw new NotImplementedException(
        }
    }

    public interface PageIndicator : ViewPager.IOnPageChangeListener
    {
        void SetViewPager(ViewPager view);

        void SetViewPager(ViewPager view, int initialPosition);

        void SetCurrentItem(int item);

        void SetOnPageChangeListener(ViewPager.IOnPageChangeListener listener);

        void NotifyDataSetChanged();
    }

}