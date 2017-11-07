using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCombatApp
{
    public class AdaptItem
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

        public static ObservableCollection<AdaptItem> AdaptList()
        {
            ObservableCollection<AdaptItem> pics = new ObservableCollection<AdaptItem>()
        {
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b1.png",
                Title = "b1"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b2.png",
                Title = "b2"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b3.png",
                Title = "b3"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b4.png",
                Title = "b4"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b5.png",
                Title = "b5"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b6.png",
                Title = "b6"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b7.png",
                Title = "b7"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b8.png",
                Title = "b8"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/gyms.png",
                Title = "map"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/news.png",
                Title = "new"
            }
        };

            return pics;
        }
    }
}
