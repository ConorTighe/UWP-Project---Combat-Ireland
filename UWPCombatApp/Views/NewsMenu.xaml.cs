using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class NewsMenu : Page
    {
        private ObservableCollection<NewsItem> newsItems_;

        private ObservableCollection<NewsItem> newsItems
        {
            get
            {
                return NewsItems_;
            }
            set
            {
                NewsItems_ = value;
            }

        }

        internal ObservableCollection<NewsItem> NewsItems_ { get => newsItems_; set => newsItems_ = value; }

        public NewsMenu()
        {
            this.InitializeComponent();
            NewsItems_ = NewsItem.NewsList();
            NewsCarouselControl.ItemsSource = NewsItems_;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            String link = (String)((Button)sender).Tag;
            if (String.ReferenceEquals(link, null))
            {
                link = "https://www.reddit.com/r/MMA/";
                var uri = new Uri(link);

                var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                if (success)
                {
                    var linkSuccess = new MessageDialog("News source opened in new browser window");
                    await linkSuccess.ShowAsync();
                }
                else
                {
                    var linkFail = new MessageDialog("Couldnt open link");
                    await linkFail.ShowAsync();
                }
            }
            else
            {
                var uri = new Uri(link);

                var success = await Windows.System.Launcher.LaunchUriAsync(uri);

                if (success)
                {
                    var linkSuccess = new MessageDialog("News source opened in new browser window");
                    await linkSuccess.ShowAsync();
                }
                else
                {
                    var linkFail = new MessageDialog("Couldnt open link");
                    await linkFail.ShowAsync();
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back
            if (MainPage.MyFrame.CanGoBack)
            {
                MainPage.MyFrame.GoBack();
            }
        }
    }
}
