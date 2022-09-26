using APC.Command;
using APC.MVVM.Model;
using APC.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace APC.MVVM.ViewModel
{
    public class DepartureViewModel : BaseViewModel
    {
        private Window currentWindow;
        public DepartureViewModel()
        {

        }
        public DepartureViewModel(Cars selectedCar)
        {
            DepartureCar.CarMark = selectedCar.CarMark;
            DepartureCar.Number = selectedCar.Number;
            DepartureCar.Tachometer = selectedCar.Tachometer;
            foreach (Window item in App.Current.Windows)
            {
                if (item is DepartureWindow)
                {
                    currentWindow = item;
                }
            }
        }

        private Departure _departureCar;
        public Departure DepartureCar
        {
            get {
                if (_departureCar == null)
                {
                    _departureCar = new Departure();
                    _departureCar.DepartureTime = DateTime.Now;
                }
                return _departureCar;
            }
            set { 
                _departureCar = value;
                OnPropertyChanged("DepartureCar");
            }
        }

        private RelayCommand _departureCarCommand;
        public RelayCommand DepartureCarCommand
        {
           get
            {
                return _departureCarCommand ?? (_departureCarCommand = new RelayCommand(obj =>
                 {
                     DepartureCar.Status = "На выезде";
                     try
                     {
                         App.db.Departures.Add(DepartureCar);
                         App.db.SaveChanges();
                         ((DepartureWindow)currentWindow).DialogResult = true;
                     }
                     catch (Exception exc)
                     {
                         MessageBox.Show(exc.ToString());
                         ((DepartureWindow)currentWindow).DialogResult = false;
                     }
                     
                 }));
            }
        }


    }
}
