using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
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


namespace UWPCombatApp
{

    public sealed partial class MapMenu : Page
    {
        // List to store the gym items in
        private ObservableCollection<GymsItem> gymItems_;

        // gym items Get and Set
        private ObservableCollection<GymsItem> gymItems
        {
            get
            {
                return GymItems_;
            }
            set
            {
                GymItems_ = value;
            }

        }

        // internal Get and Set
        internal ObservableCollection<GymsItem> GymItems_ { get => gymItems_; set => gymItems_ = value; }

        // Construct the page and add the gym items to the casourol menu
        public MapMenu()
        {
            this.InitializeComponent();
            GymItems_ = GymsItem.GymList();
            CarouselControl.ItemsSource = GymItems_;
        }

        /* Locate the gym by passing the geopoint class the lat and long and then create a geopoint that we will use
         * when we navigate to the map */
        private void GymLocate(double Lat, double Long)
        {
            BasicGeoposition GymPos = new BasicGeoposition() { Latitude = Lat, Longitude = Long };
            Geopoint GymPosition = new Geopoint(GymPos);
            MainPage.MyFrame.Navigate(typeof(GymLocator), GymPosition);
        }

        // When the user selects a gym we will assign the lat and long of that gym and pass it to out map GymLocate method
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        try { 
            double lat;
            double lng;
            if (String.ReferenceEquals(((Button)sender).Tag, "PointBlank"))
            {
                    lat = 53.2931;
                    lng = -9.0133;
                GymLocate(lat, lng);
            }
            else if(String.ReferenceEquals(((Button)sender).Tag, "ShaolinMMA"))
            {
                    lat = 53.2864;
                    lng = -9.0384;
                    GymLocate(lat, lng);
            }
            else if (String.ReferenceEquals(((Button)sender).Tag, "KarateGalway"))
            {
                    lat = 53.2759;
                    lng = -9.0711;
                    GymLocate(lat, lng);
            }
            else if (String.ReferenceEquals(((Button)sender).Tag, "GalwayBoxingGym"))
            {
                    lat = 53.2779;
                    lng = -9.0774;
                    GymLocate(lat, lng);
            }
            else if (String.ReferenceEquals(((Button)sender).Tag, "Takwondo"))
            {
                    lat = 53.2758;
                    lng = -9.0163;
                    GymLocate(lat, lng);
            }
            else if (String.ReferenceEquals(((Button)sender).Tag, "galwayjudo"))
            {
                    lat = 53.2691;
                    lng = -9.0561;
                    GymLocate(lat, lng);
            }
            else if (String.ReferenceEquals(((Button)sender).Tag, "blackdragon"))
            {
                    lat = 53.2825;
                    lng = -9.0431;
                    GymLocate(lat, lng);
            }
            else if (String.ReferenceEquals(((Button)sender).Tag, "BJJLK"))
            {
                    lat = 54.9425;
                    lng = -7.7400;
                    GymLocate(lat, lng);
            }
            else if (String.ReferenceEquals(((Button)sender).Tag, "SGB"))
            {
                    lat = 53.3274;
                    lng = -6.3381;
                    GymLocate(lat, lng);
            }
            }
            catch(Exception ex)
            {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
            }
        }

        // Go back to main
        private void back_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back
            if (MainPage.MyFrame.CanGoBack)
            {
                MainPage.MyFrame.GoBack();
            }
        }

        private async void MapInfo_Click(object sender, RoutedEventArgs e)
        {
            var mapInfo = new MessageDialog("Double click: Zoom \nClick and drag: Move map view around \n Touchpad:\n " +
                "Pinch and open: Zoom out \nPinch and close: Zoom in");
            await mapInfo.ShowAsync();
        }
    }
}
