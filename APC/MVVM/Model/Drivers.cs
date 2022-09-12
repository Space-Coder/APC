using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.MVVM.Model
{
    public class Drivers
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _fio;

        public string FIO
        {
            get { return _fio; }
            set { _fio = value; }
        }

    }
}
