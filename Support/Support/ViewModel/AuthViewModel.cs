using Support.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Support.Infrastructure.Interfaces;
using Support.Infrastructure.Components;
using Newtonsoft.Json.Linq;

namespace Support.ViewModel
{
    public class AuthViewModel : AppActors
    {
        public AuthViewModel(INavService navService, INetworkService netService,
            IDatabaseService db, ICustomDialogService dialog) : base(navService, netService, db, dialog)
        {
            
        }
        public async Task ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                _dialog.ShowToastMessage("Password Does Not Match", false);
                return;
            }
            try
            {
                IsBusy = true;
                JObject contain = new JObject();
                Profile = _db.GetUserProfile();
                contain["oldPass"] = oldPassword;
                contain["email"] = Profile.Email;
                contain["password"] = newPassword;
                contain["apiKey"] = "NDgzOSFJdGV4QERldl9TdXBwb3J0";
                var content = await _networkService.PostAsync(contain, "http://197.253.19.77/op/report/auth0/setting/update");
                if (content != null)
                {
                    int result = (int)content["result"];
                    if (result == 1)
                    {
                        _navigationService.NavigateTo(PageStrings.DashboardPage);
                        _dialog.ShowToastMessage("Password Changed", false);
                    }
                    else
                    {
                        _dialog.ShowToastMessage(content["message"].ToString(), false);
                    }
                }
                IsBusy = false;
            }
            catch(Exception ex)
            {
                Utils.Utility.ShowDebug(ex);
            }
        }
            public async Task AuthenticateUser(string email, string password)
        {
            try
            {
                //_navigationService.NavigateTo(PageStrings.DashboardPage);
                if (string.IsNullOrEmpty(email))
                    _dialog.ShowToastMessage("Enter email address", true);
                else if (string.IsNullOrEmpty(password))
                    _dialog.ShowToastMessage("Enter Password", true);
                else
                {
                    IsBusy = true;
                    JObject _json = new JObject();
                    _json["usrPass"] = Utils.Utility.ToBase64($"4839.{password}");
                    _json["userId"] = Utils.Utility.ToBase64(email);  
                    _json["apiKey"] = "NDgzOSFJdGV4QERldl9TdXBwb3J0";
                    var content = await _networkService.PostAsync(_json, "http://197.253.19.77:80/op/report/auth0/1");
                    if (content != null)
                    {
                        
                        int result = (int)content["result"];
                        int defaultKey = 0;

                        
                        if (content["default"] != null)
                            defaultKey = (int)content["default"];

                        if (result == 1)
                        {
                            _db.SaveUserproile(new Models.UserProfile() { Email = email, IsLoggedIn = true });
                            if (defaultKey == 3)
                            {
                                _navigationService.NavigateTo(PageStrings.DashboardPage);
                            }
                            else if(defaultKey == 2)
                            {
                                _navigationService.NavigateTo(PageStrings.PasswordPage);
                            }
                        }
                        else
                        {
                            
                            _dialog.ShowToastMessage(content["message"].ToString(), true);
                        }

                    }
                    else
                        _dialog.ShowToastMessage("Something went wrong!!! Try Later", true);
                }
            }
            catch(Exception ex)
            {
                Utils.Utility.ShowDebug(ex);
            }
            IsBusy = false;
        }
    }
}
