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
using Xamarin.Forms.GoogleMaps;

namespace Fabrikam_Food.Views
{
    public class MapPage : ContentPage
    {
        Position currentPosition;
        Position arbitrary = new Position(-36.8530660, 174.7712930);
        GoogleMapObject.RootObject rootObject;
        String distance = "";
        String duration = "";
        public MapPage()
        {
            Title = "Map View";
            BackgroundColor = Color.FromHex("FFEBD0");
            var load = Task.Run(() => getPosition());
            var map = new Map(
                MapSpan.FromCenterAndRadius(arbitrary, Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(-36.8519210, 174.7624020),
                Label = "custom pin",
                Address = "custom detail info"
            };
            map.Pins.Add(pin);

            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;

            var slider = new Slider(1, 18, 1);
            slider.ValueChanged += (sender, e) => {
                var zoomLevel = e.NewValue; // between 1 and 18
                var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
                map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            };


        }

        public async void getPosition()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var pos = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            currentPosition = new Position(pos.Latitude, pos.Longitude);
            //var load2 = Task.Run(() => getDistance(currentPosition));
        }

        public async void getDistance(Position x)
        {
            string data = x.Latitude + "," + x.Longitude;
            string uri = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={data}&destinations=-36.8519210,174.7624020&mode=bicycling&language=fr-FR&key=AIzaSyBstZwaR9vonfm-Xk9ZyevpiAFRSCrfS_s";

            HttpClient httpclient = new HttpClient();
            string response = await httpclient.GetStringAsync(uri);

            rootObject = JsonConvert.DeserializeObject<GoogleMapObject.RootObject>(response);

            distance = rootObject.routes[0].legs[0].distance.text;

        }
        /* Time constraint, error 
        public string GetDistance() { return distance; }
        public Position GetPosition() { return currentPosition; }
        public string GetDuration() { return duration; }
        */
    }
}
