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
using Android.Text;

[assembly: ExportRenderer(typeof(MyLabel), typeof(MyLabelRenderer))]
namespace Support
{
    public class MyLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            Control.SetMaxLines(2);
            Control.Ellipsize = TextUtils.TruncateAt.End;
        }
    }
}