using Sqlite_UWP.Navigation;
using Sqlite_UWP.ViewModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Sqlite_UWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VEdit : Page
    {
        string path;
        SQLite.Net.SQLiteConnection conn;

        public VEdit()
        {
            this.InitializeComponent();

            VMEdit vmedit = new VMEdit();
            this.DataContext = vmedit;

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Customer>();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            conn.Execute("UPDATE Customer SET nama = ?, kelas = ? WHERE id = ?", inputNama.Text, inputKelas.Text, Temporary.id);
        }
    }
}
