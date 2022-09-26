using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.MVVM.Model
{
    public class Departure
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

        private string _division;
        public string Division
        {
            get { return _division; }
            set { _division = value; }
        }

        private int _trackNumber;
        public int TrackNumber
        {
            get { return _trackNumber; }
            set { _trackNumber = value; }
        }

        private string _dirver;
        public string Driver
        {
            get { return _dirver; }
            set { _dirver = value; }
        }


        private string _senior;
        public string Senior
        {
            get { return _senior; }
            set { _senior = value; }
        }

        private int _tachometer;

        public int Tachometer
        {
            get { return _tachometer; }
            set { _tachometer = value; }
        }

        private DateTime _departureTime;

        public DateTime DepartureTime
        {
            get { return _departureTime; }
            set { _departureTime = value; }
        }

        private int? _tachometerEnd;

        public int? TachometerEnd
        {
            get { return _tachometerEnd; }
            set { _tachometerEnd = value; }
        }

        private DateTime? _arrivalTime;

        public DateTime? ArrivalTime
        {
            get { return _arrivalTime; }
            set { _arrivalTime = value; }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }



    }
}
