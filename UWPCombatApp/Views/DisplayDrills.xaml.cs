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
using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;

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
                var addError = new MessageDialog("Connection to your drills could not be established at this time, returning to " +
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
        private async Task updateDrillsAsync()
        {

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

        // Popup window for adding new drills
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ppup.Height = Window.Current.Bounds.Height;
            ppup.IsOpen = true;
        }

        // Add a new drill to database
        public async void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            // Drill variables
            DrillItem drillItem = new DrillItem();
            String Name = (String)NameBox.Text;
            String Use = (String)UseCombo.SelectionBoxItem;
            int Sets;
            int Time = TimeBox.Time.Minutes;
            int hours = TimeBox.Time.Hours;
            int i = 0;

            while (i <= hours)
            {
                Time = Time + 60;
                i += 1;
            }
            bool successfullyParsedSets = int.TryParse(SetsBox.Text, out Sets);

            // Check ints
            if (successfullyParsedSets)
            {
                Sets = Int32.Parse(SetsBox.Text);
            }

            if (Name == null)
            {
                Name = "Empty drill";

            }
            else if (Use == null)
            {
                Use = "Empty equipment";
            }

            try
            {
                // Call ViewModel controller to add to the Model
                await ctv.combatDrillsTable.AddDrill(drillItem, Name, Sets, Time, catagory, Use);
                ToastContent content = new ToastContent()
                {
                    Visual = new ToastVisual()
                    {
                        BindingGeneric = new ToastBindingGeneric()
                        {
                            Children =
                    {
                    new AdaptiveText()
                    {
                        Text = Name + " added to " + catagory,
                        HintStyle = AdaptiveTextStyle.Body
                    },

                    new AdaptiveText()
                    {
                        Text = "Refresh the page to see updates!",
                        HintWrap = true,
                        HintStyle = AdaptiveTextStyle.CaptionSubtle
                    }
                    }
                        }
                    }
                };

                // Show custom Toast
                var notifier = ToastNotificationManager.CreateToastNotifier();
                notifier.Show(new ToastNotification(content.GetXml()));
            }
            catch
            {
                ToastContent content = new ToastContent()
                {
                    Visual = new ToastVisual()
                    {
                        BindingGeneric = new ToastBindingGeneric()
                        {
                            Children =
                    {
                    new AdaptiveText()
                    {
                        Text = Name + " Failed to be added into " + catagory,
                        HintStyle = AdaptiveTextStyle.Body
                    },

                    new AdaptiveText()
                    {
                        Text = "Try checking your connection or trying later!",
                        HintWrap = true,
                        HintStyle = AdaptiveTextStyle.CaptionSubtle
                    }
                    }
                        }
                    }
                };

                // Show custom Toast
                var notifier = ToastNotificationManager.CreateToastNotifier();
                notifier.Show(new ToastNotification(content.GetXml()));

            }
            // Clean up and notify user
            ppup.IsOpen = false;

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

        private void ListView_RefreshCommand(object sender, EventArgs e)
        {
            foreach (var t in _items)
            {
                _items.Insert(0, t);
            }
        }

        // Update item with values entered
        private async void NewSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            // Variables for update
            String Name = (String)NewNameBox.Text;
            String Use = (String)UseCombo.SelectionBoxItem;
            int Sets;
            int Time;
            bool successfullyParsedTime = int.TryParse(NewTimeBox.Time.ToString(), out Time);
            bool successfullyParsedSets = int.TryParse(NewSetsBox.Text, out Sets);

            // Check ints
            if (successfullyParsedSets)
            {
                Sets = Int32.Parse(NewSetsBox.Text);

            }

            if (successfullyParsedTime)
            {
                Time = Int32.Parse(NewTimeBox.Time.ToString());
            }

            if (Name == null)
            {
                Name = "Empty drill";
            }
            else if (Use == null)
            {
                Use = "Empty equipment";
            }

            // Call the db ViewModel controller to update the Model
            try
            {
                await ctv.combatDrillsTable.UpdateDrill(id, Name, Sets, Time, catagory, Use);

                ToastContent content = new ToastContent()
                {
                    Visual = new ToastVisual()
                    {
                        BindingGeneric = new ToastBindingGeneric()
                        {
                            Children =
                    {
                    new AdaptiveText()
                    {
                        Text = Name + " updated in " + catagory,
                        HintStyle = AdaptiveTextStyle.Body
                    },

                    new AdaptiveText()
                    {
                        Text = "Refresh the page to see updates!",
                        HintWrap = true,
                        HintStyle = AdaptiveTextStyle.CaptionSubtle
                    }
                    }
                        }
                    }
                };

                // Show custom Toast
                var notifier = ToastNotificationManager.CreateToastNotifier();
                notifier.Show(new ToastNotification(content.GetXml()));

                // Clean up and notify user
                ppup.IsOpen = false;
            }
            catch
            {
                ToastContent content = new ToastContent()
                {
                    Visual = new ToastVisual()
                    {
                        BindingGeneric = new ToastBindingGeneric()
                        {
                            Children =
                    {
                    new AdaptiveText()
                    {
                        Text = Name + " Failed to be updated in " + catagory,
                        HintStyle = AdaptiveTextStyle.Body
                    },

                    new AdaptiveText()
                    {
                        Text = "Try checking your connection or trying later!",
                        HintWrap = true,
                        HintStyle = AdaptiveTextStyle.CaptionSubtle
                    }
                    }
                        }
                    }
                };

                // Show custom Toast
                var notifier = ToastNotificationManager.CreateToastNotifier();
                notifier.Show(new ToastNotification(content.GetXml()));
            }
        }

        // Display info in provided text files
        private void Info_Click(object sender, RoutedEventArgs e)
        {
            // Set style for DisplayDrill Class
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

            // Open popup
            infopup.Height = Window.Current.Bounds.Height;
            infopup.IsOpen = true;
        }

        // Delete this item
        private async void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            // Use ViewModel controler to remove id from Model
            try
            {
                await ctv.combatDrillsTable.DeleteDrillAsync(id);
                ToastContent content = new ToastContent()
                {
                    Visual = new ToastVisual()
                    {
                        BindingGeneric = new ToastBindingGeneric()
                        {
                            Children =
                    {
                    new AdaptiveText()
                    {
                        Text = "Item Deleted from " + catagory,
                        HintStyle = AdaptiveTextStyle.Body
                    },

                    new AdaptiveText()
                    {
                        Text = "Refresh the list to see changes!",
                        HintWrap = true,
                        HintStyle = AdaptiveTextStyle.CaptionSubtle
                    }
                    }
                        }
                    }
                };

                // Show custom Toast
                var notifier = ToastNotificationManager.CreateToastNotifier();
                notifier.Show(new ToastNotification(content.GetXml()));

            }
            catch
            {
                ToastContent content = new ToastContent()
                {
                    Visual = new ToastVisual()
                    {
                        BindingGeneric = new ToastBindingGeneric()
                        {
                            Children =
                    {
                    new AdaptiveText()
                    {
                        Text = "Could not delete item",
                        HintStyle = AdaptiveTextStyle.Body
                    },

                    new AdaptiveText()
                    {
                        Text = "Try checking your connection or trying later!",
                        HintWrap = true,
                        HintStyle = AdaptiveTextStyle.CaptionSubtle
                    }
                    }
                        }
                    }
                };

                // Show custom Toast
                var notifier = ToastNotificationManager.CreateToastNotifier();
                notifier.Show(new ToastNotification(content.GetXml()));
            }

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

        private void Ref_Click(object sender, RoutedEventArgs e)
        {
            MainPage.MyFrame.Navigate(typeof(DisplayDrills), catagory);
        }
    }
}
