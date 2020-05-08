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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using LahmaOnline.Droid.CustomRenderer;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]

namespace LahmaOnline.Droid.CustomRenderer
{
#pragma warning disable CS0618 // Type or member is obsolete
    class CustomEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
            }
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete

}