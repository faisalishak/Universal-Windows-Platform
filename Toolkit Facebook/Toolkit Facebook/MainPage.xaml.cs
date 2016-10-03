using Microsoft.Toolkit.Uwp.Services.Facebook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Toolkit_Facebook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();

            string SID = WebAuthenticationBroker.GetCurrentApplicationCallbackUri().ToString();
            Debug.WriteLine(SID);

            QueryType.ItemsSource = new[] { "My feed", "My posts", "My tagged" };
            QueryType.SelectedIndex = 0;

            ShareBox.Visibility = Visibility.Collapsed;
            HidePostPanel();


        }

        private async void ConnectButton_OnClick(object sender, RoutedEventArgs e)
        {
            //if (!await Tools.CheckInternetConnection())
            //{
            //    return;
            //}

            //Shell.Current.DisplayWaitRing = true;
            FacebookService.Instance.Initialize("683612841803271");
            if (!await FacebookService.Instance.LoginAsync())
            {
                ShareBox.Visibility = Visibility.Collapsed;
               // Shell.Current.DisplayWaitRing = false;
                var error = new MessageDialog("Unable to log to Facebook");
                await error.ShowAsync();
                return;
            }

            FacebookDataConfig config;
            switch (QueryType.SelectedIndex)
            {
                case 1:
                    config = FacebookDataConfig.MyPosts;
                    break;
                case 2:
                    config = FacebookDataConfig.MyTagged;
                    break;
                default:
                    config = FacebookDataConfig.MyFeed;
                    break;
            }

            ListView.ItemsSource = await FacebookService.Instance.RequestAsync(config, 70);

            HideCredentialsPanel();

            ShareBox.Visibility = Visibility.Visible;
            ShowPostPanel();

            ProfileImage.DataContext = await FacebookService.Instance.GetUserPictureInfoAsync();
            ProfileImage.Visibility = Visibility.Visible;
           // Shell.Current.DisplayWaitRing = false;
        }

        private async void ShareButton_OnClick(object sender, RoutedEventArgs e)
        {
            //if (!await Tools.CheckInternetConnection())
            //{
            //    return;
            //}

            await FacebookService.Instance.PostToFeedWithDialogAsync(TitleText.Text, DescriptionText.Text, UrlText.Text);
            var message = new MessageDialog("Post sent to facebook");
            await message.ShowAsync();
        }

        private async void SharePictureButton_OnClick(object sender, RoutedEventArgs e)
        {
            //if (!await Tools.CheckInternetConnection())
            //{
            //    return;
            //}

            var openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile picture = await openPicker.PickSingleFileAsync();
            if (picture != null)
            {
                using (var stream = await picture.OpenReadAsync())
                {
                    await FacebookService.Instance.PostPictureToFeedAsync(TitleText.Text, picture.Name, stream);
                }
            }
        }

        private void CredentialsBoxExpandButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CredentialsBox.Visibility == Visibility.Visible)
            {
                HideCredentialsPanel();
            }
            else
            {
                ShowCredentialsPanel();
            }
        }

        private void PostBoxExpandButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (PostPanel.Visibility == Visibility.Visible)
            {
                HidePostPanel();
            }
            else
            {
                ShowPostPanel();
            }
        }

        private void ShowCredentialsPanel()
        {
            CredentialsBoxExpandButton.Content = "";
            CredentialsBox.Visibility = Visibility.Visible;
        }

        private void HideCredentialsPanel()
        {
            CredentialsBoxExpandButton.Content = "";
            CredentialsBox.Visibility = Visibility.Collapsed;
        }

        private void ShowPostPanel()
        {
            PostBoxExpandButton.Content = "";
            PostPanel.Visibility = Visibility.Visible;
        }

        private void HidePostPanel()
        {
            PostBoxExpandButton.Content = "";
            PostPanel.Visibility = Visibility.Collapsed;
        }

        private async void Logout_Click(object sender, RoutedEventArgs e)
        {
            await FacebookService.Instance.LogoutAsync();
        }
    }
}
