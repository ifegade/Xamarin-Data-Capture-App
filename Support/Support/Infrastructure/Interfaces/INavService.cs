using GalaSoft.MvvmLight.Views;
using System;
using Xamarin.Forms;

namespace Support.Infrastructure.Interfaces
{
    public interface INavService : INavigationService
    {
        void Configure(string pageKey, Type pageType);
        void Initialize(NavigationPage page);
        //void Configure(object logOut, Type type);
    }
}
