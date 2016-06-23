using Sqlite_UWP.Common;
using Sqlite_UWP.Model;
using Sqlite_UWP.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlite_UWP.ViewModel
{
    class VMEdit : ViewModelBase
    {
        string path = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
        SQLite.Net.SQLiteConnection conn;

        private MMhs mhs;

        public MMhs Mhs
        {
            get { return mhs; }
            set { mhs = value; }
        }

        public String nama
        {
            get { return Mhs.nama; }
            set
            {
                    Mhs.nama = value;
                RaisePropertyChanged("");
            }
        }

        public String kelas
        {
            get { return Mhs.kelas; }
            set
            {
                Mhs.kelas = value;
                RaisePropertyChanged("");
            }
        }

        public VMEdit()
        {
            mhs = new MMhs();
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<Customer>();
            loadData();
        }

        private void loadData()
        {             
            var query = conn.Query<Customer>("SELECT * FROM Customer WHERE id =" + Temporary.id);
            foreach(var item in query)
            {
                nama = item.nama;
                kelas = item.kelas;
            }
           
            
        }
    }
}
