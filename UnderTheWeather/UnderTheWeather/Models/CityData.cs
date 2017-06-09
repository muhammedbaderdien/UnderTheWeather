using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.IO;
using UnderTheWeather.Helpers;
using System.Reflection;
using System.Linq;

namespace UnderTheWeather
{
    public static class CityData
    {
        public static IList<City> Cities { get; private set; }

        static CityData()
        {
            try
            {
                var assembly = typeof(ResourceLoader).GetTypeInfo().Assembly;
                Stream stream = assembly.GetManifestResourceStream("UnderTheWeather.city.list.za.json");
                string jsonZACities = "";
                using (var reader = new StreamReader(stream))
                {
                    jsonZACities = reader.ReadToEnd();
                }

                Cities = JsonConvert.DeserializeObject<List<City>>(jsonZACities).OrderBy(x => x.name).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
