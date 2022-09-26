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
    public class ParkViewModel : BaseViewModel
    {
        private Window currentWindow;
        public ParkViewModel()
        {
        }
        public ParkViewModel(Departure selectedCar)
        {
            CarFromDeparture = selectedCar;
            CarFromDeparture.ArrivalTime = DateTime.Now;
            foreach (Window item in App.Current.Windows)
            {
                if (item is ParkWindow)
                {
                    currentWindow = item;
                }
            }
        }

        private Departure _carFromDeparture;
        public Departure CarFromDeparture
        {
            get
            {
                if (_carFromDeparture == null)
                {
                    _carFromDeparture = new Departure();
                }
                return _carFromDeparture;
            }
            set { 
                _carFromDeparture = value;
                OnPropertyChanged("CarFromDeparture");
            }
        }

        private RelayCommand _carToParkCommand;
        public RelayCommand CarToParkCommand
        {
            get
            {
                return _carToParkCommand ?? (_carToParkCommand = new RelayCommand(obj =>
                {
                    CarFromDeparture.Status = "В парке";
                    var carTachometerUpdate = App.db.Cars.FirstOrDefault(c => c.Number == CarFromDeparture.Number);
                    try
                    {
                        carTachometerUpdate.Tachometer = int.Parse(CarFromDeparture.TachometerEnd.ToString());
                        App.db.Cars.Update(carTachometerUpdate);
                        App.db.Departures.Update(CarFromDeparture);
                        App.db.SaveChanges();
                        ((ParkWindow)currentWindow).DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        ((ParkWindow)currentWindow).DialogResult = false;
                    }
                    
                    
                }));
            }
        }
    }
}
