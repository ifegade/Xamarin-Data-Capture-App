using Support.Models;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Infrastructure.Interfaces
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
    }
    public interface IDatabaseService
    {
        void DropTables();
        void CreateTables();
        SQLite.Net.SQLiteConnection _db { set; get; }
        UserProfile SaveUserproile(UserProfile profile);
        UserProfile GetUserProfile();
        List<SupportFormModel> GetInput();
        List<DeviceModel> GetContent();
        void LogOut(UserProfile profile);
        void SaveInput(SupportFormModel jsonContent);
        void SaveInput(DeviceModel content);
        void DeleteInput(SupportFormModel jsonContent);
        void DeleteInput(DeviceModel content);
    }
}
