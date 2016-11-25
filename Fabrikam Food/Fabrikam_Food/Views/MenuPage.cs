using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fabrikam_Food
{
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }

        public MenuPage()
        {
            Icon = "settings.png";
            Title = "Menu"; 
            BackgroundColor = Color.FromHex("FFC77A");

            Menu = new MenuListView();

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.Black,
                    Text = "MENU",
                    FontSize = 20,
                }
            };

            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            menuLabel.BackgroundColor = Color.FromHex("FFEBD0");
            layout.Children.Add(menuLabel);
            layout.Children.Add(Menu);

            Content = layout;
        }
    }
}
