using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using Govision.database;
using Govision.Model;

namespace Govision.AppSettings
{
    public partial class HistorySettings : PhoneApplicationPage
    {
        public HistorySettings()
        {
            InitializeComponent();

            //Get the history list from the data base
            HistoryList HistoryListItems = new HistoryDatabase().GetHistoryList();

            this.HistoryList.DataContext = HistoryListItems;
        }

        private void HistoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Get the history list from the data base
            HistoryList HistoryListItems = new HistoryDatabase().GetHistoryList();

            HistoryData SelectedItem = (HistoryData)HistoryList.SelectedItem;//get listbox item data

            if (SelectedItem != null)
            {
                var itemToRemove = HistoryListItems.Single(r => r.Id == SelectedItem.Id);

                MessageBoxResult mbr = MessageBox.Show("Are you sure you want to delete (" + SelectedItem.Title.ToString() + ") from your history?", "Warning", MessageBoxButton.OKCancel);

                if (mbr == MessageBoxResult.OK)
                {
                    HistoryListItems.Remove(itemToRemove);

                    HistoryDatabase History = new HistoryDatabase();
                    History.UpdateDatabase(HistoryListItems);

                    this.HistoryList.DataContext = HistoryListItems; //Update the list view
                }
            }

            HistoryList.SelectedItem = null;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageFile IsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

            int ListCount = new HistoryDatabase().GetHistoryList().Count;

            if (IsolatedStorage.FileExists("HistoryDB") && ListCount > 0)
            {
                MessageBoxResult mbr = MessageBox.Show("Are you sure you want to delete all your history?", "Warning", MessageBoxButton.OKCancel);

                if (mbr == MessageBoxResult.OK)
                {
                    IsolatedStorage.DeleteFile("HistoryDB");
                }
            } 
            else
            {
                MessageBox.Show("History list is empty.", "Empty", MessageBoxButton.OK);
            }
        }
       
    }
}