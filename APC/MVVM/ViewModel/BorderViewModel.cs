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
    public class BorderViewModel : BaseViewModel
    {
        private Cars _selectedCar;

        public Cars SelectedCar
        {
            get { return _selectedCar; }
            set { 
                _selectedCar = value;
                OnPropertyChanged("SelectedCar");
            }
        }

        private Window currentWindow;
        public BorderViewModel()
        {

        }
        public BorderViewModel(Cars car)
        {
            SelectedCar = car;
            foreach (Window item in App.Current.Windows)
            {
                if (item is BorderWindow)
                {
                    currentWindow = item;
                }
            }
        }

        private string _borderNumber;
        public string BorderNumber
        {
            get { return _borderNumber; }
            set { 
                _borderNumber = value;
                OnPropertyChanged("BorderNumber");
            }
        }

        private RelayCommand _carToBorderCommand;

        public RelayCommand CarToBorderCommand
        {
            get
            {
                return _carToBorderCommand ?? (_carToBorderCommand = new RelayCommand(obj =>
                {
                    SelectedCar.Status = $"В распо. {BorderNumber} ПОГЗ";
                    App.db.Cars.Update(SelectedCar);
                    try
                    {
                        App.db.SaveChanges();
                        ((BorderWindow)currentWindow).DialogResult = true;
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.ToString());
                        ((BorderWindow)currentWindow).DialogResult = false;
                    }
                    
                }));
            }
        }


    }
}
