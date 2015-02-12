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
    }
}