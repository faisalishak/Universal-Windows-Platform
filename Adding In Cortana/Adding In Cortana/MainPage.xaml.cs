using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Adding_In_Cortana
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///CortanaCommands.xml"));
            await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(storageFile);
        }

        public void createRectangle(Color color)
        {
            Random rnd = new Random();
            var left = rnd.Next(0, 300);
            var top = rnd.Next(0, 300);

            var rect = new Windows.UI.Xaml.Shapes.Rectangle();
            rect.Height = 100;
            rect.Width = 100;
            rect.Margin = new Thickness(left, top, 0, 0);

            rect.Fill = new SolidColorBrush(color);

            LayoutGrid.Children.Add(rect);
        }

        public void createCircle(Color color)
        {
            Random rnd = new Random();
            var left = rnd.Next(0, 300);
            var top = rnd.Next(0, 300);

            var circle = new Windows.UI.Xaml.Shapes.Ellipse();
            circle.Height = 100;
            circle.Width = 100;
            circle.Margin = new Thickness(left, top, 0, 0);

            circle.Fill = new SolidColorBrush(color);

            LayoutGrid.Children.Add(circle);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Color color = Colors.Red;
            createCircle(color);
        }
    }
}
