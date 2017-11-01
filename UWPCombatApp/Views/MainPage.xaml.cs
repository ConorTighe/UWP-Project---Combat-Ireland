using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Model;
using Windows.UI.Popups;
using UWPCombatApp.Views;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPCombatApp
{
    public sealed partial class MainPage : Page
    {
       
        
        CombatDrillsTable drillTableCntr = new CombatDrillsTable();
        public static Frame MyFrame;


        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainMenu_Loaded;
            MyFrame = myFrame;
        }

        private void MainMenu_Loaded(object sender, RoutedEventArgs e)
        {
            this.myFrame.Navigate(typeof(MainMenu));
        }

    }
}
