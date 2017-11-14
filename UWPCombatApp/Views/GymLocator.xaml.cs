using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Controls.Maps;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Popups;
using Windows.Storage.Streams;

namespace UWPCombatApp
{

    public sealed partial class GymLocator : Page
    {
        //Used to interact with the applications map
        public MapController mapControls = new MapController();
        public int indexPosition = 1;

        // Construct and init this page
        public GymLocator()
        {
            this.InitializeComponent();
        }

        // Add a icon tot he map to show the user where the gym is
        private void addIconToLocation(Geopoint location)
        {

            //Create Icon and Add text to map
            MapIcon mapIcon = new MapIcon();
            mapIcon.Location = location;
            mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/gymicon.png"));
            myMap.MapElements.Add(mapIcon);
        }

        /* Create a yellow line along the roads on bing maps to guide the user to there
         * selected location, handle any error if route cant be found on map */
        private async void ShowRouteOnMap(Geopoint startPoint, Geopoint endPoint)
        {
            //Creates the coloured trail to guide the user using there GPS
            MapRouteFinderResult routeResult = await MapRouteFinder.GetDrivingRouteAsync(
                startPoint, endPoint,
                MapRouteOptimization.Time,
                MapRouteRestrictions.None);

            // Do the following if maps route is found
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                myMap.Routes.Clear();
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                myMap.Routes.Add(viewOfRoute);
                await myMap.TrySetViewBoundsAsync(routeResult.Route.BoundingBox, null, MapAnimationKind.None);

            }
            else
            {
                // Handle the error of no map route
                var message = new MessageDialog("Can't find routes");
                await message.ShowAsync();
            }
        }

        /* On navigation create a new geopoint object with the values passed from the previous page. Add this new geopoint to the map
         * and get the location of the userss device. zoom in to there location ans then create the route, ajust screen so the map 
         * displays the user, the route and the gym */
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            Geopoint Destination = (Geopoint)e.Parameter;
            addIconToLocation(Destination);
            Geopoint myPoint = await mapControls.Position();
            myMap.ZoomLevel = 16;
            myMap.Center = myPoint;
            indexPosition++;
            ShowRouteOnMap(myPoint, Destination);
            Geopoint position = await mapControls.Position();
            DependencyObject marker = mapControls.Marker();
            myMap.Children.Add(marker);
            MapControl.SetLocation(marker, position);
            MapControl.SetNormalizedAnchorPoint(marker, new Point(0.5, 0.5));
            myMap.ZoomLevel = 12;
            myMap.Center = position;

        }

        // Go back a page
        private void back_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back
            if (MainPage.MyFrame.CanGoBack)
            {
                MainPage.MyFrame.GoBack();
            }
        }
    }
}
