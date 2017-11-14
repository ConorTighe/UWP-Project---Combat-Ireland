using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCombatApp
{
    // This item fills the gym menu
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

        public static ObservableCollection<GymsItem> GymList()
        {
            ObservableCollection<GymsItem> gyms = new ObservableCollection<GymsItem>()
            {
            new GymsItem
            {
                Image = "ms-appx:///Assets/pointblack.jpg",
                Title = "PointBlank"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/shamma.jpg",
                Title = "ShaolinMMA"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/karate.jpg",
                Title = "KarateGalway"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/gboxing.jpg",
                Title = "GalwayBoxingGym"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/tkdg.jpg",
                Title = "Takwondo"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/galwayjudo.png",
                Title = "galwayjudo"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/bdk.jpg",
                Title = "blackdragon"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/rgj.jpg",
                Title = "BJJLK"
            },
            new GymsItem
            {
                Image = "ms-appx:///Assets/sgb.png",
                Title = "SGB"
            }
            };

            return gyms;
        }
    }
}
