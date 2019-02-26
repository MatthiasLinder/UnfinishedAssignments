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
    public class CustomAdapter : BaseAdapter<Forecast>
    {
        List<Forecast> items;
        List<int> images;
        Activity context;


        public CustomAdapter(Activity context, List<Forecast> items, List<int> images) : base()
        {

            this.context = context;
            this.images = images;
            this.items = items;

        }

        public override Forecast this[int position]
        {

            get { return items[position]; }

        }

        public override int Count { get { return items.Count; } }

        public override long GetItemId(int position)
        {

            return position;

        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            View view = convertView;

            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);

            view.FindViewById<TextView>(Resource.Id.dateTxt).Text = items[position].Date;
            view.FindViewById<ImageView>(Resource.Id.weatherPicture).SetImageResource(images[position]);
            view.FindViewById<TextView>(Resource.Id.tempMin).Text = items[position].Temperature_Min;
            view.FindViewById<TextView>(Resource.Id.tempMax).Text = items[position].Temperature_Max;

            return view;

        }
    }
}