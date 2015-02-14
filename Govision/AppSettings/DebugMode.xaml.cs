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

namespace Govision.AppSettings
{
    public partial class DebugMode : PhoneApplicationPage
    {

        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;


        public DebugMode()
        {
            InitializeComponent();

            if (settings["DebugMode"].ToString() == "true")
            {
                debugSwitch.Content = "On";
                debugSwitch.IsChecked = true;
            }

        }

        private void debugSwitch_Checked(object sender, RoutedEventArgs e)
        {

            debugSwitch.Content = "On";

            if (!settings.Contains("DebugMode"))
            {
                settings.Add("DebugMode", "true");
            }
            else
            {
                settings["DebugMode"] = "true";
            }

            settings.Save();

        }

        private void debugSwitch_Unchecked(object sender, RoutedEventArgs e)
        {

            debugSwitch.Content = "Off";

            if (!settings.Contains("DebugMode"))
            {
                settings.Add("DebugMode", "false");
            }
            else
            {
                settings["DebugMode"] = "false";
            }

            settings.Save();
        }
    }
}