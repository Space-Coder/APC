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
    public class RepairViewModel : BaseViewModel
    {
        private Window currentWindow;
        public RepairViewModel()
        {

        }
        public RepairViewModel(Cars car)
        {
            CarOnRepair.CarMark = car.CarMark;
            CarOnRepair.Number = car.Number;
            CarOnRepair.Tachometer = car.Tachometer;
            foreach (Window item in App.Current.Windows)
            {
                if (item is RepairWindow)
                {
                    currentWindow = item;
                }
            }
        }

        private Repair _carOnRepair;
        public Repair CarOnRepair
        {
            get {
                if (_carOnRepair == null)
                {
                    _carOnRepair = new Repair();
                }
                return _carOnRepair;
            }
            set { 
                _carOnRepair = value;
                OnPropertyChanged("CarOnRepair");
            }
        }

        private RelayCommand _carOnRepairCommand;
        public RelayCommand CarOnRepairCommand
        {
            get
            {
                return _carOnRepairCommand ?? (_carOnRepairCommand = new RelayCommand(obj =>
                {
                    CarOnRepair.Status = "В ремонте";
                    App.db.Repair.Add(CarOnRepair);
                    App.db.SaveChanges();
                    ((RepairWindow)currentWindow).DialogResult = true;
                }));
            }
        }

    }
}
