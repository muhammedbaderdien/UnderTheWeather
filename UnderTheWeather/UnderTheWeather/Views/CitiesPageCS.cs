using Xamarin.Forms;

namespace UnderTheWeather
{
	public class CitiesPageCS : ContentPage
	{
		public CitiesPageCS()
		{
			var picker = new Picker { Title = "Select a city" };
			picker.SetBinding(Picker.ItemsSourceProperty, "Weather By City");
			picker.SetBinding(Picker.SelectedItemProperty, "SelectedCity");
			picker.ItemDisplayBinding = new Binding("name");

			var nameLabel = new Label { HorizontalOptions = LayoutOptions.Center };
			nameLabel.SetBinding(Label.TextProperty, "SelectedCity.name");
			nameLabel.SetDynamicResource(VisualElement.StyleProperty, "TitleStyle");

			var countryLabel = new Label { FontAttributes = FontAttributes.Italic, HorizontalOptions = LayoutOptions.Center };
            countryLabel.SetBinding(Label.TextProperty, "SelectedCity.country");
            
            Content = new ScrollView
			{
				Content = new StackLayout
				{
					Margin = new Thickness(20),
					Children =
					{
						new Label { Text = "Cities", FontAttributes = FontAttributes.Bold, HorizontalOptions = LayoutOptions.Center },
						picker,
						nameLabel,
                        countryLabel//,
						//image,
						//detailsLabel
					}
				}
			};

			BindingContext = new CitiesPageViewModel();
		}
	}
}
