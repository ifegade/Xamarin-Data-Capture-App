using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Infrastructure.Interfaces
{
    public interface ICustomDialogService : IDialogService
    {
        void ShowToastMessage(string theMessage, bool lenght);
        void ShowPrompt(string title, string message);
    }
}
