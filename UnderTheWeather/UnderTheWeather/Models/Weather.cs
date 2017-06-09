namespace UnderTheWeather
{
    public class Weather
    {
        public string Address { get; set; }

        public string Today { get; set; }

        public string Temperature { get; set; }
        public string TemperatureMin { get; set; }
        public string Wind { get; set; }
        public string Humidity { get; set; }
        public string Visibility { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string ImageUrl { get; set; }

        public Weather()
        {
            //Because labels bind to these values, set them to an empty string to
            //ensure that the label appears on all platforms by default.
            this.Address = " ";
            this.Temperature = " ";
            this.Wind = " ";
            this.Humidity = " ";
            this.Visibility = " ";
            this.Sunrise = " ";
            this.Sunset = " ";
            this.ImageUrl = " ";
        }
    }
}