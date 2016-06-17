using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdaptiveCode
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //var i = "Windows.Phone.UI.Input.HardwareButtons";

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                //Windows.Phone.UI.Input.HardwareButtons.CameraPressed += CameraButtonPressed;
            }


            // StatusBar is Mobile only 
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().BackgroundColor = Windows.UI.Colors.Blue;
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().BackgroundOpacity = 1;
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ForegroundColor = Windows.UI.Colors.MediumSpringGreen;
            }
        }

        /*private void CameraButtonPressed(object sender, CameraEventArgs e)
        {
            throw new NotImplementedException();
        }*/

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            // Bottom AppBar shows on Desktop and Mobile 
            if (ShowAppBarRadioButton != null)
            {

                if (ShowAppBarRadioButton.IsChecked.HasValue &&
                   (ShowAppBarRadioButton.IsChecked.Value == true))
                {
                    commandBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    commandBar.Opacity = 1;
                }
                else
                {
                    commandBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                }
            }

            if (ShowOpaqueAppBarRadioButton != null)
            {

                if (ShowOpaqueAppBarRadioButton.IsChecked.HasValue &&
                   (ShowOpaqueAppBarRadioButton.IsChecked.Value == true))
                {
                    commandBar.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    commandBar.Background.Opacity = 0;
                }
                else
                {
                    commandBar.Background.Opacity = 1;
                }
            }

        }

        private void StatusBarHiddenCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            // StatusBar is Mobile only 
            if (Windows.Foundation.Metadata.ApiInformation.
               IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var ignore = Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
            }
        }

        private void StatusBarHiddenCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            // StatusBar is Mobile only 
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var ignore = Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ShowAsync();
            }
        }

        private void StatusBarBackgroundCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            // StatusBar is Mobile only 
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().BackgroundColor = Windows.UI.Colors.Blue;
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().BackgroundOpacity = 1;
            }
        }

        private void StatusBarBackgroundCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // StatusBar is Mobile only 
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManag ement.StatusBar"))
            {
                Windows.UI.ViewManagement.StatusBar.GetForCurrentView().
                   BackgroundOpacity = 0;
            }
        }

    }
}
