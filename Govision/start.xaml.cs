using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ZXing.Mobile;
using ZXing;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Windows.Media;

namespace Govision
{
    public partial class start : PhoneApplicationPage
    {

        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;

        public static MobileBarcodeScanningOptions ScanningOptions = new MobileBarcodeScanningOptions();
        public static MobileBarcodeScannerBase Scanner;
        public static Result LastScanResult;
        public static Action<Result> FinishedAction;

        public start()
        {
            InitializeComponent();

            //when app resume event
            PhoneApplicationService.Current.Activated += (s, e) =>
            {
                scanner();
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (settings.Contains("ThemeColor"))
            {
                ApplicationBar.BackgroundColor = ConvertStringToColor(settings["ThemeColor"].ToString());
                SystemTray.BackgroundColor = ConvertStringToColor(settings["ThemeColor"].ToString());
            }

            scanner();
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            try
            {
                scannerControl.StopScanning();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.HResult, "Error", MessageBoxButton.OK); }

            base.OnNavigatingFrom(e);
        }

        private void scanner()
        { 
            try
            {
                //set scanner options (Set PossibleFormats to QR_CODE)
                ScanningOptions.PossibleFormats = new List<ZXing.BarcodeFormat>() { 
                    ZXing.BarcodeFormat.QR_CODE
                    //ZXing.BarcodeFormat.AZTEC,
                    //ZXing.BarcodeFormat.DATA_MATRIX,
                };

                ScanningOptions.AutoRotate = true;
                //ScanningOptions.TryHarder = true;
                scannerControl.ScanningOptions = ScanningOptions;

                //start the scanner
                scannerControl.StartScanning(HandleResult, ScanningOptions);

                //if device doesn't have a flash, disable flash button
                if (scannerControl.CanTorch == false)
                {
                    ApplicationBar.Buttons.RemoveAt(0);

                    ApplicationBarIconButton flashButton = new ApplicationBarIconButton();
                    flashButton.Text = "Flash";
                    flashButton.IconUri = new Uri("/Assets/icons/appbar.camera.flash.png", UriKind.Relative);
                    flashButton.Click += _flashButton;
                    flashButton.IsEnabled = false;
                    ApplicationBar.Buttons.Add(flashButton);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message + "\n" + ex.HResult, "Error", MessageBoxButton.OK); }
        }

        void HandleResult(ZXing.Result result)
        {
            if (settings["DebugMode"].ToString() == "true")
            {
                MessageBox.Show(result.Text, "GV Tag Founded", MessageBoxButton.OK);
            }
            
            NavigationService.Navigate(new Uri("/DataBeacon.xaml?t=" + result.Text, UriKind.Relative));
        }

        private void _flashButton(object sender, EventArgs e)
        {
            scannerControl.ToggleTorch();
            scannerControl.AutoFocus();
            scannerControl.ToggleTorch();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            Application.Current.Terminate();
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/History.xaml", UriKind.Relative));
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Settings.xaml", UriKind.Relative));
        }

        public Color ConvertStringToColor(String hex)
        {

            hex = hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            int start = 0;

            //handle ARGB strings (8 characters long) 
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            //convert RGB characters to bytes 
            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return Color.FromArgb(a, r, g, b);
        }
    }
}