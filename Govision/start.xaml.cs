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

namespace Govision
{
    public partial class start : PhoneApplicationPage
    {

        public static MobileBarcodeScanningOptions ScanningOptions = new MobileBarcodeScanningOptions();
        public static MobileBarcodeScannerBase Scanner;
        public static Result LastScanResult;
        public static Action<Result> FinishedAction;

        public start()
        {
            InitializeComponent();

            PhoneApplicationService.Current.Activated += (s, e) =>
            {
                scanner();
            };
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            scanner();
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            try
            {
                scannerControl.StopScanning();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

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
            catch (Exception ex) { MessageBox.Show("Error" +"\n" + ex.Message + "\n" + ex.HResult); }
        }

        void HandleResult(ZXing.Result result)
        {
            MessageBox.Show(result.Text);
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
    }
}