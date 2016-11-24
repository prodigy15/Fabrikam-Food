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

            //populate main party size picker
            MainPicker.Items.Add("1");
            MainPicker.Items.Add("2");
            MainPicker.Items.Add("3");
            MainPicker.Items.Add("4");
            MainPicker.Items.Add("5");
            MainPicker.Items.Add("6");
            MainPicker.Items.Add("7");
            MainPicker.Items.Add("8");
            MainPicker.Items.Add("9");

            //var load = Task.Run(() => LoadReservations());
        }

        private async void LoadReservations()
        {
            Datepicker.IsEnabled = false;
            PeriodPicker.IsEnabled = false;
            MainPicker.IsEnabled = false;

            //await DisplayAlert("Test", "STARTING", "OK");
            reservationList = await AzureManager.AzureManagerInstance.GetReservation();
           // await DisplayAlert("Test", "END", "OK");


            Datepicker.IsEnabled = true;
            PeriodPicker.IsEnabled = true;
            MainPicker.IsEnabled = true; 
        }
        private async void DatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            LoadReservations();
            query[0] = e.NewDate.ToString().Substring(0, 10);
            await DisplayAlert("test", $"Set: {query[0]}", "OK");
        }
        private void PeriodPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var period = PeriodPicker.Items[PeriodPicker.SelectedIndex];
            period = e.ToString();

        }
        private async void MainPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            // 
            var partySize = MainPicker.Items[MainPicker.SelectedIndex]; // << this may be problemtic too idk
            // nah i found that from a tutorial so that is safe oo k
            query[1] = e.ToString(); // < size is 2 u cant have index 2 hahaha knew ethat 
            checkAvailability();
        }

        //check whether reservation is available or not based on user query
        private async void checkAvailability()
        {
            // To update:
            // 1. get an item from reservation list which u think needs updating
            // 2. Update the desired parameters
            // 3. Repost
            // Example:
            //var updatedReservation = reservationList[0];    // <-- this is your new reservatiopn with modified parameters
            // This is useful because it maintains [ string ID ] which will be used by the database as primary key 
            // to know which row needs to be modified otherwise u gotta make a new one - nvm too long
            //so i can't use linq...? otherwise its hard part?
            // u can but its not matching b0ss hmm ok ty for ur time bv0ss allgood
            //updatedReservation.HT_Availability = 5;
            //await AzureManager.AzureManagerInstance.UpdateReservation(updatedReservation);
            // thats the hard part lol
            // hey one last thing
            // if ii get the row from linq, doesn't it return ienumerable
            // do i have to create a new reservation object and update it as that? 

            // thats thew hard way cos u needa make sure ID are matching and u wouldve screwed up on that
            // how do i get 1. then ?
            // U gotta match a certain parameter to retrieve it like how u were doin before 
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
