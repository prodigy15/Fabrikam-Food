using Fabrikam_Food.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Fabrikam_Food.Views
{
    public partial class MenuListPage : ContentPage
    {
        public MenuListPage()
        {
            InitializeComponent();
            Title = "Menu List";
        }
        private async void ViewMenu_Clicked(Object sender, EventArgs e)
        {
            List<Menu> menulist = await AzureManager.AzureManagerInstance.GetMenu();

            MenuList.ItemsSource = menulist;

        }
        /*
        protected override async void OnAppearing()
        {
            await Task.Run(() => ViewMenu());
        }*/
    }
}
