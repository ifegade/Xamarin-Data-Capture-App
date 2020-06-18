using Android.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.OS;
using Android.Runtime;

namespace Support.Infrastructure.Interfaces
{
    public interface ILocationInterface : ILocationListener
    {
        //   void InitialService();
        string GetLat();
        string GetLong();
        string GetLocation();
    }
}