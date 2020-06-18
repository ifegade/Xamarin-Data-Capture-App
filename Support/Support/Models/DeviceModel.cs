using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Models
{
    public class DeviceModel
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [JsonProperty("refno")]
        public string DeviceId { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
    }
}
