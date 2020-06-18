using Support.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support.Infrastructure.Interfaces
{
    public interface INetworkService
    {
        void SetUpHttpClient();        
        Task<JObject> PostAsync(JObject content, string url);        
    }
}
