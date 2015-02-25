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
    public partial class album : PhoneApplicationPage
    {
        public album()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AlbumPhotosList AlbumPhotosListItems = new AlbumPhotosList();

            AlbumPhotosListItems.Add(new AlbumData { id = "1", src= "Assets/Album/1.jpg", total = "9" });
            AlbumPhotosListItems.Add(new AlbumData { id = "2", src = "Assets/Album/2.jpg", total = "9" });
            AlbumPhotosListItems.Add(new AlbumData { id = "3", src = "Assets/Album/3.jpg", total = "9" });
            AlbumPhotosListItems.Add(new AlbumData { id = "4", src = "Assets/Album/4.jpg", total = "9" });
            AlbumPhotosListItems.Add(new AlbumData { id = "5", src = "Assets/Album/5.jpg", total = "9" });
            AlbumPhotosListItems.Add(new AlbumData { id = "6", src = "Assets/Album/6.jpg", total = "9" });
            AlbumPhotosListItems.Add(new AlbumData { id = "7", src = "Assets/Album/7.jpg", total = "9" });
            AlbumPhotosListItems.Add(new AlbumData { id = "8", src = "Assets/Album/8.jpg", total = "9" });
            AlbumPhotosListItems.Add(new AlbumData { id = "9", src = "Assets/Album/9.jpg", total = "9" });

            this.albumView.DataContext = AlbumPhotosListItems;
        }

    }
}