using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWPCombatApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPCombatApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DisplayDrills : Page
    {
        
        String parameters;
        CombatTableView ctv = new CombatTableView();

        public DisplayDrills()
        {
            this.InitializeComponent();
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            parameters = (String)e.Parameter;

            testBox.Text = parameters;
            
            
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            // Height is only important if we want the Popup sized to the screen 
            ppup.Height = Window.Current.Bounds.Height;
            ppup.IsOpen = true;
        }

        private async void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            DrillItem drillItem = new DrillItem();
            String Name = NameBox.Text;
            int Sets;
            int Time;
            bool successfullyParsedTime = int.TryParse(SetsBox.Text, out Time);
            bool successfullyParsedSets = int.TryParse(TimeBox.Text, out Sets);

            if (successfullyParsedSets)
            {
                Sets = Int32.Parse(SetsBox.Text);
                
            }
            if (successfullyParsedTime)
            {
                Time = Int32.Parse(TimeBox.Text);
            }
            
                await ctv.combatDrillsTable.AddDrill(drillItem, Name, Sets, Time, parameters);
                ppup.IsOpen = false;
                var addSuccess = new MessageDialog("Drill added to database");
                await addSuccess.ShowAsync();
           
        }
    }
}
