using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.Background;
using Microsoft.Toolkit.Uwp;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Sqlite;
using System.Diagnostics;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        string path, id;
        SQLite.Net.SQLiteConnection conn;

        public MainPage()
        {
            this.InitializeComponent();
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Deadline>();
            conn.CreateTable<Expired>();
            Debug.WriteLine(path);
           
            tampilkanData();
        }   

        private void tampilkanData()
        {
            var query = conn.Table<Deadline>();
            List<Deadline> listDeadline = new List<Deadline>();
            foreach (Deadline item in query)
            {
                listDeadline.Add(new Deadline()
                {
                    id = item.id,
                    judul = item.judul,
                    tanggal = item.tanggal,
                    konten = item.konten,
                    tag = item.tag,
                });
            }
            lvDeadline.ItemsSource = listDeadline;

            var queryExpired = conn.Table<Expired>();
            List<Expired> listExpired = new List<Expired>();
            foreach (Expired item in queryExpired)
            {
                listExpired.Add(new Expired()
                {
                    id = item.id,
                    judul = item.judul,
                    tanggal = item.tanggal,
                    konten = item.konten,
                    tag = item.tag.Replace("\n", "\n\r"),
                });
            }
            lvExpired.ItemsSource = listExpired;


            if (listDeadline.Count == 0)
            {
                keterangan.Visibility = Visibility.Visible;
            }
            else {
                keterangan.Visibility = Visibility.Collapsed;
            }

        }

        private void tampilan_ItemClick(object sender, ItemClickEventArgs e)
        {
            hapus.IsEnabled = true;
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TampilData));
        }

        private void hapus_Click(object sender, RoutedEventArgs e)
        {
            Deadline hapusDeadLine = lvDeadline.SelectedItems[0] as Deadline;
            conn.Delete<Deadline>(hapusDeadLine.id);
            hapus.IsEnabled = false;
            tampilkanData();
        }

        //protected override async void OnNavigatedTo(NavigationEventArgs e) {
        //    if (BackgroundTaskRegistration.AllTasks.All(x => x.Value.Name != "ToastTask")) {
        //        BackgroundTaskBuilder builder = new BackgroundTaskBuilder();
        //        builder.Name = "ToastTask";
        //        builder.TaskEntryPoint = "ToastTask.CheckAnswerTask";
        //        builder.SetTrigger(new ToastNotificationActionTrigger());
        //        var status = await BackgroundExecutionManager.RequestAccessAsync();
        //        if (status != BackgroundAccessStatus.Denied) {
        //            builder.Register();
        //        }
        //    }
        //} 






    }
}
