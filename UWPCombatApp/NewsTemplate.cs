using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPCombatApp
{
    class NewsItem
    {
        public String Image
        {
            get;
            set;
        }
        public String Link
        {
            get;
            set;
        }

        public static ObservableCollection<NewsItem> NewsList()
        {
            ObservableCollection<NewsItem> pics = new ObservableCollection<NewsItem>()
        {
            new NewsItem
            {
                Image = "ms-appx:///Assets/b1.png",
                Link = "Reddit"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/b2.png",
                Link = "MMAfighting"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/b3.png",
                Link = "MMAjunkie"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/b4.png",
                Link = "b4"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/b5.png",
                Link = "b5"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/b6.png",
                Link = "b6"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/b7.png",
                Link = "b7"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/b8.png",
                Link = "b8"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/gyms.png",
                Link = "map"
            }
        };

            return pics;
        }
    }
}
