using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Xamarin.Forms;
using LahmaOnline.CustomRenderer;
using LahmaOnline.Droid.CustomRenderer;

[assembly: ExportRenderer(typeof(VGradientBoxView), typeof(VGradientBoxViewRenderer))]
namespace LahmaOnline.Droid.CustomRenderer
{
    using Android.Graphics;
    using Android.Util;
    using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0618 // Type or member is obsolete
    public class VGradientBoxViewRenderer : VisualElementRenderer<VGradientBoxView>
    {
        private float _cornerRadius;
        private RectF _bounds;
        private Path _path;
        protected override void OnElementChanged(ElementChangedEventArgs<VGradientBoxView> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
            {
                return;
            }
            var element = (VGradientBoxView)Element;
            _cornerRadius = TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)element.Radius, Context.Resources.DisplayMetrics);
        }
        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);
            if (w != oldw && h != oldh)
            {
                _bounds = new RectF(0, 0, w, h);
            }
            _path = new Path();
            _path.Reset();
            _path.AddRoundRect(_bounds, _cornerRadius, _cornerRadius, Path.Direction.Cw);
            _path.Close();
        }
        protected override void DispatchDraw(Canvas canvas)
        {
            var gradient = new LinearGradient(0, 0, 0, Height,
                                              Element.StartColor.ToAndroid(),
                                              Element.EndColor.ToAndroid(),
                                              Shader.TileMode.Mirror);
            var paint = new Paint
            {
                Dither = true,
            };
            if(_path!=null)
                canvas.ClipPath(_path);
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }
    }
#pragma warning restore CS0618 // Type or member is obsolete
}