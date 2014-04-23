using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Personal.Resources;
using System.Threading;
using Newtonsoft.Json;


namespace Personal
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
           
            
          
            //NavigationService.Navigate(new Uri("/Pantallas/Login.xaml", UriKind.RelativeOrAbsolute));
            //NavigationService.Navigate(new Uri("Home.xaml",UriKind.Relative));
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void loadingProgress_Loaded(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(1000);
            NavigationService.Navigate(new Uri("/Pantallas/Home.xaml", UriKind.RelativeOrAbsolute));
            
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}