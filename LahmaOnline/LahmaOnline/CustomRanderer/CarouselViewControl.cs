using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Timers;
using Xamarin.Forms;

namespace LahmaOnline.CustomRenderer
{
	public class CarouselViewControl : View
	{
		public static readonly BindableProperty OrientationProperty = BindableProperty.Create("Orientation", typeof(CarouselViewOrientation), typeof(CarouselViewControl), CarouselViewOrientation.Horizontal);
		public static readonly BindableProperty InterPageSpacingProperty = BindableProperty.Create("InterPageSpacing", typeof(int), typeof(CarouselViewControl), 0);
		public static readonly BindableProperty IsSwipeEnabledProperty = BindableProperty.Create("IsSwipeEnabled", typeof(bool), typeof(CarouselViewControl), true);
		public static readonly BindableProperty IndicatorsTintColorProperty = BindableProperty.Create("IndicatorsTintColor", typeof(Color), typeof(CarouselViewControl), Color.White);
		public static readonly BindableProperty CurrentPageIndicatorTintColorProperty = BindableProperty.Create("CurrentPageIndicatorTintColor", typeof(Color), typeof(CarouselViewControl), Color.FromHex("#2196F3"));
		public static readonly BindableProperty IndicatorsShapeProperty = BindableProperty.Create("IndicatorsShape", typeof(IndicatorsShape), typeof(CarouselViewControl), IndicatorsShape.Circle);
		public static readonly BindableProperty ShowIndicatorsProperty = BindableProperty.Create("ShowIndicators", typeof(bool), typeof(CarouselViewControl), true);
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(CarouselViewControl), null);
		public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create("ItemTemplate", typeof(DataTemplate), typeof(CarouselViewControl), null);
		public static readonly BindableProperty PositionProperty = BindableProperty.Create("Position", typeof(int), typeof(CarouselViewControl), 0, BindingMode.TwoWay);
		public static readonly BindableProperty AnimateTransitionProperty = BindableProperty.Create("AnimateTransition", typeof(bool), typeof(CarouselViewControl), true);
		public static readonly BindableProperty ShowArrowsProperty = BindableProperty.Create("ShowArrows", typeof(bool), typeof(CarouselViewControl), false);
		public static readonly BindableProperty ArrowsBackgroundColorProperty = BindableProperty.Create("ArrowsBackgroundColor", typeof(Color), typeof(CarouselViewControl), Color.Black);
		public static readonly BindableProperty ArrowsTintColorProperty = BindableProperty.Create("ArrowsTintColor", typeof(Color), typeof(CarouselViewControl), Color.White);
		public static readonly BindableProperty ArrowsTransparencyProperty = BindableProperty.Create("ArrowsTransparency", typeof(float), typeof(CarouselViewControl), 0.5f);
		public static readonly BindableProperty PositionSelectedCommandProperty = BindableProperty.Create("PositionSelectedCommand", typeof(Command), typeof(CarouselViewControl), null, BindingMode.Default, (bindable, value) => { return true; });

		public CarouselViewOrientation Orientation
		{
			get { return (CarouselViewOrientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		public int InterPageSpacing
		{
			get { return (int)GetValue(InterPageSpacingProperty); }
			set { SetValue(InterPageSpacingProperty, value); }
		}

		public bool IsSwipeEnabled
		{
			get { return (bool)GetValue(IsSwipeEnabledProperty); }
			set { SetValue(IsSwipeEnabledProperty, value); }
		}

		public Color IndicatorsTintColor
		{
			get { return (Color)GetValue(IndicatorsTintColorProperty); }
			set { SetValue(IndicatorsTintColorProperty, value); }
		}

		public Color CurrentPageIndicatorTintColor
		{
			get { return (Color)GetValue(CurrentPageIndicatorTintColorProperty); }
			set { SetValue(CurrentPageIndicatorTintColorProperty, value); }
		}

		public IndicatorsShape IndicatorsShape
		{
			get { return (IndicatorsShape)GetValue(IndicatorsShapeProperty); }
			set { SetValue(IndicatorsShapeProperty, value); }
		}

		public bool ShowIndicators
		{
			get { return (bool)GetValue(ShowIndicatorsProperty); }
			set { SetValue(ShowIndicatorsProperty, value); }
		}

		public IEnumerable ItemsSource
		{
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}

		public int Position
		{
			get { return (int)GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}

		public bool AnimateTransition
		{
			get { return (bool)GetValue(AnimateTransitionProperty); }
			set { SetValue(AnimateTransitionProperty, value); }
		}

		public bool ShowArrows
		{
			get { return (bool)GetValue(ShowArrowsProperty); }
			set { SetValue(ShowArrowsProperty, value); }
		}

		public Color ArrowsBackgroundColor
		{
			get { return (Color)GetValue(ArrowsBackgroundColorProperty); }
			set { SetValue(ArrowsBackgroundColorProperty, value); }
		}

		public Color ArrowsTintColor
		{
			get { return (Color)GetValue(ArrowsTintColorProperty); }
			set { SetValue(ArrowsTintColorProperty, value); }
		}

		public float ArrowsTransparency
		{
			get { return (float)GetValue(ArrowsTransparencyProperty); }
			set { SetValue(ArrowsTransparencyProperty, value); }
		}

		public Command PositionSelectedCommand
		{
			get { return (Command)GetValue(PositionSelectedCommandProperty); }
			set { SetValue(PositionSelectedCommandProperty, value); }
		}

		public event EventHandler<PositionSelectedEventArgs> PositionSelected;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendPositionSelected()
		{
			PositionSelected?.Invoke(this, new PositionSelectedEventArgs { NewValue = this.Position });
		}

		public event EventHandler<ScrolledEventArgs> Scrolled;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendScrolled(double percent, ScrollDirection direction)
		{
			Scrolled?.Invoke(this, new ScrolledEventArgs { NewValue = percent, Direction = direction });
		}
    }

	public class PositionSelectedEventArgs : EventArgs
	{
		public int NewValue { get; set; }
	}

	public class ScrolledEventArgs : EventArgs
	{
		public double NewValue { get; set; }
		public ScrollDirection Direction { get; set; }
	}

	public enum CarouselViewOrientation
	{
		Horizontal,
		Vertical
	}

	public static class IEnumerableExtensions
	{
		public static object GetItem(this IEnumerable e, int index)
		{
			var enumerator = e.GetEnumerator();
			int i = 0;
			while (enumerator.MoveNext())
			{
				if (i == index)
					return enumerator.Current;
				i++;
			}
			return null;
		}

		public static int GetCount(this IEnumerable e)
		{
			var enumerator = e.GetEnumerator();
			int i = 0;
			while (enumerator.MoveNext())
			{
				i++;
			}
			return i;
		}

		public static List<object> GetList(this IEnumerable e)
		{
			var enumerator = e.GetEnumerator();
			var list = new List<object>();
			while (enumerator.MoveNext())
			{
				list.Add(enumerator.Current);
			}
			return list;
		}
	}

	public enum IndicatorsShape
	{
		Circle,
		Square
	}

	public enum ScrollDirection
	{
		Left,
		Right,
		Up,
		Down
	}
}
