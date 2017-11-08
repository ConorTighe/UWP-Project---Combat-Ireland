using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
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

namespace UWPCombatApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapMenu : Page
    {
        private ObservableCollection<GymsItem> gymItems_;

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

        internal ObservableCollection<GymsItem> GymItems_ { get => gymItems_; set => gymItems_ = value; }

        public MapMenu()
        {
            this.InitializeComponent();
            GymItems_ = GymsItem.GymList();
            CarouselControl.ItemsSource = GymItems_;
        }

        private void GymLocate(double Lat, double Long)
        {
            BasicGeoposition GymPos = new BasicGeoposition() { Latitude = Lat, Longitude = Long };
            Geopoint GymPosition = new Geopoint(GymPos);
            MainPage.MyFrame.Navigate(typeof(GymLocator), GymPosition);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        try { 
            double lat;
            double lng;
            if (String.ReferenceEquals(((Button)sender).Tag, "b1"))
            {
                lat = 53.2931;
                lng = -9.0133;
                GymLocate(lat, lng);
            }
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
        }
       }
    }
}
