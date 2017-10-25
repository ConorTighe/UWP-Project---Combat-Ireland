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

namespace Model
{
    class CombatDrillsTable
    {
        private MobileServiceCollection<DrillItem, DrillItem> drills;
        private IMobileServiceSyncTable<DrillItem> drillTable = App.MobileService.GetSyncTable<DrillItem>();

        public CombatDrillsTable()
        {
            
        }

        protected async void OnNavigatedTo(NavigationEventArgs e)
        {
            await GetDrillsAsync();
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

        public async void GetById(string n)
        {
            IMobileServiceTableQuery<DrillItem> query = drillTable.Where(drillItem => drillItem.Name == n)
                .Select(drillItem => drillItem);
            List<DrillItem> items = await query.ToListAsync();
            Console.WriteLine(items);
        }

        public async Task GetDrillsAsync()
        {
            MobileServiceInvalidOperationException exception = null;
            try { 
            drills = await drillTable
            .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }
            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                // code here
            }

        }

        public async Task DeleteDrillAsync(DrillItem d)
        {
            MobileServiceInvalidOperationException exception = null;
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
            drillItem.Name = n;
            drillItem.Sets = s;
            drillItem.SetTime = t;
            drillItem.Style = sty;
            await App.MobileService.GetTable<DrillItem>().UpdateAsync(drillItem);
        }

    }
}
