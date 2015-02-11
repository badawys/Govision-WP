﻿using Microsoft.Phone.Controls;
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

            //YouTube.CancelPlay(); // used to reenable page

            try
            {
                if (NetworkInterface.GetIsNetworkAvailable())
                {
                    System.Uri uri = new Uri("http://api.govision.co/v1/index.php?id=" + t);

                    HttpClient httpClient = new HttpClient();
                    var response = await httpClient.GetAsync(uri);
                    string res = await response.Content.ReadAsStringAsync();

                    string videoId = res;

                    //Get The Video Uri and set it as a player source 
                    var url = await YouTube.GetVideoUriAsync(videoId, YouTubeQuality.Quality360P);

                    PhoneApplicationService.Current.State["videoURI"] = url.Uri;

                    NavigationService.Navigate(new Uri("/video.xaml", UriKind.Relative));

                }
                else
                {
                    MessageBox.Show("You're not connected to Internet!");
                    //Go back to the main page
                    NavigationService.Navigate(new Uri("/start.xaml", UriKind.Relative));
                    //Don't allow to navigate back to the scanner with the back button
                    NavigationService.RemoveBackEntry();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (YouTube.CancelPlay()) // used to abort current youtube download
                e.Cancel = true;
            else
            {
                // your code here
            }
            base.OnBackKeyPress(e);

            //Go back to the main page
            NavigationService.Navigate(new Uri("/start.xaml", UriKind.Relative));
            //Don't allow to navigate back to the scanner with the back button
            NavigationService.RemoveBackEntry();

        }

    }
}