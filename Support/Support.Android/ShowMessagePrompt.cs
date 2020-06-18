
using Android.App;
using Android.Widget;
using Support;
using Support.Infrastructure.Interfaces;
using System.Threading.Tasks;
using Support.Droid;
using Support.Infrastructure.Components;

[assembly: Xamarin.Forms.Dependency(typeof(ShowMessagePrompt))]
namespace Support
{
    public class ShowMessagePrompt : IShowMessagePrompt
    {
        
        Activity mContext;

        public void ShowDialogPrompt(string Title, string Message,
            bool SetCancelable = false, bool SetInverseBackgroundForced = false,
            MessageResult PositiveButton = MessageResult.OK,
            MessageResult NegativeButton = MessageResult.CANCEL,
            MessageResult NeutralButton = MessageResult.YES)
        {
            var tcs = new TaskCompletionSource<MessageResult>();

            var builder = new AlertDialog.Builder(MainActivity.AppActivity);
            builder.SetIconAttribute(Android.Resource.Attribute.AlertDialogIcon);
            builder.SetTitle(Title);
            builder.SetMessage(Message);
            builder.SetInverseBackgroundForced(SetInverseBackgroundForced);
            builder.SetCancelable(SetCancelable);

            builder.SetPositiveButton((PositiveButton != MessageResult.NONE) ? PositiveButton.ToString() : string.Empty, (senderAlert, args) =>
            {
                tcs.SetResult(PositiveButton);
            });
            builder.SetNegativeButton((NegativeButton != MessageResult.NONE) ? NegativeButton.ToString() : string.Empty, delegate
            {
                tcs.SetResult(NegativeButton);
            });
            builder.SetNeutralButton((NeutralButton != MessageResult.NONE) ? NeutralButton.ToString() : string.Empty, delegate
            {
                tcs.SetResult(NeutralButton);
            });

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
            });

            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                builder.Show();
            });
        }

        public void ShowLongToastMessage(string message)
        {
            Toast.MakeText(MainActivity.AppActivity, message, ToastLength.Long).Show();
        }

        public void ShowShortToastMessage(string message)
        {
            Toast.MakeText(MainActivity.AppActivity, message, ToastLength.Short).Show();
        }
    }
}