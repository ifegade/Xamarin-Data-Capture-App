using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Support.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordPage : ContentPage
    {
        public PasswordPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.Login;
        }

        private async void submit_Clicked(object sender, EventArgs e)
        {
            await App.Locator.Login.ChangePassword(oPassword.Text, nPassword.Text, cPassword.Text);
        }
    }
}
