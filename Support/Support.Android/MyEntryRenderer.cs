using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Support;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using Android.Graphics;

[assembly:ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace Support
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //Control.SetBackgroundColor(global::Android.Graphics.Color.LightGreen);
                var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());

                shape.Paint.Color = global::Android.Graphics.Color.ParseColor("#999797");
                shape.Paint.SetStyle(Paint.Style.Stroke);
                Control.SetBackground(shape);
                Control.SetPaddingRelative(30, 15, 30, 15);
                GradientDrawable gd = new GradientDrawable();
                //gd.SetColor(null);
                gd.SetCornerRadius(50);
                gd.SetStroke(2, Android.Graphics.Color.ParseColor("#4A4A4A"));
                Control.SetBackground(gd);
                //Control.TextAlignment = Android.Views.TextAlignment.Center;
                //Control.TextSize = 20f;                
                // Fixed height creates padding at top and bottom
                //Element.HeightRequest = 45;
            }
        }
    }
}