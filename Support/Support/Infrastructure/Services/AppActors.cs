using Support.Infrastructure.Interfaces;
using Support.Models;
using Support.Infrastructure.Components;
using Support.ViewModel;
using Microsoft.Practices.ServiceLocation;
using MvvmHelpers;
using System.ComponentModel;
using Xamarin.Forms;


namespace Support.Infrastructure.Services
{
    public class AppActors : BaseViewModel
    {
        private UserProfile _profile;
        public UserProfile Profile
        {
            get { return _profile; }
            set
            {
                SetProperty(ref _profile, value);
                
            }
        }
        protected readonly INavService _navigationService;
        protected readonly INetworkService _networkService;
        protected readonly IDatabaseService _db;
        protected readonly ICustomDialogService _dialog;
        private ICustomDialogService dialog;
        public AppActors(IDatabaseService db) : base()
        {
            this._db = db;
        }
        public AppActors(INavService navService, INetworkService netService, 
            IDatabaseService db, ICustomDialogService dialog) : base()
        {
            this._navigationService = navService;
            this._networkService = netService;
            this._db = db;
            this._dialog = dialog;
            IsBusy = false;
            SetupProfile();            
        }

        public void SetupProfile()
        {
            _profile = new UserProfile();            
            //_profile = _db.GetUserProfile();
            RegisterHandler();
        }

        public AppActors(ICustomDialogService dialog)
        {
            this.dialog = dialog;
            SetupProfile();
        }

        private void Profile_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(nameof(Profile.IsLoggedIn)))
            {
                if (!Profile.IsLoggedIn)
                {
                    //ServiceLocator.Current.GetInstance<AuthViewModel>().LoginButtonClicked();
                }
            }    
        }

        public void ProcessFailedResponse(NetworkResponseModel content)
        {
            if (content != null)
                if (content.NetCode.Equals(NetworkCode.Oops))
                    _dialog.ShowToastMessage(PromptMessage.CheckInternet, true);
        }
        private void RegisterHandler()
        {
            //MessagingCenter.Subscribe<ProfileMessage>(this, "ProfileMessage", message =>
            //{
            //    Profile = _db.GetUserProfile();
            //});
        }
    }
}
