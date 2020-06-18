using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Models
{
    public enum NetworkCode
    {
        Default,
        Successful,
        UnSuccessful,
        Oops
    }
    public class NetworkResponseModel
    {
        public string ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseJsonContent { get; set; }
        public NetworkCode NetCode { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}
