using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyToolkit.Multimedia;
using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Navigation;
using Windows.Web.Http;

namespace Govision
{
    public partial class video : PhoneApplicationPage
    {
        public video()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var url = PhoneApplicationService.Current.State["videoURI"];

            System.Uri uri = new Uri(url.ToString());

            this.player.Source = uri;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            //Go back to the main page
            //NavigationService.Navigate(new Uri("/start.xaml", UriKind.Relative));
            //Don't allow to navigate back to the scanner with the back button
            //NavigationService.RemoveBackEntry();
            
        }
    }
}