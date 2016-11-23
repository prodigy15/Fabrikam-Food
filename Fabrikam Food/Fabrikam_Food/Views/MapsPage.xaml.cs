using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Fabrikam_Food.Views
{
    public partial class MapsPage : ContentPage
    {
        public MapsPage()
        {
            Title = "Map";
            
        }
        public async void Button_OnClicked(object sender, EventArgs e)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            LongLabel.Text = position.Longitude.ToString();
            LatLabel.Text = position.Latitude.ToString();
            
        }


        
    }
}
