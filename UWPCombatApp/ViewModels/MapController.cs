using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace UWPCombatApp
{
    public class MapController
    {
        // This thread gets out users location
        public async Task<Geopoint> Position()
        {
            return (await new Geolocator().GetGeopositionAsync()).Coordinate.Point;
        }

        // This function will produce a marker which we will use to place show the gyms location on the map
        public UIElement Marker()
        {
            Canvas marker = new Canvas();
            Ellipse outer = new Ellipse();
            outer.Fill = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            outer.Margin = new Thickness(-12.5, -12.5, 0, 0);
            Ellipse inner = new Ellipse() { Width = 50, Height = 50 };
            inner.Margin = new Thickness(-10, -10, 0, 0);
            Ellipse core = new Ellipse { Width = 10, Height = 10 };
            core.Fill = new SolidColorBrush(Colors.Red);
            core.Margin = new Thickness(-10, -10, 0, 0);
            TextBlock you = new TextBlock();
            you.Text = "You'r Here";
            you.Foreground = new SolidColorBrush(Colors.Blue);
            you.Margin = new Thickness(-20, -30, 0, 0); ;
            marker.Children.Add(outer);
            marker.Children.Add(inner);
            marker.Children.Add(core);
            marker.Children.Add(you);
            return marker;
        }

        //This function will provide the gym name beside the marker
        public UIElement MarkerText(String Text)
        {
            Canvas marker = new Canvas();
            TextBlock text = new TextBlock();
            text.Foreground = new SolidColorBrush(Colors.Red);
            marker.Background = new SolidColorBrush(Colors.Black);
            text.FontSize = 16;
            text.Text = Text;
            marker.Children.Add(text);
            return marker;
        }
    }
}
