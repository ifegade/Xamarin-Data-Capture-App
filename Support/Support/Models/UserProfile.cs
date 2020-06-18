using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using SQLite.Net.Attributes;
using Newtonsoft.Json;

namespace Support.Models
{
    public class UserProfile //: INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Email { get; set; }
        public bool IsLoggedIn { get; internal set; }
    }
}
