using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using Android.Graphics;
using Support;

[assembly: ExportRenderer(typeof(MyPicker), typeof(MyPickerRenderer))]
namespace Support
{
    public class MyPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
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
                Control.TextSize = 15f;
                // Fixed height creates padding at top and bottom
               // Element.HeightRequest = 45;
            }
        }
    }
}
