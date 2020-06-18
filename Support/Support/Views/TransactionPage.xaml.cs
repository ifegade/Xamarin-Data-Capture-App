using Microsoft.Practices.ServiceLocation;
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
    public partial class TransactionPage : ContentPage
    {
        public TransactionPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.Transaction;
        }

        private async void submitButton_Clicked(object sender, EventArgs e)
        {
            await ServiceLocator.Current.GetInstance<TransactionViewModel>()
                .SubmitCoordinate(deviceId.Text);
        }
    }
}
