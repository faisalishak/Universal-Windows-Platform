using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlite_UWP
{
    class Customer
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string nama { get; set; }
        public string kelas { get; set; }
    }
}
