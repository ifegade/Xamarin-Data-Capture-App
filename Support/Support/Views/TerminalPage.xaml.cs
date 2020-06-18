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
    public partial class TerminalPage : ContentPage
    {

        public TerminalPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = App.Locator.Transaction;
                //reason.ItemsSource = new List<string>()
                //{
                //    "Hardware Damage",
                //    "Software Upgrade",
                //    "Swap",
                //    "Malfunction",
                //    "Others"
                //};
            }
            catch (Exception ex)
            {
                Utils.Utility.ShowDebug(ex);
            }
        }

        private void battery_Toggled(object sender, ToggledEventArgs e)
        {
            ServiceLocator.Current.GetInstance<TransactionViewModel>()
                .JsonContent.Battery = e.Value ? "BAD" : "Ok";
        }
        private void sticker_Toggled(object sender, ToggledEventArgs e)
        {
            ServiceLocator.Current.GetInstance<TransactionViewModel>()
                .JsonContent.Sticker = e.Value ? "BAD" : "Ok";
        }      
        private void charger_Toggled(object sender, ToggledEventArgs e)
        {
            ServiceLocator.Current.GetInstance<TransactionViewModel>()
                .JsonContent.Charger = e.Value ? "BAD" : "Ok";
        }       
        private void ref_Toggled(object sender, ToggledEventArgs e)
        {
            ServiceLocator.Current.GetInstance<TransactionViewModel>()
                .JsonContent.RefNo = e.Value ? "BAD" : "Ok";
        }
        private void bank_Toggled(object sender, ToggledEventArgs e)
        {
            ServiceLocator.Current.GetInstance<TransactionViewModel>()
                .JsonContent.BankLogo = e.Value ? "BAD" : "Ok";
        }
        private void auth_Toggled(object sender, ToggledEventArgs e)
        {
            ServiceLocator.Current.GetInstance<TransactionViewModel>()
                .JsonContent.Auth = e.Value ? "BAD" : "Ok";
        }
        private void seq_Toggled(object sender, ToggledEventArgs e)
        {
            
            ServiceLocator.Current.GetInstance<TransactionViewModel>()
                .JsonContent.SeqNo = e.Value ? "BAD" : "Ok";
        }

        //private void reason_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    ServiceLocator.Current.GetInstance<TransactionViewModel>()
        //        .JsonContent.Purpose = e.SelectedItem.ToString();
        //}

        private async void submitResponse_Clicked(object sender, EventArgs e)
        {
          //  await ServiceLocator.Current.GetInstance<TransactionViewModel>().ContinueTerminalStatus(reason.Text);
        }
    }
}
