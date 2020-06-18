using Android.App;
using Android.Content;
using Android.Locations;
using Android.Util;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Support.Infrastructure.Components;
using Support.Infrastructure.Interfaces;
using Support.Infrastructure.Services;
using Support.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Support.ViewModel
{
    public class TransactionViewModel : AppActors
    {
        public SupportFormModel JsonContent;
        string _locationProvider;
        public TransactionViewModel(INavService navService, INetworkService netService,
            IDatabaseService db, ICustomDialogService dialog) : base(navService, netService, db, dialog)
        {
            IsBusy = false;
            JsonContent = new SupportFormModel();
            var location = DependencyService.Get<ILocationInterface>();
            //       DependencyService.Get<ILocationInterface>().InitialService();
        }
        public async Task SubmitCoordinate(string DeviceId)
        {

            try
            {
                if (string.IsNullOrEmpty(DeviceId))
                    _dialog.ShowToastMessage("Enter REF ID", false);
                else
                {
                    IsBusy = true;
                    var location = DependencyService.Get<ILocationInterface>().GetLocation(); //ServiceLocator.Current.GetInstance<ILocationInterface>().GetLocation();                    
                    DeviceModel content = new DeviceModel()
                    {
                        Long = DependencyService.Get<ILocationInterface>().GetLong(),
                        Lat = DependencyService.Get<ILocationInterface>().GetLat(),
                        DeviceId = DeviceId
                    };
                    await SaveContent(content);
                    //System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(content).ToString());
                }
            }
            catch (Exception ex)
            {
                Utils.Utility.ShowDebug(ex);
            }
            IsBusy = false;
        }
        async Task SaveContent(DeviceModel content)
        {
            try
            {
                var json = JObject.Parse(JsonConvert.SerializeObject(content));

                json["apiKey"] = "API KEY";
                if (App.IsConnected)
                {
                    var results = await _networkService.PostAsync
                        (json, $"URL");

                    if (results != null)
                    {
                        int result = (int)results["result"];
                        if (result == 1)
                        {
                            if (content.ID != 0)
                                _db.DeleteInput(content);
                            IsBusy = false;
                            _dialog.ShowToastMessage("Data Submitted", true);
                            return;
                        }
                        else
                        {
                            _dialog.ShowToastMessage(results["message"].ToString(), true);
                        }
                    }
                    else
                        _dialog.ShowToastMessage("Something went wrong", true);
                    _db.SaveInput(content);
                }
                else
                {
                    _db.SaveInput(content);
                    _dialog.ShowToastMessage("Data Saved for offline Sync", true);
                }
            }
            catch (Exception ex)
            {
                Utils.Utility.ShowDebug(ex);
            }
        }
        JObject ServerJsonContent;
        public async Task ProcessSupportForm(string tid,
            string acquiringBank, string MerchantName,
            string MerchantNumber, string terminalType,
            string version, string terminalSerial,
            string purpose, string battery, string merchantAddress, string reason)
        {
            //  int b = 0;
            if (string.IsNullOrEmpty(tid)// || string.IsNullOrEmpty(acquiringBank)
                || string.IsNullOrEmpty(MerchantName) || string.IsNullOrEmpty(MerchantNumber)
                || string.IsNullOrEmpty(terminalType) || string.IsNullOrEmpty(version)
                || string.IsNullOrEmpty(terminalSerial) || string.IsNullOrEmpty(purpose)
                || string.IsNullOrEmpty(merchantAddress))
                _dialog.ShowToastMessage("Input all data", true);
            else if (tid.Length != 8)
                _dialog.ShowToastMessage("Invalid Terminal ID", true);
            //else if (!int.TryParse(battery, out b))
            //{
            //    _dialog.ShowToastMessage("Invalid Battery Level", true);
            //}        
            // else if(b > 100)
            //   _dialog.ShowToastMessage("Invalid Battery Level", true);
            else
            {
                JsonContent = new SupportFormModel()
                {
                    SupportStaff = ServiceLocator.Current.GetInstance<IDatabaseService>().GetUserProfile().Email,
                    TerminalID = tid,
                    Purpose = reason,
                    PosStatus = purpose,
                    MerchantName = MerchantName,
                    Version = version,
                    TerminalType = terminalType,
                    TerminalSerial = terminalSerial,
                    MerchantMobile = MerchantNumber,
                    AcquiringBank = acquiringBank,
                    Battery = $"{battery}%",
                    merchantAddress = merchantAddress,

                };
                await ContinueTerminalStatus();
                //_navigationService.NavigateTo(PageStrings.TerminalPage);
            }
        }

        internal async void SendContent(List<DeviceModel> deviceContent)
        {
            foreach (var item in deviceContent)
            {
                await SaveContent(item);
                _dialog.ShowToastMessage("Offline Data Sent", true);
            }
        }

        public async void SendContent(List<SupportFormModel> contentHolder)
        {
            foreach (var item in contentHolder)
            {
                await SendContent(item);
                _dialog.ShowToastMessage("Offline Data Sent", true);
            }
        }

        private async Task SendContent(SupportFormModel item)
        {
            try
            {
                if (item != null)
                {
                    var _json = JObject.Parse(JsonConvert.SerializeObject(item).ToString());
                    if (App.IsConnected)
                    {
                        var content = await _networkService.PostAsync(_json, "URL");
                        if (content != null)
                        {
                            int result = (int)content["result"];
                            if (result == 1)
                            {
                                if (item.ID != 0)
                                    _db.DeleteInput(item);
                                _dialog.ShowToastMessage("Data Submitted", true);
                                _navigationService.NavigateTo(PageStrings.DashboardPage);
                            }
                            else
                            {
                                _db.SaveInput(item);
                                _dialog.ShowToastMessage(content["message"].ToString(), true);
                            }
                        }
                    }
                    else
                    {
                        _db.SaveInput(item);
                        _dialog.ShowToastMessage("You do not have internet connection. Your data will be summited later", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Utility.ShowDebug(ex);
            }
        }

        public async Task ContinueTerminalStatus()
        {
            try
            {
                IsBusy = true;
                JsonContent.Long = DependencyService.Get<ILocationInterface>().GetLong();
                JsonContent.Lat = DependencyService.Get<ILocationInterface>().GetLat();
                //JsonContent.Purpose = reason;
                System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(JsonContent).ToString());
                await SendContent(JsonContent);
                (Xamarin.Forms.Application.Current as App).MasterDetailPage.GoToHome();
            }
            catch (Exception ex)
            {
                Utils.Utility.ShowDebug(ex);
            }
            IsBusy = false;
        }
    }
}
