using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWPCombatApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWPCombatApp.Views
{

    public sealed partial class DisplayDrills : Page
    {
        // Variables 
        String catagory;
        CombatTableView ctv = new CombatTableView();
        private ObservableCollection<DrillItem> _items;
        private ObservableCollection<DrillItem> _temp;
        String id;

        // Construct class, Init component and populate the list
        public DisplayDrills()
        {
            this.InitializeComponent();
            _items = new ObservableCollection<DrillItem>();
        }

        /* When the page is navigated to collect value sent in parameters(this is the fighting style)
         * Then initilize the table for the drills, create local storage and sync up the drills
           Then add the items to the refresh list source, Error handling in catch too */
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            catagory = (String)e.Parameter;
            try
            {
                await ctv.combatDrillsTable.Initialization;
                await ctv.combatDrillsTable.InitLocalStoreAsync();
                await AddItemsAsync();
            }
            catch
            {
                var addError = new MessageDialog("Connection to your drills could not be established at this time, returning to" +
                    "main menu");
                await addError.ShowAsync();
                if (MainPage.MyFrame.CanGoBack)
                {
                    MainPage.MyFrame.GoBack();
                }
            }
            RefreshListView.ItemsSource = _items;
        }

        // Get most updated drills
        private async Task updateDrillsAsync() { 

            await ctv.combatDrillsTable.GetDrillsAsync(catagory);
        }

        // Get drills to database and insert them in list
        private async Task AddItemsAsync()
        {
            await updateDrillsAsync();
            _temp = ctv.combatDrillsTable.GetDrills();

            foreach (var t in _temp)
            {
                _items.Insert(0, t);
            }

            loadTxt.Visibility = Visibility.Collapsed;
        }

        // Get drills and update list when user pulls list down
        private async void ListView_RefreshCommand(object sender, EventArgs e)
        {
            loadTxt.Visibility = Visibility.Visible;
            _items = ctv.combatDrillsTable.GetDrills();
           await AddItemsAsync();
            loadTxt.Visibility = Visibility.Collapsed;
        }

        private void ListView_RefreshIntentCanceled(object sender, EventArgs e)
        {
        }

        // Popup window for adding new drills
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ppup.Height = Window.Current.Bounds.Height;
            ppup.IsOpen = true;
        }

        // Add a new drill to database
        public async void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            DrillItem drillItem = new DrillItem();
            String Name = NameBox.Text;
            String Use = (String)UseCombo.SelectionBoxItem;
            int Sets;
            int Time;
            bool successfullyParsedTime = int.TryParse(SetsBox.Text, out Time);
            bool successfullyParsedSets = int.TryParse(TimeBox.Text, out Sets);

            if (successfullyParsedSets)
            {
                Sets = Int32.Parse(SetsBox.Text);
                
            }
            if (successfullyParsedTime)
            {
                Time = Int32.Parse(TimeBox.Text);
            }
            
                await ctv.combatDrillsTable.AddDrill(drillItem, Name, Sets, Time, catagory, Use);
                ppup.IsOpen = false;
                var addSuccess = new MessageDialog("Drill added to database");
                await addSuccess.ShowAsync();
           
        }

        // Go back to main menu
        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            if (MainPage.MyFrame.CanGoBack)
            {
                MainPage.MyFrame.GoBack();
            }
        }

        // Open delete popup and retrieve id of item to be deleted
        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            delpup.Height = Window.Current.Bounds.Height;
            delpup.IsOpen = true;
            id = (((Button)sender).Tag).ToString();
        }

        // Update item with values entered
        private async void NewSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            String Name = NewNameBox.Text;
            String Use = NewUseCombo.SelectionBoxItem.ToString();
            int Sets;
            int Time;
            bool successfullyParsedTime = int.TryParse(NewSetsBox.Text, out Time);
            bool successfullyParsedSets = int.TryParse(NewTimeBox.Text, out Sets);

            if (successfullyParsedSets)
            {
                Sets = Int32.Parse(NewSetsBox.Text);

            }
            if (successfullyParsedTime)
            {
                Time = Int32.Parse(NewTimeBox.Text);
            }
            
                await ctv.combatDrillsTable.UpdateDrill(id, Name, Sets, Time, catagory, Use);
            
            ppup.IsOpen = false;
            var addSuccess = new MessageDialog("Drill Updated");
            await addSuccess.ShowAsync();

        }

        // Display info in provided text files
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            string text;
            if (catagory == "Karate")
            {
                text = System.IO.File.ReadAllText(@"Info/Karate.txt");
                InfoText.Text = text;
            }
            else if (catagory == "Boxing")
            {
                text = System.IO.File.ReadAllText(@"Info/Boxing.txt");
                InfoText.Text = text;
            }
            else if (catagory == "TKD")
            {
                text = System.IO.File.ReadAllText(@"Info/TKD.txt");
                InfoText.Text = text;
            }
            else if (catagory == "Kickboxing")
            {
                text = System.IO.File.ReadAllText(@"Info/Kickboxing.txt");
                InfoText.Text = text;
            }
            else if (catagory == "MuayThai")
            {
                text = System.IO.File.ReadAllText(@"Info/MuayThai.txt");
                InfoText.Text = text;
            }
            else if (catagory == "BJJ")
            {
                text = System.IO.File.ReadAllText(@"Info/BJJ.txt");
                InfoText.Text = text;
            }
            else if (catagory == "Judo")
            {
                text = System.IO.File.ReadAllText(@"Info/Judo.txt");
                InfoText.Text = text;
            }
            else if (catagory == "MMA")
            {
                text = System.IO.File.ReadAllText(@"Info/MMA.txt");
                InfoText.Text = text;
            }
            else
            {
                InfoText.Text = "Error Loading Text";
            }
            
            

            infopup.Height = Window.Current.Bounds.Height;
            infopup.IsOpen = true;
        }

        // Delete this item
        private async void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("id: " + id);
            await ctv.combatDrillsTable.DeleteDrillAsync(id);
            var addSuccess = new MessageDialog("Drill Deleted");
            await addSuccess.ShowAsync();

        }

        // Cancel delete
        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            delpup.IsOpen = false;
        }

        // Update item popup
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            updateppup.Height = Window.Current.Bounds.Height;
            updateppup.IsOpen = true;
            id = (((Button)sender).Tag).ToString();
        }
    }
}
