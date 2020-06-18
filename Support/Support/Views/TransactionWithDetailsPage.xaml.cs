using Microsoft.Practices.ServiceLocation;
using Support.Infrastructure.Components;
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
    public partial class TransactionWithDetailsPage : ContentPage
    {
        public TransactionWithDetailsPage()
        {
            try
            {
                InitializeComponent();
                BindingContext = App.Locator.Transaction;
                terminalType.ItemsSource = new List<string>
                {
                    "Vx520",
                    "Vx675",
                    "Vx520C.",
                    "Pax S90.",
                    "NewPos8110", 
                    "Others",
                };
                purpose.ItemsSource = new List<string>
                {
                    "Routine Check",
                    "Upgrade",
                    "Retrival"
                };
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


        private async void terminalButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                tid.BackgroundColor = string.IsNullOrEmpty(tid.Text) ? Color.Red : Color.Default;
           //     acquiringBank.BackgroundColor = string.IsNullOrEmpty(acquiringBank.Text) ? Color.Red : Color.Default;

                MerchantName.BackgroundColor = string.IsNullOrEmpty(MerchantName.Text) ? Color.Red : Color.Default;

                MerchantNumber.BackgroundColor = string.IsNullOrEmpty(MerchantNumber.Text) ? Color.Red : Color.Default;

                if (userInput.Equals("Others"))
                    OtherTerminal.BackgroundColor = string.IsNullOrEmpty(OtherTerminal.Text) ? Color.Red : Color.Default;
                else
                {
                    terminalType.BackgroundColor = string.IsNullOrEmpty(terminalType.SelectedItem?.ToString()) ? Color.Red : Color.Default;
                    terminalType.BackgroundColor = terminalType.SelectedItem == null ? Color.Red : Color.Default;
                }
                version.BackgroundColor = string.IsNullOrEmpty(version.Text) ? Color.Red : Color.Default;
                reason.BackgroundColor = string.IsNullOrEmpty(version.Text) ? Color.Red : Color.Default;
                merchantAddress.BackgroundColor = string.IsNullOrEmpty(merchantAddress.Text) ? Color.Red : Color.Default;
                terminalSerial.BackgroundColor = string.IsNullOrEmpty(terminalSerial.Text) ? Color.Red : Color.Default;
                purpose.BackgroundColor = purpose.SelectedItem == null ? Color.Red : Color.Default;
                purpose.BackgroundColor = string.IsNullOrEmpty(purpose.SelectedItem?.ToString()) ? Color.Red : Color.Default;
                await ServiceLocator.Current
                    .GetInstance<TransactionViewModel>()
                    .ProcessSupportForm(tid.Text,
                        "",//acquiringBank.Text,
                        MerchantName.Text,
                        MerchantNumber.Text,
                        userInput.Equals("Others") ? OtherTerminal.Text : terminalType.SelectedItem?.ToString(),
                        version.Text,
                        terminalSerial.Text,
                        purpose.SelectedItem?.ToString(),
                        string.Empty, merchantAddress.Text, reason.Text);
            }
            catch(Exception ex)
            {
                Utils.Utility.ShowDebug(ex);
            }
        }
        public string userInput { get; set; } = string.Empty;



        private void terminalType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as Picker).SelectedItem != null)
            {
                userInput = (sender as MyPicker).SelectedItem.ToString();
                if (userInput.Equals("Others"))
                    OtherTerminal.IsVisible = true;
                else
                    OtherTerminal.IsVisible = false;
            }
        }
    }
}