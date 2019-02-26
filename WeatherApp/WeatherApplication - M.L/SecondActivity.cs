using System;
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
    /// <summary>
    /// 5 Day Forecast
    /// Each day has it's Visibility, WeatherIcon, Max temperature and Min temperature
    /// Using ListView
    /// </summary>
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : Activity
    {
        ListView list;
        TextView cityEntry;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.secondView);

            list = FindViewById<ListView>(Resource.Id.listView1);
            
            Button weatherButton = FindViewById<Button>(Resource.Id.weatherBtn);
            weatherButton.Click += WeatherButton_Click;
            // Create your application here
        }

        private void WeatherButton_Click(object sender, EventArgs e)
        {
            cityEntry = FindViewById<TextView>(Resource.Id.cityEntry2);
            Forecast();
        }

        private async void Forecast()
        {
            var forecasts = await Core.GetForecast(cityEntry.Text);

            if (forecasts != null)
            {
                List<int> images = new List<int>();

                foreach (Forecast weather in forecasts)
                {
                    images.Add(Resources.GetIdentifier(weather.Visibility, "drawable", PackageName));
                }

                list.Adapter = new CustomAdapter(this, forecasts, images);
            }
        }
    }
}