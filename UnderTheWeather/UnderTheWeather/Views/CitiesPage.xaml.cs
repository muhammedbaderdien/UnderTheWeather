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
    public partial class CitiesPage : ContentPage
    {
        private CitiesPageViewModel _model;
        public CitiesPage()
        {
            InitializeComponent();
            BindingContext = _model = new CitiesPageViewModel();

        }


        async void OnNavigateButtonClicked(object sender, EventArgs e)
        {
            City curCity = _model.SelectedCity;

            if (curCity == null)
            {
                await DisplayAlert("City", "Please select a city", "OK");
            }
            else
            {
                Weather weather = await Core.GetWeatherByCityId(curCity.id);
                if (weather != null)
                {

                    var secondPage = new WeatherPage()
                    {
                        BindingContext = weather
                    };
                    await Navigation.PushAsync(secondPage);
                }
                else
                {
                    await DisplayAlert("Weather", "No weather found for selected city. Please select another city", "OK");
                }


            }

        }
    }
}
