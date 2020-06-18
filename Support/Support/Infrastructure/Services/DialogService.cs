using Support.Infrastructure.Interfaces;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Support
{
    public class DialogService : ICustomDialogService
    {
        public DialogService()
        {

        }

        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessage(string message, string title)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText, Action<bool> afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public void ShowToastMessage(string theMessage, bool longToast)
        {
            if(longToast)
                DependencyService.Get<IShowMessagePrompt>().ShowLongToastMessage(theMessage);
            else
                DependencyService.Get<IShowMessagePrompt>().ShowShortToastMessage(theMessage);
        }

        public Task ShowMessageBox(string message, string title)
        {
            throw new NotImplementedException();
        }

        public void ShowPrompt(string title, string message)
        {
            DependencyService.Get<IShowMessagePrompt>().ShowDialogPrompt("Ifeoluwa", "Ifegade");
        }
    }
}
