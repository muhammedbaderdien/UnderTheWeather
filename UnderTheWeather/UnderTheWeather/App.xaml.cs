using UnderTheWeather.Helpers;
using UnderTheWeather.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace UnderTheWeather
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            try
            {

            
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    
                    new NavigationPage(new WeatherPage())
                    {
                        Title = "Under The Weather",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },

                    new NavigationPage(new CitiesPage())
                    {
                        Title = "Weather By City",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                }
            };

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            // Handle when your app resumes

        }
    }
}
