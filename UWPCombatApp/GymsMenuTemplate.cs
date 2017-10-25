using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCombatApp
{
    class GymsItem
    {
        public String Image
        {
            get;
            set;
        }
        public String Title
        {
            get;
            set;
        }
        public String Lat
        {
            get;
            set;
        }
        public String Long
        {
            get;
            set;
        }

        public static ObservableCollection<GymsItem> GymList()
        {
            ObservableCollection<GymsItem> gyms = new ObservableCollection<GymsItem>()
            {
            new GymsItem
            {
                Image = "ms-appx:///Assets/b1.png",
                Title = "b1",
                Lat = "testlat",
                Long = "testlong"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/b2.png",
                Title = "b2",
                Lat = "testlat",
                Long = "testlong"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/b3.png",
                Title = "b3",
                Lat = "testlat",
                Long = "testlong"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/b4.png",
                Title = "b4",
                Lat = "testlat",
                Long = "testlong"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/b5.png",
                Title = "b5",
                Lat = "testlat",
                Long = "testlong"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/bing.png",
                Title = "map",
                Lat = "testlat",
                Long = "testlong"
            }
            };

            return gyms;
        }
    }
}
