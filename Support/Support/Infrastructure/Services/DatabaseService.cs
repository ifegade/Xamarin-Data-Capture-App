using Support.Infrastructure.Interfaces;
using Support.Models;
using SQLite.Net;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Support.Infrastructure.Services
{
    public class DatabaseService : IDatabaseService
    {
        public SQLiteConnection _db { get; set; }

        public DatabaseService()
        {
            _db = DependencyService.Get<IDatabaseConnection>().DbConnection();
            CreateTables();
        }
        public UserProfile GetUserProfile()
        {
            return _db.Table<UserProfile>().FirstOrDefault();
        }

        public void LogOut(UserProfile profile)
        {
            //var check = _db.Table<UserProfile>().FirstOrDefault();
            if(profile != null)
            {
                profile.IsLoggedIn = false;
                _db.Update(profile);
              //  MessagingCenter.Send<ProfileMessage>(new ProfileMessage(), "ProfileMessage");
            }
        }


        public UserProfile SaveUserproile(UserProfile profile)
        {
            var check = _db.Table<UserProfile>().FirstOrDefault();
            if(check != null)
            {
                if (check.Email.Equals(profile.Email))
                {             
                    _db.Update(check);
                }
                else if(check.Email != profile.Email)
                {
                    DropTables();
                    CreateTables();                    
                    _db.Insert(profile);
                }
            }
            else if (check == null)
            {
                _db.Insert(profile);
                
            }
            return GetUserProfile();
        }

        public void DropTables()
        {
            _db.DropTable<DeviceModel>();
            _db.DropTable<UserProfile>();
            _db.DropTable<SupportFormModel>();
        }

        public void CreateTables()
        {
            _db.CreateTable<UserProfile>();
            _db.CreateTable<SupportFormModel>();
            _db.CreateTable<DeviceModel>();
        }
        
        public void SaveInput(SupportFormModel jsonContent)
        {
            if (jsonContent.ID == 0)
                _db.Insert(jsonContent);
        }

        public List<SupportFormModel> GetInput()
        {
            return _db.Table<SupportFormModel>().ToList();
        }
        public List<DeviceModel> GetContent()
        {
            return _db.Table<DeviceModel>().ToList();
        }
        public void SaveInput(DeviceModel content)
        {
            if (content.ID == 0)
                _db.Insert(content);
        }

        public void DeleteInput(SupportFormModel jsonContent)
        {
            _db.Delete<SupportFormModel>(jsonContent.ID);
        }

        public void DeleteInput(DeviceModel content)
        {
            _db.Delete<DeviceModel>(content.ID);
        }
    }
}
