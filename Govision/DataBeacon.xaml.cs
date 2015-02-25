using Govision.database;
using Govision.Model;
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
    public partial class DataBeacon : PhoneApplicationPage
    {
        public DataBeacon()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            var t = NavigationContext.QueryString["t"];

            if (t == "123456789123456") //check for Album preview tag first
            {
                HistoryDatabase history = new HistoryDatabase();
                HistoryList HistoryListItems = new HistoryDatabase().GetHistoryList();

                int lastId = 0; //Set the list id to 0

                if (HistoryListItems.Count > 0)
                    lastId = HistoryListItems[0].Id; //Last item id is the id of the first item of the list

                history.AddItem(new HistoryData() { Id = lastId + 1, Title = "Despicable Me", Image = "Assets/Gallery.png", Tag_Type = "Gallery", Tag_id = NavigationContext.QueryString["t"] });

                //Go back to the main page
                NavigationService.Navigate(new Uri("/album.xaml", UriKind.Relative));
                //Don't allow to navigate back to the scanner with the back button
                NavigationService.RemoveBackEntry();
            }
            else
            {
                string apiVersion = "0"; //TODO: API Version check (API Version Changes)
                string videoId = null;

                YouTube.CancelPlay(); // used to re-enable page

                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    System.Uri uri = new Uri("http://api.govision.co/v1/index.php?id=" + t);

                    try
                    {
                        HttpClient httpClient = new HttpClient();
                        var response = await httpClient.GetAsync(uri);
                        string res = await response.Content.ReadAsStringAsync();

                        videoId = res; //TODO: Change to Result variable (API Version Changes)
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Couldn't connect to Govision servers\nTry again later\n\nERROR:" + ex.Message);

                        //Go back to the main page
                        NavigationService.Navigate(new Uri("/start.xaml", UriKind.Relative));
                        //Don't allow to navigate back to the scanner with the back button
                        NavigationService.RemoveBackEntry();
                    }

                    if (apiVersion == "0") //TODO: API Version check (API Version Changes)
                    {
                        if (videoId != null)
                        {
                            YouTubeVideo(videoId);
                        }
                    }

                }
                else
                {
                    MessageBox.Show("You're not connected to Internet!\nPlease check your Internet connection and tray again.");

                    //Go back to the main page
                    NavigationService.Navigate(new Uri("/start.xaml", UriKind.Relative));
                    //Don't allow to navigate back to the scanner with the back button
                    NavigationService.RemoveBackEntry();
                }
            }        
        }

        private async void YouTubeVideo(string videoId)
        {
            try
            {
                //Get The Video Uri and set it as a player source 
                var url = await YouTube.GetVideoUriAsync(videoId, YouTubeQuality.Quality360P);
                string VideoTitle = await YouTube.GetVideoTitleAsync(videoId);

                HistoryDatabase history = new HistoryDatabase();
                HistoryList HistoryListItems = new HistoryDatabase().GetHistoryList();

                int lastId = 0; //Set the list id to 0

                if (HistoryListItems.Count > 0)
                    lastId = HistoryListItems[0].Id; //Last item id is the id of the first item of the list
               

                history.AddItem(new HistoryData() { Id = lastId + 1 ,Title = VideoTitle, Image = "Assets/Video.png", Tag_Type = "Video", Tag_id = NavigationContext.QueryString["t"] });

                PhoneApplicationService.Current.State["videoURI"] = url.Uri;

                NavigationService.Navigate(new Uri("/video.xaml", UriKind.Relative));
                //Don't allow to navigate back to the data beacon page with the back button
                NavigationService.RemoveBackEntry();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't get YouTube video\n\nERROR:" + ex.Message);

                //Go back to the main page
                NavigationService.Navigate(new Uri("/start.xaml", UriKind.Relative));
                //Don't allow to navigate back to the scanner with the back button
                NavigationService.RemoveBackEntry();
            } 
        }

        //TODO: Add GovisionVideo support 
        private async void GovisionVideo(string path)
        {

        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (YouTube.CancelPlay()) // used to abort current you tube download
                e.Cancel = true;
            
            //Go back to the main page
            //NavigationService.Navigate(new Uri("/start.xaml", UriKind.Relative));
            //Don't allow to navigate back to the scanner with the back button
            //NavigationService.RemoveBackEntry();

            NavigationService.GoBack();
            
            base.OnBackKeyPress(e);
        }

    }
}