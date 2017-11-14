using Microsoft.Toolkit.Uwp.UI.Controls.TextToolbarSymbols;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPCombatApp;
using System.IO;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace Model
{
    /* This class is where all the interaction with the azure easy table takes place */
    class CombatDrillsTable
    {
       // Create a local mobile collection for storing results and a SyncSericeTable object for inetacting with Azure
       private MobileServiceCollection<DrillItem, DrillItem> drills;
       private IMobileServiceSyncTable<DrillItem> drillTable = App.MobileService.GetSyncTable<DrillItem>();

        //Construct page
       public CombatDrillsTable()
       {
            Initialization = InitializeAsync();
       }

       // Get initilized task
       public Task Initialization { get; private set; }

       private async Task InitializeAsync()
        {
            // Asynchronously initialize this instance.
            await InitLocalStoreAsync();
        }

        // Sync up service
        private async Task SyncAsync()
        {
            await App.MobileService.SyncContext.PushAsync();
            await drillTable.PullAsync("drillItem", drillTable.CreateQuery());
        }

        // Get drills
        public MobileServiceCollection<DrillItem, DrillItem> GetDrills()
        {
            return this.drills;
        }

        // Add drill to database
        public async Task AddDrill(DrillItem drillItem, String n, int s, int t, string sty, string use)
        {
            drillItem.Name = n;
            drillItem.Sets = s;
            drillItem.SetTime = t;
            drillItem.Style = sty;
            drillItem.Use = use;
            await App.MobileService.GetTable<DrillItem>().InsertAsync(drillItem);
            drills.Add(drillItem);
        }

        // Initilize local storage
        public async Task InitLocalStoreAsync()
        {
            if (!App.MobileService.SyncContext.IsInitialized)
            {
                var store = new MobileServiceSQLiteStore("localstore.db");
                store.DefineTable<DrillItem>();
                await App.MobileService.SyncContext.InitializeAsync(store);
            }

            await SyncAsync();
        }

        // Get drills by style
        public async Task GetDrillsAsync(String cat)
        {
            await InitLocalStoreAsync();
            MobileServiceInvalidOperationException exception = null;
            try { 
            drills = await drillTable.Where(drillItem => drillItem.Style == cat)
                    .ToCollectionAsync();
                Console.WriteLine(drills);
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }
            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            

        }

        // Delete drill from database
        public async Task DeleteDrillAsync(string id)
        {
            Console.WriteLine(id);
            MobileServiceInvalidOperationException exception = null;
            DrillItem d = new DrillItem();
            d.Id = id;
            try {
                await drillTable.DeleteAsync(d);
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error deleting item").ShowAsync();
            }
        }

        // Update drill in database
        public async Task UpdateDrill( String Id, String n, int s, int t, string sty, string use)
        {
            DrillItem drillItem = new DrillItem();
            drillItem.Id = Id;
            drillItem.Name = n;
            drillItem.Sets = s;
            drillItem.SetTime = t;
            drillItem.Style = sty;
            drillItem.Use = use;
            await App.MobileService.GetTable<DrillItem>().UpdateAsync(drillItem);
        }

    }
}
