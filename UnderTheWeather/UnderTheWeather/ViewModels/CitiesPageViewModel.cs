using System.Collections.Generic;

namespace UnderTheWeather
{
	public class CitiesPageViewModel : ViewModelBase
	{
		public IList<City> Cities { get { return CityData.Cities; } }

        City selectedCity;
		public City SelectedCity
		{
			get { return selectedCity; }
			set
			{
				if (selectedCity != value)
				{
					selectedCity = value;
					OnPropertyChanged();
				}
			}
		}
	}
}
