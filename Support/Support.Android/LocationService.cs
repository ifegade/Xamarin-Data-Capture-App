using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;
using Support.Infrastructure.Interfaces;
using Android.Util;
using Xamarin.Forms;
using Support.Droid;

[assembly:Xamarin.Forms.Dependency(typeof(LocationService))]
namespace Support.Droid
{
    public class LocationService : Service, ILocationInterface
    {
        public Location _currentLocation;
        public LocationManager _locationManager;
        string _locationProvider;
        string TAG = typeof(LocationService).FullName;     

        public LocationService() : base()
        {
            LocationMethod();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }
        public string GetLat()
        {
            return _currentLocation?.Latitude.ToString();
        }
        public string GetLong()
        {
            return _currentLocation?.Longitude.ToString();
        }
        public string GetLocation()
        {
            if (_currentLocation == null)
                return string.Empty;
            return $"{_currentLocation.Latitude}-{_currentLocation.Longitude}";
        }
        public void LocationMethod()
        {
            _locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                _locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                _locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + _locationProvider + ".");
        }

        public void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            if (_currentLocation == null)
            {
                
            }
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
            
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            
        }

        public void Dispose()
        {
            _locationManager.RemoveUpdates(this);
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}