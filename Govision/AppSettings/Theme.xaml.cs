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
                picker.Color = (Color)settings["ThemeColor"];
                ColorRect.Fill = new SolidColorBrush((Color)settings["ThemeColor"]);
                ColorRect2.Fill = new SolidColorBrush((Color)settings["ThemeColor"]);
            }
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
                settings.Add("ThemeColor", ((SolidColorBrush)ColorRect.Fill).Color);
            }
            else
            {
                settings["ThemeColor"] = ((SolidColorBrush)ColorRect.Fill).Color;
            }

            settings.Save();

            NavigationService.GoBack();
        }

    }
}