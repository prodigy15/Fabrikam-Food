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
            this.WaitLabel.IsVisible = false;
            this.EntreeLabel.IsVisible = false;
            this.SideLabel.IsVisible = false;
            this.MainLabel.IsVisible = false;
            this.DessertLabel.IsVisible = false;
        }
        private async void ViewMenu_Clicked(Object sender, EventArgs e)
        {
            this.WaitLabel.IsVisible = true;
            List<Menu> menulist = await AzureManager.AzureManagerInstance.GetMenu();
            this.MenuButton.IsVisible = false;
            this.WaitLabel.IsVisible = false;
            this.EntreeLabel.IsVisible = true;
            this.SideLabel.IsVisible = true;
            this.MainLabel.IsVisible = true;
            this.DessertLabel.IsVisible = true;
            //EntreeList.ItemsSource = menulist;
            EntreeList.ItemsSource = menulist.Where(tl => menulist.Any(t => tl.Type =="Entree"));
            SideList.ItemsSource = menulist.Where(tl => menulist.Any(t => tl.Type == "Sides"));
            MainList.ItemsSource = menulist.Where(tl => menulist.Any(t => tl.Type == "Main"));
            DessertList.ItemsSource = menulist.Where(tl => menulist.Any(t => tl.Type == "Dessert"));

        }
        /*
        protected override async void OnAppearing()
        {
            await Task.Run(() => ViewMenu());
        }*/
    }
}
