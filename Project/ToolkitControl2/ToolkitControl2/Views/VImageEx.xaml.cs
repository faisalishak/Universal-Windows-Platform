using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using ToolkitControl2.Models;
using ToolkitControl2.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ToolkitControl2.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VImageEx : Page
    {
        private ObservableCollection<MPhotoDataItem> photos;
        private int imageIndex;


        public VImageEx()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);           

            //await LoadDataAsync();

            AddImage(false, true);
        }

        private async Task LoadDataAsync()
        {
            photos = await new VMPhotoDataSource().GetItemsAsync(true);
        }

        private void AddImage(bool broken, bool placeholder)
        {
            var newImage = new ImageEx
            {
                IsCacheEnabled = true,
                Stretch = Stretch.UniformToFill,
                Source = broken ? "ms-appx:///Assets/Photos/ColumbiaRiverGorge.png" : "ms-appx:///Assets/Photos/ColumbiaRiverGorge.png",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                MaxWidth = 300,
                Background = new SolidColorBrush(Colors.Transparent),
                Foreground = new SolidColorBrush(Colors.White)
            };

            if (placeholder)
            {
                newImage.PlaceholderSource = new BitmapImage(new Uri("ms-appx:///Assets/Photos/ImageExPlaceholder.jpg"));
                newImage.PlaceholderStretch = Stretch.UniformToFill;
            }

            Container.Children.Add(newImage);

            // Move to next image
            //imageIndex++;
            //if (imageIndex >= photos.Count)
            //{
            //    imageIndex = 0;
            //}
        }

        private void WithPlaceHolder_Click(object sender, RoutedEventArgs e)
        {
            AddImage(false, true);
        }
    }
}
