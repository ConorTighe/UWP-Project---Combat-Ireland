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
                Image = "ms-appx:///Assets/n1.png",
                Link = "https://www.fighter.ie/categories"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/n2.png",
                Link = "https://www.mmafighting.com/"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/n3.png",
                Link = "http://mmajunkie.com/"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/n4.png",
                Link = "https://www.mmamania.com/latest-news"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/n5.png",
                Link = "http://www.the42.ie/combat-sports/news/"
            },
            new NewsItem
            {
                Image = "ms-appx:///Assets/n6.png",
                Link = "https://www.reddit.com/r/MMA/"
            }
        };

            return pics;
        }
    }
}
