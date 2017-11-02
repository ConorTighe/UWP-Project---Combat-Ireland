using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCombatApp.Views
{

    public sealed partial class DisplayDrills : Page
    {

        String catagory;
        CombatTableView ctv = new CombatTableView();
        private ObservableCollection<DrillItem> _items;
        private ObservableCollection<DrillItem> _temp;

        public DisplayDrills()
        {
            this.InitializeComponent();
            _items = new ObservableCollection<DrillItem>();
            
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            catagory = (String)e.Parameter;
            
            await ctv.combatDrillsTable.Initialization;
            await ctv.combatDrillsTable.InitLocalStoreAsync();
            await AddItemsAsync();
            RefreshListView.ItemsSource = _items;
        }

        private async Task updateDrillsAsync() { 

            await ctv.combatDrillsTable.GetDrillsAsync(catagory);
        }

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

        private async Task ListView_RefreshCommand(object sender, EventArgs e)
        {
            loadTxt.Visibility = Visibility.Visible;
            _items = ctv.combatDrillsTable.GetDrills();
           await AddItemsAsync();
            loadTxt.Visibility = Visibility.Collapsed;
        }

        private void ListView_RefreshIntentCanceled(object sender, EventArgs e)
        {
        }


        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            // Height is only important if we want the Popup sized to the screen 
            ppup.Height = Window.Current.Bounds.Height;
            ppup.IsOpen = true;
        }

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            // Height is only important if we want the Popup sized to the screen 
            updateppup.Height = Window.Current.Bounds.Height;
            updateppup.IsOpen = true;
        }

        public async void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            DrillItem drillItem = new DrillItem();
            String Name = NameBox.Text;
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
            
                await ctv.combatDrillsTable.AddDrill(drillItem, Name, Sets, Time, catagory);
                ppup.IsOpen = false;
                var addSuccess = new MessageDialog("Drill added to database");
                await addSuccess.ShowAsync();
           
        }

        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back
            if (MainPage.MyFrame.CanGoBack)
            {
                MainPage.MyFrame.GoBack();
            }
        }

        private async void DelButton_Click(object sender, RoutedEventArgs e)
        {
            String drillId = (String)((Button)sender).Tag;
            await ctv.combatDrillsTable.DeleteDrillAsync(drillId);
        }

        private async void NewSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            String Name = NewNameBox.Text;
            String id = (String)((Button)sender).Tag;
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

            await ctv.combatDrillsTable.UpdateDrill(id, Name, Sets, Time, catagory);
            ppup.IsOpen = false;
            var addSuccess = new MessageDialog("Drill Updated");
            await addSuccess.ShowAsync();

        }

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
            else
            {
                InfoText.Text = "Error Loading Text";
            }
            
            

            infopup.Height = Window.Current.Bounds.Height;
            infopup.IsOpen = true;
        }
    }
}
