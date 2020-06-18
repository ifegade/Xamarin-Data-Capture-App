using Newtonsoft.Json;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Models
{
    public class SupportFormModel
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public SupportFormModel()
        {
            LastSeen = DateTime.Now.ToString("ddd, MMM d yyyy - HH:mm:ss");
            Auth = Sticker = Charger = RefNo = BankLogo = SeqNo = "OK";
         }
        public string merchantAddress { get; set; }
        public string SupportStaff { get; set; }
        [JsonProperty("tid")]
        public string TerminalID { get; set; }
        [JsonProperty("LogoName")]
        public string AcquiringBank { get; set; }        
        public string MerchantName { get; set; }
        public string MerchantMobile { get; set; }       
        public string PosStatus { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
        public string TerminalType { get; set; } 
        [JsonProperty("AppVersion")]       
        public string Version { get; set; }
        public string TerminalSerial { get; set; }        
                
        [JsonProperty("BatteryPercent")]
        public string Battery { get; set; }
        [JsonProperty("Sticker")]
        public string Sticker { get; set; }        
        [JsonProperty("ChargerWorking")]
        public string Charger { get; set; }
        
        [JsonProperty("RefNo")]
        public string RefNo { get; set; }
        [JsonProperty("BankLogo")]
        public string BankLogo { get; set; }
        [JsonProperty("AuthCode")]
        public string Auth { get; set; }
        [JsonProperty("SeqNo")]
        public string SeqNo { get; set; }
        public string Purpose { get; set; }
        public string LastSeen { get; set; }
        public string Printer { get; set; }
    }
}
