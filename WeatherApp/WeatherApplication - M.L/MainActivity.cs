using System;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Text.Style;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Android.Content;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace WeatherApp
{
    [Activity(Label = "WeatherApp.Android",
                 Theme = "@style/MyTheme",
                 MainLauncher = true)]

    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {

            AppCenter.Start("2a85051c-b6f8-4f11-bd1a-e8d9e1fd4d24", typeof(Analytics), typeof(Crashes));

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);

            Button button = FindViewById<Button>(Resource.Id.weatherBtn);
            Button forecastButton = FindViewById<Button>(Resource.Id.forecastBtn);

            button.Click += Button_Click;
            forecastButton.Click += ForecastButton_Click;

            var editToolbar = FindViewById<Toolbar>(Resource.Id.edit_toolbar);

            editToolbar.Title = "Editing";
            editToolbar.InflateMenu(Resource.Menu.edit_menu);

            editToolbar.MenuItemClick += (sender, e) => {
                Toast.MakeText(this, "Bottom toolbar tapped: " + e.Item.TitleFormatted, ToastLength.Short).Show();
            };

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {

            MenuInflater.Inflate(Resource.Menu.top_menu, menu);
            return base.OnCreateOptionsMenu(menu);

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);

        }

        private void ForecastButton_Click(object sender, EventArgs e)
        {

            var secondActivity = new Intent(this, typeof(SecondActivity));
            StartActivity(secondActivity);

        }

        private async void Button_Click(object sender, EventArgs e)
        {
            ImageView ivVisibility = FindViewById<ImageView>(Resource.Id.imageViewVisibility);
            EditText cityEntry = FindViewById<EditText>(Resource.Id.cityEntry);

            if (!string.IsNullOrEmpty(cityEntry.Text))
            {
                Weather weather = await Core.GetWeather(cityEntry.Text);
                
                if (weather != null)
                {

                    FindViewById<TextView>(Resource.Id.tempText).Text = "Min temp: " + weather.Temperature_Min + " Max temp: " + weather.Temperature_Max;
                    FindViewById<TextView>(Resource.Id.windText).Text = weather.Wind;
                    FindViewById<TextView>(Resource.Id.pressureText).Text = weather.Pressure;

                    switch (weather.Visibility)
                    {
                        case "Clouds":

                            ivVisibility.SetImageResource(Resource.Drawable.clouds);
                            ivVisibility.Visibility = ViewStates.Visible;
                            break;

                        case "Clear":

                            ivVisibility.SetImageResource(Resource.Drawable.sunny);
                            ivVisibility.Visibility = ViewStates.Visible;
                            break;

                        case "Mist":

                            ivVisibility.SetImageResource(Resource.Drawable.mist2);
                            ivVisibility.Visibility = ViewStates.Visible;
                            break;

                        case "Rain":

                            ivVisibility.SetImageResource(Resource.Drawable.rain2);
                            ivVisibility.Visibility = ViewStates.Visible;
                            break;

                        case "Fog":

                            ivVisibility.SetImageResource(Resource.Drawable.fog);
                            ivVisibility.Visibility = ViewStates.Visible;
                            break;

                    }

                    FindViewById<TextView>(Resource.Id.visibilityText).Text = weather.Visibility;

                }
            }
        }
    }
}