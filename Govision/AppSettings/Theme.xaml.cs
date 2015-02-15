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
using System.ComponentModel;
using System.IO.IsolatedStorage;

namespace Govision.AppSettings
{
    public partial class Theme : PhoneApplicationPage
    {
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        public Theme()
        {
            InitializeComponent();

            if (settings.Contains("ThemeColor"))
            {
                picker.Color = ConvertStringToColor(settings["ThemeColor"].ToString());
                ColorRect.Fill = new SolidColorBrush(ConvertStringToColor(settings["ThemeColor"].ToString()));
                ColorRect2.Fill = new SolidColorBrush(ConvertStringToColor(settings["ThemeColor"].ToString()));
            }
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

        private void picker_ColorChanged(object sender, System.Windows.Media.Color color)
        {
            ColorRect.Fill = new SolidColorBrush(color);
            ColorRect2.Fill = new SolidColorBrush(color);
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (!settings.Contains("ThemeColor"))
            {
                settings.Add("ThemeColor", ((SolidColorBrush)ColorRect.Fill).Color.ToString());
            }
            else
            {
                settings["ThemeColor"] = ((SolidColorBrush)ColorRect.Fill).Color.ToString();
            }

            settings.Save();

            NavigationService.GoBack();
        }

    }
}