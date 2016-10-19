using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ToolkitControl2.Models;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ToolkitControl2.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VPullToRefresh : Page
    {
        private readonly ObservableCollection<MItems> _items;

        public VPullToRefresh()
        {
            this.InitializeComponent();

            _items = new ObservableCollection<MItems>();
            AddItems();
        }

        private void listView_RefreshRequested(object sender, EventArgs e)
        {
            AddItems();
        }


        private void listView_PullProgressChanged(object sender, Microsoft.Toolkit.Uwp.UI.Controls.RefreshProgressEventArgs e)
        {
            refreshIndicator.Opacity = e.PullProgress;

            refreshIndicator.Background = e.PullProgress < 10 ? new SolidColorBrush(Colors.DarkBlue) : new SolidColorBrush(Colors.Crimson); 
        }

        private void AddItems()
        {
            for (int i = 0; i < 50; i++)
            {
                _items.Insert(0, new MItems { Title = "Item " + new Random().Next(10000) });
            }
        }

    }
}
