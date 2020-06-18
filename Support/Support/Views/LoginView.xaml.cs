using Microsoft.Practices.ServiceLocation;
using Support.Infrastructure.Interfaces;
using Support.ViewModel;
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
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            //email.Text = "developers@iisysgroup.com";
            //password.Text = "gYmTrB";
            email.Text = ServiceLocator.Current.GetInstance<IDatabaseService>()
                .GetUserProfile()?.Email; //"developers@iisysgroup.com";            
            loginButton.Clicked += LoginButton_Clicked;
            BindingContext = App.Locator.Login;
            this.BindingContext = App.Locator.Login;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            await ServiceLocator.Current.GetInstance<AuthViewModel>()
                .AuthenticateUser(email.Text, password.Text);
        }
    }
}
