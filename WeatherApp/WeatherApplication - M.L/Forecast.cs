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
    public class Forecast
    {
        public string Date { get; set; } = " ";
        public string Temperature_Min { get; set; } = " ";
        public string Temperature_Max { get; set; } = " ";
        public string Visibility { get; set; } = " ";
    }
}