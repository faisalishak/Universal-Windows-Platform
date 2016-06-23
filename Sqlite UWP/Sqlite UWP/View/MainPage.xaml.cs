using Sqlite_UWP.Model;
using Sqlite_UWP.Navigation;
using Sqlite_UWP.View;
using Sqlite_UWP.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sqlite_UWP
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
            VMListMhs l = new VMListMhs();
            ListMhs.DataContext = l;

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Customer>();

        }

        /*private object getDataAnggota()
        {
            Customer cus = new Customer();
            var query = conn.Table<Customer>();

            foreach (var msg in query)
            {
                
                cus.id = msg.id;
                cus.nama =  " " + msg.nama;
                cus.kelas=  " " + msg.kelas;

            }

            return null;
        }*/

        private void BSubmit_Click(object sender, RoutedEventArgs e)
        {
            /*Customer m = (from p in conn.Table<Customer>()
                          where p.id == 2
                          select p).FirstOrDefault();
            string nama = IName.Text;
            string kelas = IKelas.Text;

            conn.Execute("INSERT INTO Customer VALUES (null, ?, ?)", nama, kelas);*/

            /*var s = conn.Insert(new Customer()
            {
                nama = IName.Text,
                kelas = IKelas.Text
            });*/

            var query = conn.Table<Customer>();
            string id = "";
            string name = "";
            string classs = "";

            //ORetrieve.Text = "ID : " + id + "\nNama : " + name + "\nKelas : " + classs;
        }

        private void SymbolIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Temporary.id = id;

            this.Frame.Navigate(typeof(VEdit));
        }

        private void SymbolIcon_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            conn.Execute("DELETE FROM Customer WHERE id = ?", id);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(VAdd));
        }

        private void ListMhs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MMhs data = (sender as ListView).SelectedItem as MMhs;
            id = data.id.ToString();
        }
    }
}
