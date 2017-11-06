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
    class CombatDrillsTable
    {
        private MobileServiceCollection<DrillItem, DrillItem> drills;
        private IMobileServiceSyncTable<DrillItem> drillTable = App.MobileService.GetSyncTable<DrillItem>();

        public CombatDrillsTable()
        {
            Initialization = InitializeAsync();
        }

        public Task Initialization { get; private set; }

       private async Task InitializeAsync()
        {
            // Asynchronously initialize this instance.
            await InitLocalStoreAsync();
            
        }

        private async Task SyncAsync()
        {
            await App.MobileService.SyncContext.PushAsync();
            await drillTable.PullAsync("drillItem", drillTable.CreateQuery());
        }

        public MobileServiceCollection<DrillItem, DrillItem> GetDrills()
        {
            return this.drills;
        }

        public async Task AddDrill(DrillItem drillItem, String n, int s, int t, string sty)
        {
            drillItem.Name = n;
            drillItem.Sets = s;
            drillItem.SetTime = t;
            drillItem.Style = sty;

            await App.MobileService.GetTable<DrillItem>().InsertAsync(drillItem);
            drills.Add(drillItem);
        }

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

        public async Task UpdateDrill( String Id, String n, int s, int t, string sty)
        {
            DrillItem drillItem = new DrillItem();
            drillItem.Id = Id;
            drillItem.Name = n;
            drillItem.Sets = s;
            drillItem.SetTime = t;
            drillItem.Style = sty;
            await App.MobileService.GetTable<DrillItem>().UpdateAsync(drillItem);
        }

    }
}
