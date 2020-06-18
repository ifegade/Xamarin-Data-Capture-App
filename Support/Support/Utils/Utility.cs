using Newtonsoft.Json.Linq;
using Support.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Support.Utils
{
    public class Utility
    {
        public static string ToBase64(string theString)
        {
            if (!string.IsNullOrEmpty(theString))
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(theString));
            }
            return string.Empty;
        }
        public static bool ContinueProcessing(NetworkResponseModel model)
        {
            if (model != null && model.NetCode.Equals(NetworkCode.Successful))
                return true;
            return false;
        }
        public static void ShowDebug(string Logtag, string exception)
        {
            ShowDebug(Logtag);
            ShowDebug(exception);
        }
        public static void ShowDebug(string Logtag, Exception exception)
        {
            ShowDebug(Logtag);
            ShowDebug(exception);
        }
        public static void ShowDebug(Exception ex)
        {
            if (ex != null)
            {
                ShowDebug(ex.StackTrace);
                ShowDebug("=================");
                ShowDebug(ex.InnerException);
                ShowDebug("=================");
                ShowDebug(ex.Message);
            }
        }
        public static void ShowDebug(string message)
        {
            Debug.WriteLine(message);
        }
        public static bool isValidEmail(string theEmail)
        {
            if (string.IsNullOrWhiteSpace(theEmail))
                return false;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            if (!(new Regex(strRegex)).IsMatch(theEmail))
                return false;
            return true;
        }

        internal static List<string> GetErrors(JObject jsonResponse)
        {
            var content = new List<String>();
            foreach (var item in jsonResponse)
                content.Add(item.ToString());
            return content;
        }

    }
}
