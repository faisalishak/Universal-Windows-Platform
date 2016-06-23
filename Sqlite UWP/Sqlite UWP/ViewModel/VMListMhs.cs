using Sqlite_UWP.Common;
using Sqlite_UWP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlite_UWP.ViewModel
{
    class VMListMhs : ViewModelBase
    {
        string path;
        SQLite.Net.SQLiteConnection conn;
        private ObservableCollection<MMhs> collectionMhs = new ObservableCollection<MMhs>();

        public ObservableCollection<MMhs> CollectionMhs
        {
            get { return collectionMhs; }
            set
            {
                if ( collectionMhs != value)
                {
                    collectionMhs = value;
                    RaisePropertyChanged("");
                }
            }
        }

        //Constructor
        public VMListMhs()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Customer>();

            LoadUrl();
        }

        private void LoadUrl()
        {
            var query = conn.Table<Customer>();

            foreach (var msg in query)
            {
                MMhs mhs = new MMhs();  
                mhs.id = msg.id.ToString();
                mhs.nama =  msg.nama;
                mhs.kelas =  msg.kelas;

                collectionMhs.Add(mhs);
            }

            ShowToast st = new ShowToast(7, "success");
        }
    }
}
