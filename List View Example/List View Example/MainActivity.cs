using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace List_View_Example
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : ListActivity
    {
        readonly string[] MarvelCharacters = new string[] { "Captain America", "Iron Man", "Doctor Strange", "Spider Man", "The Hulk" };

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);


            ListAdapter = new CustomAdapter(this, MarvelCharacters);

            ListView.ItemClick += ListView_ItemClick;

        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            Toast.MakeText(this, MarvelCharacters[e.Position], ToastLength.Short).Show();

        }
    }
}