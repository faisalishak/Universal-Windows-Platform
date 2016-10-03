using Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Sqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TampilData : Page
    {
        string path, id;
        SQLite.Net.SQLiteConnection conn;

        public TampilData()
        {
            this.InitializeComponent();

            tanggal.Date = DateTime.Today;
            tanggal.MinDate = DateTime.Today;
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Deadline>();
        }

      

        private void simpan_Click(object sender, RoutedEventArgs e)
        {
            conn.Insert(new Deadline()
            {
                judul = judul.Text,
                tanggal = tanggal.Date.Value.ToString("yyyy-MM-dd"),
                konten = isi.Text,
                tag = tag.SelectionBoxItem.ToString(),
                dateTimeHandle = tanggal.Date.Value.ToString()
            });

            judul.Text = " ";
            tanggal.Date = DateTime.Today;
            isi.Text = " ";
            tag.SelectedIndex = 0;
        }

        private void OnScheduleToast(object sender, RoutedEventArgs e)
        {
            //string xml = @"<toast>
            //            <visual>
            //            <binding template=""ToastGeneric"">
            //            <text>Hello !</text>
            //            <text>This is aschedule toast !</text>
            //            </binding>
            //            </visual>
            //            </toast>";
            ////XmlDocument doc = new XmlDocument();
            //doc.LoadXml(xml);
            //ScheduledToastNotification toast = new ScheduledToastNotification(doc, DateTimeOffset.Now.AddSeconds(10));

            //ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        /*var updateData = koneksi.Query<deadLine>("select * from deadline where id = ?", deadLine.id).FirstOrDefault();
            if (updateData != null)
            {
                updateData.tag = "Sudah dibeli";
                koneksi.RunInTransaction(() =>
                {
                    koneksi.Update(updateData);
                });
            }*/
    }
}
