
using Support.Infrastructure.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Infrastructure.Interfaces
{
    public interface IShowMessagePrompt
    {
        void ShowDialogPrompt(string Title, string Message,            
            bool SetCancelable = false, bool SetInverseBackgroundForced = false,
            MessageResult PositiveButton = MessageResult.OK,
            MessageResult NegativeButton = MessageResult.NONE,
            MessageResult NeutralButton = MessageResult.NONE
            );
        void ShowShortToastMessage(string message);
        void ShowLongToastMessage(string message);
    }
}
