using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

using Xamarin.Forms;

namespace Fabrikam_Food.Views
{
    public partial class HomePage : ContentPage
    {
        bool authenticated = false;
        public HomePage()
        {
            InitializeComponent();
            Title = "Login Page";
            BackgroundColor = Color.FromHex("FFEBD0");
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (authenticated == true)
            {
                // Hide the Sign-in button.
                this.loginButton.IsVisible = false;
            }
        }
        async void loginButton_Clicked(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
                authenticated = await App.Authenticator.Authenticate();

            if (authenticated)
            {
                this.loginButton.IsVisible = false;
                LoginDescription.Text = "Welcome, " + AzureManager.AzureManagerInstance.AzureClient.CurrentUser.UserId;
            }
        }
    }
}
