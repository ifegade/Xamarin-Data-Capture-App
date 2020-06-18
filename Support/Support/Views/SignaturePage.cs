using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Support.Views
{
    public class SignaturePage : ContentPage
    {
        SignaturePadView signature;
        public SignaturePage()
        {
            StackLayout con = new StackLayout();
           signature = new SignaturePadView();
            signature.StrokeWidth = 3;
            signature.HeightRequest = 150;
            signature.CaptionText = "Please Sign Here";
            signature.PromptText = "Merchant Signature";            
            signature.HorizontalOptions = LayoutOptions.FillAndExpand;
            signature.BackgroundColor = Color.White;
            signature.StrokeColor = Color.Black;
            con.Children.Add(signature);
            Button a = new Button { Text = "Submit" };
            a.Clicked += A_Clicked;
            con.Children.Add(a);
            Content = con;
            
            
        }

        private async void A_Clicked(object sender, EventArgs e)
        {
            //var theImage = await signature.GetImageStreamAsync(SignatureImageFormat.Png);
            
            //ImageSource a = ImageSource.FromStream()
        }
    }
}
