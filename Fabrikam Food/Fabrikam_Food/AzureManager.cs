using Fabrikam_Food.DataModels;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrikam_Food
{
    public class AzureManager
    {
        private static AzureManager instance;
        private MobileServiceClient client;

        //used for table references
        private IMobileServiceTable<Menu> menuTable;
        private IMobileServiceTable<Reservations> reservationTable;

        private AzureManager()
        {
            // Azure connection 
            this.client = new MobileServiceClient("http://fabrikamsolution.azurewebsites.net/");
            //db
            this.menuTable = this.client.GetTable<Menu>();
            this.reservationTable = this.client.GetTable<Reservations>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }

        // Retrieve information about Menu table
        public async Task<List<Menu>> GetMenu()
        {
            return await this.menuTable.ToListAsync();
        }

        public async Task<List<Reservations>> GetReservation()
        {
            return await this.reservationTable.ToListAsync();
        } 

        public async Task PostReservation(Reservations res)
        {
            await reservationTable.InsertAsync(res);
        }
        public async Task UpdateReservation(Reservations res)
        {
            await reservationTable.UpdateAsync(res);
        }
    }
}
