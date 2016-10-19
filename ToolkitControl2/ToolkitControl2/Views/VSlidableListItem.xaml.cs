using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ToolkitControl2.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class VSlidableListItem : Page
    {
        private ObservableCollection<MItems> _items;

        public VSlidableListItem()
        {
            this.InitializeComponent();

            ObservableCollection<MItems> items = new ObservableCollection<MItems>();

            for (var i = 0; i < 1000; i++)
            {
                items.Add(new MItems() { Title = "Item " + i });
            }

            _items = items;
        }

        private bool CanExecuteDeleteItemCommand(MItems item)
        {
            return true;
        }

        private void ExecuteDeleteItemCommand(MItems item)
        {
            _items.Remove(item);
        }

        private void SlidableListItem_RightCommandRequested(object sender, EventArgs e)
        {

        }
    }
}
