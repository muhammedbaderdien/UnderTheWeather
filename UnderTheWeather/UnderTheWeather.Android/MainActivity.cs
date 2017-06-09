using Android.App;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Util;
using UnderTheWeather.Droid;
using UnderTheWeather.Helpers;
using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(MainActivity))]
namespace UnderTheWeather.Droid
{
    public class LocationEventArgs : EventArgs, ILocationEventArgs
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public string Address { get; set; }
    }

    [Activity(Icon = "@drawable/icon", Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IMyLocation, ILocationListener
    {
        Location _currentLocation;
        LocationManager _locationManager;

        string _locationProvider;
        static readonly string TAG = "X:" + typeof(MainActivity).Name;
        protected override void OnCreate(Bundle bundle)
        {
            System.Net.ServicePointManager.DnsRefreshTimeout = 0;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            InitializeLocationManager();

            LoadApplication(new App());
            

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        void InitializeLocationManager()
        {
            _locationManager = (LocationManager)GetSystemService(LocationService);
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
        
        protected override void OnResume()
        {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }

        protected override void OnPause()
        {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        public async void OnLocationChanged(Location location)
        {
            _currentLocation = location;
            var address = await ReverseGeocodeCurrentLocation();
            if (_currentLocation != null)
            {
                LocationEventArgs args = new LocationEventArgs()
                {
                    lat = location.Latitude,
                    lng = location.Longitude,
                    Address = GetAddress(address)
                };
                locationObtained(this, args);
            };
        }
        //---an EventHandler delegate that is called when a location
        // is obtained---
        public static event EventHandler<ILocationEventArgs> locationObtained;
        //---custom event accessor that is invoked when client
        // subscribes to the event---
        event EventHandler<ILocationEventArgs> IMyLocation.locationObtained
        {
            add
            {
                locationObtained += value;
            }
            remove
            {
                locationObtained -= value;
            }
        }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }


        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            /* This is called when the GPS status alters */
            switch (status)
            {
                case Availability.OutOfService:
                    //Vm.Location = "Status Changed: Out of Service";
                    break;
                case Availability.TemporarilyUnavailable:
                    //Vm.Location = "Status Changed: Temporarily Unavailable";
                    break;
                case Availability.Available:
                    //Vm.Location = "Status Changed: Available";
                    break;
            }
        }

        async Task<Address> ReverseGeocodeCurrentLocation()
        {
            try
            {
                Geocoder geocoder = new Geocoder(this);
                IList<Address> addressList =
                    await geocoder.GetFromLocationAsync(_currentLocation.Latitude, _currentLocation.Longitude, 10);

                Address address = addressList.FirstOrDefault();

                return address;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string GetAddress(Address address)
        {
            if (address != null)
            {
                StringBuilder deviceAddress = new StringBuilder();
                for (int i = 0; i < address.MaxAddressLineIndex; i++)
                {
                    deviceAddress.AppendLine(address.GetAddressLine(i));
                }
                // Remove the last comma from the end of the address.
                return deviceAddress.ToString();
            }
            else
            {
                return "Unable to determine the address. Try again in a few minutes.";
            }
        }

        //---method to call to start getting location---
        public void ObtainMyLocation()
        {
            //_locationManager = (LocationManager)GetSystemService(LocationService);
            _locationManager.RequestLocationUpdates(LocationManager.NetworkProvider,
                    0,   //---time in ms---
                    0,   //---distance in metres---
                    this);
        }
        //---stop the location update when the object is set to
        // null---
        ~MainActivity()
        {
            _locationManager.RemoveUpdates(this);
        }
    }
}