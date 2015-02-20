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

            AlbumPhotosListItems.Add(new AlbumData { src= "Assets/tags/1.png" });
            AlbumPhotosListItems.Add(new AlbumData { src = "Assets/tags/2.png" });
            AlbumPhotosListItems.Add(new AlbumData { src = "Assets/tags/3.png" });
            AlbumPhotosListItems.Add(new AlbumData { src = "Assets/tags/4.png" });
            AlbumPhotosListItems.Add(new AlbumData { src = "Assets/tags/5.png" });

            this.albumView.DataContext = AlbumPhotosListItems;
        }

    }
}