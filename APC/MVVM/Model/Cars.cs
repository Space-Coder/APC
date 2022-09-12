using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.MVVM.Model
{
    public class Cars
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _carMark;

        public string CarMark
        {
            get { return _carMark; }
            set { _carMark = value; }
        }

        private string _number;

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        private int _tachometer;

        public int Tachometer
        {
            get { return _tachometer; }
            set { _tachometer = value; }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }


    }
}
