using Fabrikam_Food.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fabrikam_Food
{
    public class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            BackgroundColor = Color.FromHex("FFEBD0");
            var menuPage = new MenuPage();
            menuPage.BackgroundColor = Color.FromHex("FFEBD0");
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);

            Master = menuPage;
            var detail = new NavigationPage(new FirstPage());
            detail.BarBackgroundColor = Color.FromHex("FFC77A");
            detail.BarTextColor = Color.White;
            Detail = detail;

        }

        void NavigateTo(MenuItem menu)
        {
            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);
            var detail = new NavigationPage(displayPage);
            detail.BarBackgroundColor = Color.FromHex("CC9F62");
            detail.BarTextColor = Color.White;
            Detail = detail;

            IsPresented = false;
        }
    }
}
