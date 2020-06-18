using Support.Infrastructure.Components;
using Support.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Support.Infrastructure.Services
{
    public class NavigationService : INavService
    {
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private NavigationPage _navigation;

        public string CurrentPageKey
        {
            get
            {
                lock (_pagesByKey)
                {
                    if (_navigation.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = _navigation.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                                      ? _pagesByKey.First(p => p.Value == pageType).Key.ToString() : null;
                }
            }
        }

        public void GoBack()
        {
            _navigation.PopAsync();
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            try
            {
                lock (_pagesByKey)
                {

                    if (_pagesByKey.ContainsKey(pageKey))
                    {
                        var type = _pagesByKey[pageKey];
                        ConstructorInfo constructor;
                        object[] parameters;

                        if (parameter == null)
                        {
                            constructor = type.GetTypeInfo()
                                .DeclaredConstructors
                                .FirstOrDefault(c => !c.GetParameters().Any());

                            parameters = new object[]
                            {
                            };
                        }
                        else
                        {
                            constructor = type.GetTypeInfo()
                                .DeclaredConstructors
                                .FirstOrDefault(
                                    c =>
                                    {
                                        var p = c.GetParameters();
                                        return p.Count() == 1
                                               && p[0].ParameterType == parameter.GetType();
                                    });

                            parameters = new[]
                            {
                            parameter
                        };
                        }

                        if (constructor == null)
                        {
                            throw new InvalidOperationException(
                                "No suitable constructor found for page " + pageKey);
                        }
                        try
                        {
                            var page = constructor.Invoke(parameters) as Page;
                            if (pageKey.Equals(PageStrings.DashboardPage))
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    (Xamarin.Forms.Application.Current as App).MainPage = page;
                                });
                            else if (pageKey.Equals(PageStrings.LogOut))
                            {
                                (Xamarin.Forms.Application.Current as App).UnRegisterNavService();//.MainPage = new NavigationPage(page);
                                                                                                  //  (Xamarin.Forms.Application.Current as App).RegisterNavService();
                            }
                            else
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    _navigation.PushAsync(page);
                                });
                        }
                        catch(Exception ex)
                        {
                            Utils.Utility.ShowDebug(ex);
                        }
                    }
                    else
                    {
                        throw new ArgumentException(
                            string.Format(
                                "No such page: {0}. Did you forget to call NavigationService.Configure?",
                                pageKey), nameof(pageKey));
                    }
                }
            }
            catch(Exception ex)
            {
                Utils.Utility.ShowDebug(ex);
            }
        }      

        public void Initialize(NavigationPage navigation)
        {
            _navigation = navigation;
        }

        public void Configure(string pageKey, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }
    }
}
