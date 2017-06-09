using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace UnderTheWeather
{
    public class DataService
    {
        public static async Task<JContainer> GetDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);

            JContainer data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = (JContainer)JsonConvert.DeserializeObject(json);
            }

            return data;
        }
    }
}