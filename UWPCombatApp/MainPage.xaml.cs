using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPCombatApp
{
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<AdaptItem> picItems_;

        private MobileServiceCollection<DrillItem, DrillItem> items;
        private IMobileServiceSyncTable<DrillItem> drillTable = App.MobileService.GetSyncTable<DrillItem>();

        private ObservableCollection<AdaptItem> PicItems
        {
            get
            {
                return picItems_;
            }
            set
            {
                picItems_ = value;
            }

        }
        
        public MainPage()
        {
            this.InitializeComponent();
            picItems_ = AdaptItem.AdaptList();
            AGVC.ItemsSource = picItems_;

        }


    }
}
