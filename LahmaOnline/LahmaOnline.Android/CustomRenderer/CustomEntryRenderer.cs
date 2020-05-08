using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using LahmaOnline.CustomRenderer;
using LahmaOnline.Droid.CustomRenderer;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace LahmaOnline.Droid.CustomRenderer
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                Control.SetBackgroundDrawable(gd);
            }
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}

