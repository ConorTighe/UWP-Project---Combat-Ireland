using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    
    public sealed partial class MainMenu : Page
    {
        // list for storing the items
        public ObservableCollection<AdaptItem> picItems_;

        // Get and Set list
        private ObservableCollection<AdaptItem> PicItems
        {
            get
            {
                return picItems_;
            }
            set
            {
                picItems_ = value;
            }

        }

        // Main Menu Contructor where I bind the data items to the adaptive list
        public MainMenu()
        {
            this.InitializeComponent();
            picItems_ = AdaptItem.AdaptList();
            AGVC.ItemsSource = picItems_;

        }

        // Handles when a user clicks on a item in the adaptive list to bring them to there chosen page
        public void AGVC_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            String catagory;
            if ((e.ClickedItem as AdaptItem).Title == "b1")
            {
                catagory = "Boxing";

                MainPage.MyFrame.Navigate(typeof(DisplayDrills), catagory);
            }
            else if ((e.ClickedItem as AdaptItem).Title == "b2")
            {

                catagory = "Karate";

                MainPage.MyFrame.Navigate(typeof(DisplayDrills), catagory);
            }
            else if ((e.ClickedItem as AdaptItem).Title == "b3")
            {

                catagory = "TKD";

                MainPage.MyFrame.Navigate(typeof(DisplayDrills), catagory);
            }
            else if ((e.ClickedItem as AdaptItem).Title == "b4")
            {

                catagory = "Kickboxing";

                MainPage.MyFrame.Navigate(typeof(DisplayDrills), catagory);
            }
            else if ((e.ClickedItem as AdaptItem).Title == "b5")
            {

                catagory = "MuayThai";

                MainPage.MyFrame.Navigate(typeof(DisplayDrills), catagory);
            }
            else if ((e.ClickedItem as AdaptItem).Title == "b6")
            {

                catagory = "BJJ";

                MainPage.MyFrame.Navigate(typeof(DisplayDrills), catagory);
            }
            else if ((e.ClickedItem as AdaptItem).Title == "b7")
            {

                catagory = "Judo";

                MainPage.MyFrame.Navigate(typeof(DisplayDrills), catagory);
            }
            else if ((e.ClickedItem as AdaptItem).Title == "b8")
            {

                catagory = "MMA";

                MainPage.MyFrame.Navigate(typeof(DisplayDrills), catagory);
            }
            else if ((e.ClickedItem as AdaptItem).Title == "map")
            {
                try
                {
                    MainPage.MyFrame.Navigate(typeof(MapMenu));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                }
            }
            else if ((e.ClickedItem as AdaptItem).Title == "news")
            {
                try
                {
                    MainPage.MyFrame.Navigate(typeof(NewsMenu));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                }
            }

        }

        private async void MoreInfo_Click(object sender, RoutedEventArgs e)
        {
            string link = "https://github.com/ConorTighe1995/UWP-Project---Combat-Ireland";
            var uri = new Uri(link);

            var success = await Windows.System.Launcher.LaunchUriAsync(uri);

            if (success)
            {
                var linkSuccess = new MessageDialog("About page opened in new browser window");
                await linkSuccess.ShowAsync();
            }
        }
    }
}
