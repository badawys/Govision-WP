using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Govision.Model;
using System.ComponentModel;

namespace Govision
{
    public partial class History : PhoneApplicationPage
    {
        public History()
        {
            InitializeComponent();



            List<HistoryData> HistoryListItems = new List<HistoryData>();


            HistoryListItems.Add(new HistoryData() { Title = "Ut enim ad minim veniam", Image = "Assets/Video.png", Tag_Type = "Video" });
            HistoryListItems.Add(new HistoryData() { Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua", Image = "Assets/Text.png", Tag_Type = "Text" });
            HistoryListItems.Add(new HistoryData() { Title = "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat", Image = "Assets/Text.png", Tag_Type = "Text" });
            HistoryListItems.Add(new HistoryData() { Title = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur", Image = "Assets/Video.png", Tag_Type = "Video" });
            HistoryListItems.Add(new HistoryData() { Title = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum", Image = "Assets/Video.png", Tag_Type = "Video" });
            HistoryListItems.Add(new HistoryData() { Title = "totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo", Image = "Assets/Gallery.png", Tag_Type = "Galery" });
            HistoryListItems.Add(new HistoryData() { Title = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium", Image = "Assets/Video.png", Tag_Type = "Video" });
            HistoryListItems.Add(new HistoryData() { Title = "Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt", Image = "Assets/Gallery.png", Tag_Type = "Gallery" });
            HistoryListItems.Add(new HistoryData() { Title = "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur", Image = "Assets/Video.png", Tag_Type = "Video" });
            HistoryListItems.Add(new HistoryData() { Title = "adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem", Image = "Assets/Text.png", Tag_Type = "Text" });
            this.HistoryList.DataContext = HistoryListItems;
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
           
            //Go back to the main page
            NavigationService.Navigate(new Uri("/start.xaml", UriKind.Relative));
            //Don't allow to navigate back to the scanner with the back button
            NavigationService.RemoveBackEntry();

        }

    }
   
}