using Fabrikam_Food.DataModels;
using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Fabrikam_Food.Views
{
    public partial class MapAnalytics : ContentPage
    {
        GoogleMapObject.RootObject rootObject;
        Position currentPosition ;
        String distance = "";
        String duration = "";
        public MapAnalytics()
        {
            InitializeComponent();
            Title = "Map Analytics";
            BackgroundColor = Color.FromHex("FFEBD0");
        }
        public async void getDistance(Position x)
        {
            string data = x.Latitude + "," + x.Longitude;
            string uri = "https://maps.googleapis.com/maps/api/directions/json?origin=OGGB&destination=250+Queen+St,+Auckland&key=AIzaSyD4jf0zzdaPwcbOPHlcAMINJCAAO_Z6T-Q";

            HttpClient httpclient = new HttpClient();
            string response = await httpclient.GetStringAsync(uri); // response stored as string
            //Dserialize into a googlemapobject class
            rootObject = JsonConvert.DeserializeObject<GoogleMapObject.RootObject>(response);
            distance = rootObject.routes[0].legs[0].distance.text;
            duration = rootObject.routes[0].legs[0].duration.text;

            string information = $"From Geocode {x.Latitude}, {x.Longitude} to 250 Queen Street will take approximately {duration} and is a total of {distance}.";
            await DisplayAlert("Map Analytics:", information, "OK");

        }
        public async void getPosition()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var pos = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            currentPosition = new Position(pos.Latitude, pos.Longitude);

            var load2 = Task.Run(() => getDistance(currentPosition));
        }

        public async void MapDataButton_Clicked(Object sender, EventArgs e)
        {
            getDistance(new Position(36.853066, 174.771293));
        }
    }
}
