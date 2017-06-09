using UnderTheWeather.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnderTheWeather.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPage : ContentPage
    {
        public WeatherPage()
        {
            InitializeComponent();

            IMyLocation loc;

            // Handle when your app starts
            loc = DependencyService.Get<IMyLocation>();
            loc.locationObtained += async (object sender, ILocationEventArgs e) =>
            {
                Weather weather = await Core.GetWeatherByCurLocation(e.lat.ToString(), e.lng.ToString(), e.Address);
                if (weather != null)
                {

                    BindingContext = weather;
                }

            };
            //loc.ObtainMyLocation();
        }


    }
}
