using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlite
{
    class Deadline
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string judul { get; set; }
        public string tanggal{ get; set; }
        public string konten { get; set; }
        public string tag { get; set; }
        public string dateTimeHandle { get; set; }

        public override string ToString()
        {
            return
                Environment.NewLine +
                judul + Environment.NewLine +
                tanggal + Environment.NewLine +
                konten + Environment.NewLine +
                "[" + tag + "]" +
                Environment.NewLine;
        }
    }

}
