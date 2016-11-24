using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Fabrikam_Food.Views
{
    public class MapPage : ContentPage
    {
        Position currentPosition;
        public MapPage()
        {
            var load = Task.Run(() => getPosition());
            var map = new Map(
                MapSpan.FromCenterAndRadius(
                        currentPosition, Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;

            var slider = new Slider(1, 18, 1);
            slider.ValueChanged += (sender, e) => {
                var zoomLevel = e.NewValue; // between 1 and 18
                var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
                map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            };
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = currentPosition,
                Label = "custom pin",
                Address = "custom detail info"
            };
            map.Pins.Add(pin);
        }

        public async void getPosition()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
            var pos = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            currentPosition = new Position(pos.Latitude, pos.Longitude);
        }

    }
}
