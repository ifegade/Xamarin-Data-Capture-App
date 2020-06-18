using System.IO;
using SQLite.Net;
using Support.Infrastructure.Interfaces;
using SQLite.Net.Platform.XamarinAndroid;
using Support;
using Support.Infrastructure.Components;

[assembly: Xamarin.Forms.Dependency(typeof(DB_Droid))]
namespace Support
{
    public class DB_Droid : IDatabaseConnection
    {
        private static SQLiteConnection _connection;
        public SQLiteConnection DbConnection()
        {
            var dbName = AppContants.DatabaseName;
            var path = Path.Combine(System.Environment
                .GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);
            if (_connection == null)
            {
                System.Diagnostics.Debug.WriteLine(path);
                _connection = new SQLiteConnection(new SQLitePlatformAndroid(), path);
            }
            return _connection;
        }
    }
}