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
        GeocodeMapObject.RootObject grootObject;
        String distance = "";
        String duration = "";
        Label distanceLabel;
        Label travelTimeLabel;
        Label addressLabel;
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
            Button geocodeButton = new Button { Text = "MAP ANALYTICS(GPS)", };
            addressLabel = new Label { Text = "Address" };
            distanceLabel = new Label { Text = "Distance" };
            travelTimeLabel = new Label { Text = "Travel Time" };
            stack.Children.Add(geocodeButton);
            stack.Children.Add(addressLabel);
            stack.Children.Add(distanceLabel);
            stack.Children.Add(travelTimeLabel);
            geocodeButton.Clicked += OnButton_Clicked;
        }

        public async Task getPosition()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var pos = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            currentPosition = new Position(pos.Latitude, pos.Longitude);
            //var load2 = Task.Run(() => getDistance(currentPosition));
        }

        public async void getDistance(Position x)
        {
            HttpClient hc1 = new HttpClient();
            string res = await hc1.GetStringAsync("https://maps.googleapis.com/maps/api/geocode/json?latlng="+ x.Latitude + "," + x.Longitude +"&key=AIzaSyD4jf0zzdaPwcbOPHlcAMINJCAAO_Z6T-Q");
            grootObject = JsonConvert.DeserializeObject<GeocodeMapObject.RootObject>(res);
            var address = grootObject.results[0].formatted_address;

            string data = x.Latitude + "," + x.Longitude;
            string uri = "https://maps.googleapis.com/maps/api/directions/json?origin="+ address +"&destination=250+Queen+St,+Auckland&key=AIzaSyD4jf0zzdaPwcbOPHlcAMINJCAAO_Z6T-Q";

            HttpClient httpclient = new HttpClient();
            string response = await httpclient.GetStringAsync(uri); // response stored as string
            //Dserialize into a googlemapobject class
            rootObject = JsonConvert.DeserializeObject<GoogleMapObject.RootObject>(response);
            distance = rootObject.routes[0].legs[0].distance.text;
            duration = rootObject.routes[0].legs[0].duration.text;

            distanceLabel.Text = "Distance: " + distance + " from Fabrikam Food"; 
            travelTimeLabel.Text = "Travel Time: " + duration + " from Fabrikam Food";
            addressLabel.Text = "Your current Address is " + address;

            //string information = $"From Geocode {x.Latitude}, {x.Longitude} to 250 Queen Street will take approximately {duration} and is a total of {distance}.";
            //await DisplayAlert("Map Analytics:", information, "OK");

        }
        async void OnButton_Clicked(object sender, EventArgs e)
        {
            await getPosition();
            getDistance(currentPosition);
        }
        /* Time constraint, error 
        public string GetDistance() { return distance; }
        public Position GetPosition() { return currentPosition; }
        public string GetDuration() { return duration; }
        */
    }
}