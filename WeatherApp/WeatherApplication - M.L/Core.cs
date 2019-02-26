using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WeatherApp
{
    public class Core
    {

#region GetWeather

        public static async Task<Weather> GetWeather(string city)
        {

            string key = "a316892ef34d729320d6d415178d8981";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?q="
                + city + "&appid=" + key + "&units=metric";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                Weather weather = new Weather();

                weather.Title = (string)results["name"];
                weather.Temperature_Min = (string)results["main"]["temp_min"] + " C";
                weather.Temperature_Max = (string)results["main"]["temp_max"] + " C";
                weather.Wind = (string)results["wind"]["speed"] + " m/s";
                weather.Pressure = (string)results["main"]["pressure"] + "hPa";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                weather.Visibility = (string)results["weather"][0]["main"];

                DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);

                weather.Sunrise = sunrise.ToString() + " UTC";
                weather.Sunset = sunset.ToString() + " UTC";

                return weather;

            }

            else
            {

                return null;

            }

        }

#endregion


#region GetForecast

        public static async Task<List<Forecast>> GetForecast(string inputID)
        {

            string key = "c1ed9cd699d44dd86d769a257cf3d07f";
            string queryString = "http://api.openweathermap.org/data/2.5/forecast?q=" + inputID + "&units=metric&appid=" + key;
            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);

            var forecast = new List<Forecast>();

            int currentIterator = 0;

            for (int i = 0; i < 5; i++)
            {
                Forecast weather = new Forecast();

                weather.Date = UnixTimeToString((long)results["list"][currentIterator]["dt"]);
                weather.Temperature_Min = (string)results["list"][currentIterator]["main"]["temp_min"] + " C";
                weather.Temperature_Max = (string)results["list"][currentIterator]["main"]["temp_max"] + " C";
                weather.Visibility = "_" + (string)results["list"][currentIterator]["weather"][0]["icon"];
                weather.Date = UnixTimeToString((long)results["list"][currentIterator]["dt"]);

                currentIterator += 8;
                forecast.Add(weather);

            }

            return forecast;
        }

#endregion



        public static string UnixTimeToString(long dt)
        {

            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(dt).ToLocalTime();
            return dateTime.ToString("dd/MM/yyyy");

        }
    }
}