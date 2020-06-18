using Support;
using Support.Models;
using Support.Views;
using System;

using Xamarin.Forms;

namespace Support.Views.Dashboard
{
    public class DashboardController : MasterDetailPage
    {
        public DashboardController()
        {
            Master = new MenuPage();
            Detail = new NavigationPage(new TransactionPage());
           (Master as MenuPage).ListView.ItemSelected += ListView_ItemSelected;
            MasterBehavior = MasterBehavior.Popover;
            IsPresented = false;
            (Xamarin.Forms.Application.Current as App).MasterDetailPage = this;
        }
        public void GoToHome()
        {
            var page = (Page)Activator.CreateInstance(typeof(TransactionPage));
            Detail = new NavigationPage(page);
            (Application.Current as App).navigationService.Initialize(Detail as NavigationPage);
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuItemModel;
            if (item == null)
                return;
            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;
            Detail = new NavigationPage(page);
            (Application.Current as App).navigationService.Initialize(Detail as NavigationPage);           
            IsPresented = false;
            (sender as ListView).SelectedItem = null;
        }
    }
}
