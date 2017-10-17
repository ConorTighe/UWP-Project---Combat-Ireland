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

        public static ObservableCollection<AdaptItem> AdaptList()
        {
            ObservableCollection<AdaptItem> pics = new ObservableCollection<AdaptItem>()
        {
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b1.png"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b2.png"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b3.png"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b4.png"
            },
            new AdaptItem
            {
                Image = "ms-appx:///Assets/b5.png"
            }
        };

            return pics;
        }
    }
}
