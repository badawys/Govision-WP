using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Windows.Media;

namespace ZXing.Mobile
{
    public partial class ZXingScannerControl : UserControl, IDisposable
    {
        public ZXingScannerControl() : base()
        {
            InitializeComponent();
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

        public void StartScanning(Action<ZXing.Result> scanCallback, MobileBarcodeScanningOptions options = null)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;

            if (settings.Contains("ThemeColor"))
            {
                header.Fill = new SolidColorBrush(ConvertStringToColor(settings["ThemeColor"].ToString()));
            }


            ScanCallback = scanCallback;
            ScanningOptions = options ?? MobileBarcodeScanningOptions.Default;

            // Initialize a new instance of SimpleCameraReader with Auto-Focus mode on
            if (_reader == null)
            {
                _reader = new SimpleCameraReader(options);
                _reader.ScanInterval = ScanningOptions.DelayBetweenAnalyzingFrames;

                // We need to set the VideoBrush we're going to display the preview feed on
                // IMPORTANT that it gets set before Camera initializes
                _previewVideo.SetSource(_reader.Camera);

                // The reader throws an event when a result is available 
                _reader.DecodingCompleted += (o, r) => DisplayResult(r);

                // The reader throws an event when the camera is initialized and ready to use
                _reader.CameraInitialized += ReaderOnCameraInitialized;

            }
        }

        public Action<Result> ScanCallback { get; set; }
        public MobileBarcodeScanningOptions ScanningOptions { get; set; }
        public MobileBarcodeScannerBase Scanner { get; set; }
        public UIElement CustomOverlay { get; set; }
        public bool UseCustomOverlay { get; set; }

        public Result LastScanResult { get; set; }

        SimpleCameraReader _reader;
        
        public bool IsTorchOn
        {
            get { return _reader.Camera.FlashMode == FlashMode.On; }
        }

        public bool CanTorch
        {
            get { return _reader.Camera.IsFlashModeSupported(FlashMode.On) == true ? true : false; }
        }

        public void Torch(bool on)
        {
            _reader.Camera.FlashMode = on ? FlashMode.On : FlashMode.Auto;
        }

        public void ToggleTorch()
        {
            _reader.Camera.FlashMode = _reader.Camera.FlashMode == FlashMode.On ? FlashMode.Off : FlashMode.On;
        }

        public void AutoFocus()
        {
            _reader.Camera.Focus();
        }

        public void StopScanning()
        {
            if (_reader != null) 
            { 
                _reader.Stop();
			    _reader = null;
            }

        }

        public void Cancel()
        {
            LastScanResult = null;

			StopScanning ();

            if (ScanCallback != null)
                ScanCallback(null);
        }
        
        private void ReaderOnCameraInitialized(object sender, bool initialized)
        {
            // We dispatch (invoke) to avoid access exceptions
            Dispatcher.BeginInvoke(() =>
            {
                if (_reader != null && _previewTransform != null)
                    _previewTransform.Rotation = _reader.CameraOrientation;
            });

            if (_reader != null)
            {
                // We can set if Camera should flash or not when focused
                _reader.FlashMode = FlashMode.Off;

                // Starts the capturing process
                _reader.Start();
            }
        }
        
        private void DisplayResult(Result result)
        {
			StopScanning ();

            if (ScanCallback != null)
                ScanCallback(result);
        }

        public void Dispose()
        {
            //this.gridCustomOverlay.Children.Clear();

			StopScanning (); 
        }

        protected override void OnTap(System.Windows.Input.GestureEventArgs e)
        {
            base.OnTap(e);

            if (_reader != null) 
            {
                //var pos = e.GetPosition(this);
                _reader.Focus();
            }
        }
    }
}
