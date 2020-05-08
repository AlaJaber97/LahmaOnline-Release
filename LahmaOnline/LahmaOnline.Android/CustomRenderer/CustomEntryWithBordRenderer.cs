using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using LahmaOnline.CustomRenderer;
using LahmaOnline.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(CustomEntryWithBord), typeof(CustomEntryWithBordRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace LahmaOnline.Droid.CustomRenderer
{
    [Obsolete]
    public class CustomEntryWithBordRenderer:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            //added for disable copy paste
            if (Control != null)
            {
                Control.CustomSelectionActionModeCallback = new Callback();
                Control.LongClickable = false;
            }
            //end
            if (e.NewElement != null)
            {


                var view = (CustomEntryWithBord)Element;

                if (view.IsCurvedCornersEnabled)
                {
                    // creating gradient drawable for the curved background
                    var _gradientBackground = new GradientDrawable();
                    _gradientBackground.SetShape(ShapeType.Rectangle);
                    _gradientBackground.SetColor(view.BackgroundColor.ToAndroid());

                    // Thickness of the stroke line
                    _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

                    // Radius for the curves
                    _gradientBackground.SetCornerRadius(
                        DpToPixels(this.Context,
                            Convert.ToSingle(view.CornerRadius)));

                    // set the background of the label
                    Control.SetBackground(_gradientBackground);
                }

                // Set padding for the internal text from border
                Control.SetPadding(
                    (int)DpToPixels(this.Context, Convert.ToSingle(10)),
                    Control.PaddingTop,
                    (int)DpToPixels(this.Context, Convert.ToSingle(10)),
                    Control.PaddingBottom);
            }

        }



        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }

    }
    public class Callback : Java.Lang.Object, ActionMode.ICallback
    {
        public bool OnActionItemClicked(ActionMode mode, IMenuItem item)
        {
            return false;
        }
        public bool OnCreateActionMode(ActionMode mode, IMenu menu)
        {
            return false;
        }
        public void OnDestroyActionMode(ActionMode mode) { }
        public bool OnPrepareActionMode(ActionMode mode, IMenu menu)
        {
            return false;
        }
    }
}