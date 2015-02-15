using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using ZXing;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;

namespace Govision
{
    public partial class DemoMode : PhoneApplicationPage
    {

        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        public DemoMode()
        {
            InitializeComponent();
        }

        private void _StartButton(object sender, EventArgs e)
        {
            // create a tag reader instance
            IBarcodeReader reader = new BarcodeReader();

            reader.Options.PossibleFormats = new List<ZXing.BarcodeFormat>() { 
                    ZXing.BarcodeFormat.QR_CODE
                };
            reader.Options.TryHarder = true;

            // load a bitmap
            var barcodeBitmap = new WriteableBitmap(tagImage.Source as BitmapSource);

            var result = reader.Decode(barcodeBitmap);
            // do something with the result
            if (result != null)
            {
                HandleResult(result);
            }
        }

        void HandleResult(ZXing.Result result)
        {
            if (settings.Contains("DebugMode") && settings["DebugMode"].ToString() == "true")
            {
                MessageBox.Show(result.Text, "GV Tag Founded", MessageBoxButton.OK);
            }

            NavigationService.Navigate(new Uri("/DataBeacon.xaml?t=" + result.Text, UriKind.Relative));
        }

        private void Tag1_Click(object sender, EventArgs e)
        {
            tagImage.Source = new BitmapImage(new Uri("Assets/tags/1.png", UriKind.Relative));
        }

        private void Tag2_Click(object sender, EventArgs e)
        {
            tagImage.Source = new BitmapImage(new Uri("Assets/tags/2.png", UriKind.Relative));
        }

        private void Tag3_Click(object sender, EventArgs e)
        {
            tagImage.Source = new BitmapImage(new Uri("Assets/tags/3.png", UriKind.Relative));
        }

        private void Tag4_Click(object sender, EventArgs e)
        {
            tagImage.Source = new BitmapImage(new Uri("Assets/tags/4.png", UriKind.Relative));
        }

        private void Tag5_Click(object sender, EventArgs e)
        {
            tagImage.Source = new BitmapImage(new Uri("Assets/tags/5.png", UriKind.Relative));
        }
    }
}