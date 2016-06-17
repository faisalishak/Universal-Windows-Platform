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
        string path;
        SQLite.Net.SQLiteConnection conn;
        ObservableCollection<Customer> listPengguna = new ObservableCollection<Customer>();


        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = getDataAnggota();

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Customer>();
        }

        private object getDataAnggota()
        {
            Customer cus = new Customer();
            var query = conn.Table<Customer>();

            foreach (var msg in query)
            {
                cus.id = msg.id;
                cus.nama =  " " + msg.nama;
                cus.kelas=  " " + msg.kelas;

                listPengguna.Add(cus);
            }

            return listPengguna;
        }

        private void BSubmit_Click(object sender, RoutedEventArgs e)
        {
            /*Customer m = (from p in conn.Table<Customer>()
                          where p.id == 2
                          select p).FirstOrDefault();*/
            string nama = IName.Text;
            string kelas = IKelas.Text;

            conn.Execute("INSERT INTO Customer VALUES (null, ?, ?)", nama, kelas);

            /*var s = conn.Insert(new Customer()
            {
                nama = IName.Text,
                kelas = IKelas.Text
            });*/

            var query = conn.Table<Customer>();
            string id = "";
            string name = "";
            string classs = "";

            ORetrieve.Text = "ID : " + id + "\nNama : " + name + "\nKelas : " + classs;
            ShowToast(5, "Halo dunia");
        }

        private void ShowToast(int timeoutInSeconds, string text)
        {
            var toastTemplate = ToastTemplateType.ToastImageAndText01;
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(text));

            ////Andreas Hammar 2014-10-08 08:39: note! does not work on windows phone
            //var toastImageAttributes = toastXml.GetElementsByTagName("image");
            //((XmlElement)toastImageAttributes[0]).SetAttribute("src", "ms-appx:///Assets/jay_transparent.png");
            //((XmlElement)toastImageAttributes[0]).SetAttribute("alt", "jay");

            var toast = new ToastNotification(toastXml);
            if (timeoutInSeconds >= 0)
                toast.ExpirationTime = DateTime.Now.AddSeconds(timeoutInSeconds);

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
