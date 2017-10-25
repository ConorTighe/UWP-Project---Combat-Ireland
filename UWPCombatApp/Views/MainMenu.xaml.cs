using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainMenu : Page
    {
        public ObservableCollection<AdaptItem> picItems_;

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

        public MainMenu()
        {
            this.InitializeComponent();
            picItems_ = AdaptItem.AdaptList();
            AGVC.ItemsSource = picItems_;
            
        }

        public void AGVC_ItemClick(object sender, Windows.UI.Xaml.Controls.ItemClickEventArgs e)
        {
            String catagory;
            if ((e.ClickedItem as AdaptItem).Title == "b1")
            {
                catagory = "Boxing";
               
                MainPage.MyFrame.Navigate(typeof(DisplayDrills), catagory);
            }
            if ((e.ClickedItem as AdaptItem).Title == "b2")
            {

                catagory = "Karate";
               
                MainPage.MyFrame.Navigate(typeof(DisplayDrills), catagory);
            }
            if ((e.ClickedItem as AdaptItem).Title == "map")
            {
                MainPage.MyFrame.Navigate(typeof(MapMenu));
            }

        }

    }
}
