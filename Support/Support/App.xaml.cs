using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Support.Infrastructure.Interfaces;
using Support.Infrastructure.Services;
using Support.ViewModel;
using System;
using Support.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Microsoft.Practices.ServiceLocation;
using Support.Models;
using Support.Infrastructure.Components;
using Support.Views;
using Support.Views.Dashboard;
using Android.Locations;
using Android.Util;

namespace Support
{
    public partial class App : Application
    {
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator
        {
            get; private set;
            //get
            //{
            //    return _locator ?? (_locator = new ViewModelLocator());
            //}
        }

        public static bool IsConnected { get; internal set; }

        public DashboardController MasterDetailPage { get; set; }
        public INavService navigationService;
        public App()
        {
            InitializeComponent();
            try
            {
                //       MainPage = new SignaturePage();
                CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;
                IsConnected = CrossConnectivity.Current.IsConnected;
                Locator = new ViewModelLocator();
                if (!SimpleIoc.Default.IsRegistered<INavigationService>())
                {
                    RegisterNavService();
                }
                else
                    navigationService = SimpleIoc.Default.GetInstance<INavService>();

            }
            catch (Exception ex)
            {
                Log.Info("Ifeoluwa", ex.StackTrace);
                Log.Info("Ifeoluwa", ex.Message);
                Utils.Utility.ShowDebug(ex);
            }
        }
        public void RegisterNavService()
        {
            navigationService = new NavigationService();            
            navigationService.Configure(PageStrings.LoginPage, typeof(LoginView));
            navigationService.Configure(PageStrings.LogOut, typeof(LoginView));
            navigationService.Configure(PageStrings.TransactionDetailsPage, typeof(TransactionWithDetailsPage));
            navigationService.Configure(PageStrings.TransactionPage, typeof(LoginView));
            navigationService.Configure(PageStrings.DashboardPage, typeof(DashboardController));
            navigationService.Configure(PageStrings.TerminalPage, typeof(TerminalPage));
            navigationService.Configure(PageStrings.PasswordPage, typeof(PasswordPage));

            SimpleIoc.Default.Register<INavService>(() => navigationService);
            SimpleIoc.Default.Register<ICustomDialogService>(() => new DialogService(), true);
            SimpleIoc.Default.Register<IDatabaseService>(() => new DatabaseService(), true);
            SimpleIoc.Default.Register<INetworkService>(() => new NetworkService());
          //  SimpleIoc.Default.Register<ILocationListener>(() => new LocationService);
            Locator = new ViewModelLocator();
        }
        public void UnRegisterNavService()
        {


            SimpleIoc.Default.Unregister<INavService>();
            SimpleIoc.Default.Unregister<ICustomDialogService>();
            SimpleIoc.Default.Unregister<IDatabaseService>();
            SimpleIoc.Default.Unregister<INetworkService>();
            ViewModelLocator.UnRegisterModels();
            RegisterNavService();
            var firstPage = new NavigationPage(new LoginView());
            // Set Navigation page as default page for Navigation Service:
            navigationService.Initialize(firstPage);
            // You have to also set MainPage property for the app:
            MainPage = firstPage;
            //    MainPage = new DashboardController();
            MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);
        }
        protected async override void OnStart()
        {
            base.OnStart();
            try
            {

                NavigationPage firstPage;
                UserProfile profile = ServiceLocator.Current.GetInstance<IDatabaseService>().GetUserProfile();
                if (profile != null && profile.IsLoggedIn == true)
                {

                    var thePage = new DashboardController();

                    firstPage = thePage.Detail as NavigationPage;
                    firstPage.SetValue(NavigationPage.HasNavigationBarProperty, false);
                    navigationService.Initialize(firstPage);
                    //firstPage.BarBackgroundColor = Color.FromHex("#000000");
                    MainPage = thePage;
                    var contentHolder = ServiceLocator.Current.GetInstance<IDatabaseService>().GetInput();
                    if (contentHolder != null && contentHolder.Count > 0)
                    {
                        ServiceLocator.Current.GetInstance<TransactionViewModel>().SendContent(contentHolder);
                    }
                    var deviceContent = ServiceLocator.Current.GetInstance<IDatabaseService>().GetContent();
                    if (deviceContent != null && deviceContent.Count > 0)
                    {
                        ServiceLocator.Current.GetInstance<TransactionViewModel>().SendContent(deviceContent);
                    }
                }

                else
                {
                    firstPage = new NavigationPage(new LoginView());
                    // Set Navigation page as default page for Navigation Service:
                    navigationService.Initialize(firstPage);
                    // You have to also set MainPage property for the app:
                    MainPage = firstPage;
                }
                //MainPage = new DashboardController();
                MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);

            }
            catch (Exception ex)
            {
                Utils.Utility.ShowDebug(ex);
            }
        }
        public void OpenfirstPage(NavigationPage page)
        {

        }
        private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected;
        }

        public void SwitchPage(Page newPage)
        {

        }
    }
}
