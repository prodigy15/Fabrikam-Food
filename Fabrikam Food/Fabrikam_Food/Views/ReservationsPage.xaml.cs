using Fabrikam_Food.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Fabrikam_Food.Views
{
    public partial class ReservationsPage : ContentPage
    {
        List<Reservations> reservationList;
        Reservations currentReservation;
        String[] query = new String[2];
        String period = "";
        bool availability = false;
        public ReservationsPage()
        {
            InitializeComponent();
            //populate meal period picker
            PeriodPicker.Items.Add("Lunch");
            PeriodPicker.Items.Add("High Tea");
            PeriodPicker.Items.Add("Dinner");

            //var load = Task.Run(() => LoadReservations());
        }

        private async void LoadReservations()
        {
            Datepicker.IsEnabled = false;
            PeriodPicker.IsEnabled = false;

            //await DisplayAlert("Test", "STARTING", "OK");
            reservationList = await AzureManager.AzureManagerInstance.GetReservation();
           // await DisplayAlert("Test", "END", "OK");

            Datepicker.IsEnabled = true;
            PeriodPicker.IsEnabled = true;
        }
        private async void DatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            LoadReservations();
            query[0] = e.NewDate.ToString().Substring(0, 10);
            await DisplayAlert("Retrieving data...", "Please wait a moment... ", "OK");
        }
        private void PeriodPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var period = PeriodPicker.Items[PeriodPicker.SelectedIndex];
            period = e.ToString();
            query[1] = period;

        }
        
        private async void AddReservation()
        {
            
            currentReservation = new Reservations
            {
                User = AzureManager.AzureManagerInstance.AzureClient.CurrentUser.UserId,
                Date = query[0],
                Period = query[1]
            };
            await AzureManager.AzureManagerInstance.PostReservation(currentReservation);
            await DisplayAlert("Successful query", "Your reservation has been successful", "OK");
        }
        private async void ChangeReservation()
        {
            //await AzureManager.AzureManagerInstance.UpdateReservation((reservationList.Where(s => reservationList.Any(s1 => s.User == AzureManager.AzureManagerInstance.AzureClient.CurrentUser.UserId))).ToList());

        }
        
        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            AddReservation();
            await DisplayAlert("Adding data...", "Please wait a moment... ", "OK");
        }
        private async void ChangeButton_Clicked(object sender, EventArgs e)
        {
            ChangeReservation();
        }
        /*
         var r = reservationList.Where(s => reservationList.Any(s1 => s.Date == query[0]));
         var e = reservationList.Where(s => reservationList.Any(s1 => s.Date == query[0])).
                                 Where(s => reservationList.Any(s1 => s.L_Availability >= Int32.Parse(query[1])));
         await DisplayAlert("title", ""+r.Count(), "OK");
         await DisplayAlert("title", ""+e.Count(), "OK");
         switch (period) 
          {
             case("Lunch"):
                 if (reservationList.Any(s => s.Date == query[0]))
                 {
                     await DisplayAlert("t1", "date exists", "OK");
                 }
                 if (reservationList.Any(s => s.L_Availability >= Int32.Parse(query[1])))
                 {
                     await DisplayAlert("t2", "availabilityexists", "OK");
                 }
                 var dl1 = reservationList.Where(
                     x => x.Date == query[0] &&                      // same date
                     x.L_Availability >= Int32.Parse(query[1]));     // less people than availbe
                 if (e.Count() > 0) {
                     availability = true;
                     await DisplayAlert("Test", "entered", "OK");    
                     // This isnt entered, meaning count is 0 -< Date ios at fault //true
                 } 
                 break;
             case ("High Tea"):
                 var dl2 = reservationList.Where(x => x.Date == query[0]
                 && x.HT_Availability >= Int32.Parse(query[1]));
                 if (dl2.Count() > 0) availability = true;
                 break;
             case ("Dinner"):
                 var dl3 = reservationList.Where(x => x.Date == query[0]
                 && x.D_Availability >= Int32.Parse(query[1]));
                 if (dl3.Count() > 0) availability = true;
                 break;
         }
     }
//var res = new Reservation { Date = query[0], }
         //AzureManager.AzureManagerInstance.UpdateReservation();
         /*
     private async void ReserveButton_OnClick(object sender, EventArgs e)
     {
         switch (period)
         {
             case ("Lunch"):
             //    var res = new Reservation { Date = query[0], }
             //    await AzureManager.AzureManagerInstance.UpdateReservation(res);
             //    break;
             case ("High Tea"):

                 break;
            // case ("Dinner"):
         }
     }*/
    }
}
