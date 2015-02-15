using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Govision
{
    public partial class Settings : PhoneApplicationPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void AppThemeButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AppSettings/Theme.xaml", UriKind.Relative));
        }

        private void DebugModeButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AppSettings/DebugMode.xaml", UriKind.Relative));
        }

        private void HistoryButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AppSettings/HistorySettings.xaml", UriKind.Relative));
        }
    }
}