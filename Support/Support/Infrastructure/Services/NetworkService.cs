using Support.Infrastructure.Interfaces;
using Support.Models;
using Support.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Support.Infrastructure.Services
{
    public class NetworkService :  INetworkService
    {
        //public const string LinkUrl = "http://192.168.0.122:3000/op/report/notification";//http://197.253.19.75/tams/eftpos/op/support_reporting.php";
        public static HttpClient client;
        public static string LOG_TAG = typeof(NetworkService).FullName;
        public static HttpClientHandler handler;
        private static HttpResponseMessage Response;
            public async Task<JObject> PostAsync(JObject content, string url)
            {
            try
            {
                if (!App.IsConnected)
                    return null;
                if (client == null)
                    SetUpHttpClient();                
                Utils.Utility.ShowDebug(LOG_TAG, content.ToString());
                var response = await new HttpClient().PostAsync(url, new StringContent(content.ToString(),
                                                Encoding.UTF8,
                                                "application/json"));//.WithTimeout(TimeSpan.FromSeconds(10).Milliseconds);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return JObject.Parse(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                Utils.Utility.ShowDebug(LOG_TAG, ex);
            }
            return null;
        }

        
        public void SetUpHttpClient()
        {
            handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                //handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip
                //    | System.Net.DecompressionMethods.Deflate;

            }
            client = new HttpClient(handler);
            //client.BaseAddress = new Uri(AppContants.BaseUrl);
            
            client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
            client.DefaultRequestHeaders.Add("Pragma", "no-cache");
            client.DefaultRequestHeaders.TryAddWithoutValidation("content-encoding", "gzip");
      //      client.DefaultRequestHeaders.Add("content-type", "application/json");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Response = new HttpResponseMessage();
        }
    }
}
