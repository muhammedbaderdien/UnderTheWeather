using UnderTheWeather.Helpers;
using Plugin.Geolocator;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UnderTheWeather
{
    public class Core
    {
        //Sign up for a free API key at http://openweathermap.org/appid
        private static string key = "1160f6a79d2306f67662fb11494f1903";
        public static async Task<Weather> GetWeatherByCurLocation(string Latitude, string Longitude, string Address)
        {
            try
            {
                string queryString = "http://api.openweathermap.org/data/2.5/weather?lat=" + Latitude + "&lon=" + Longitude + "&appid=" + key + "&units=metric";

                var results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

                if (results["weather"] != null)
                {
                    Weather weather = new Weather()
                    {
                        Address = Address,

                        TemperatureMin = "min " + results["main"]["temp_min"].ToString() + "°C",
                        Temperature = "max " + results["main"]["temp_max"].ToString() + "°C",

                        Wind = (string)results["wind"]["speed"] + " kph",
                        Humidity = (string)results["main"]["humidity"] + " %",
                        Visibility = (string)results["weather"][0]["main"],
                        ImageUrl = "http://openweathermap.org/img/w/" + (string)results["weather"][0]["icon"] + ".png",
                        Today = "TODAY, " + DateTime.Now.ToString("dd MMMM yyyy")
                    };

                    return weather;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static async Task<Weather> GetWeatherByCityId(string cityID)
        {
            string queryString = "http://api.openweathermap.org/data/2.5/weather?id=" + cityID + "&appid=" + key + "&units=metric";

            var results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                Weather weather = new Weather()
                {
                    Address = (string)results["name"],
                    TemperatureMin = "min " + results["main"]["temp_min"].ToString() + "°C",
                    Temperature = "max " + results["main"]["temp_max"].ToString() + "°C",

                    Wind = (string)results["wind"]["speed"] + " kph",
                    Humidity = (string)results["main"]["humidity"] + " %",
                    Visibility = (string)results["weather"][0]["main"],
                    ImageUrl = "http://openweathermap.org/img/w/" + (string)results["weather"][0]["icon"] + ".png",
                    Today = "TODAY, " + DateTime.Now.ToString("dd MMMM yyyy")
                };

                return weather;
            }
            else
            {
                return null;
            }
        }
    }
}