using Govision.database;
using Govision.Model;
using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Govision
{
    public partial class History : PhoneApplicationPage
    {
        public History()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Get the history list from the data base
            HistoryList HistoryListItems = new HistoryDatabase().GetHistoryList();

            this.HistoryList.DataContext = HistoryListItems;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {           
            //Go back to the main page
            NavigationService.Navigate(new Uri("/start.xaml", UriKind.Relative));
            //Don't allow to navigate back to the scanner with the back button
            NavigationService.RemoveBackEntry();

        }

        private void HistoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HistoryData SelectedItem = (HistoryData)HistoryList.SelectedItem;//get listbox item data 

            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

            if (settings.Contains("DebugMode") && settings["DebugMode"].ToString() == "true")
            {
                MessageBox.Show("Your selected Tag's id is:" + SelectedItem.Tag_id.ToString());
            }

            NavigationService.Navigate(new Uri("/DataBeacon.xaml?t=" + SelectedItem.Tag_id.ToString(), UriKind.Relative));
        }

    }
   
}