using APC.Context;
using APC.MVVM.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APC.Command;
using System.Windows;
using System.Windows.Controls;

namespace APC.MVVM.ViewModel
{
    public class CarsViewModel : BaseViewModel
    {
        private Window currentWindow;
        private ObservableCollection<Cars> _cars;
        public ObservableCollection<Cars> Cars
        {
            get
            {
                if (_cars == null)
                {
                    _cars = new ObservableCollection<Cars>();
                } 
                return _cars;
            }
            set { 
                _cars = value;
                OnPropertyChanged("Cars");
            }
        }

        private ObservableCollection<Departure> _departures;
        public ObservableCollection<Departure> Departures
        {
            get 
            {
                if(_departures == null)
                {
                    _departures = new ObservableCollection<Departure>();
                }
                return _departures;
            }
            set { 
                _departures = value;
                OnPropertyChanged("Departures");
            }
        }

        private ObservableCollection<Repair> _repairs;
        public ObservableCollection<Repair> Repairs
        {
            get
            {
                if (_repairs == null)
                {
                    _repairs = new ObservableCollection<Repair>();
                }
                return _repairs;
            }
            set { 
                _repairs = value;
                OnPropertyChanged("Repairs");
            }
        }

        public CarsViewModel()
        {
            foreach (var item in App.db.Cars)
            {
                Cars.Add(item);
            }
            foreach (var item in App.db.Departures)
            {
                Departures.Add(item);
            }
            foreach (var item in App.db.Repair)
            {
                Repairs.Add(item);
            }
            foreach(Window item in App.Current.Windows)
            {
                if (item is MainWindow)
                {
                    currentWindow = item;
                }
            }
            CountCars();
        }

        private void CountCars()
        {
            CounterAll = Cars.Count;
            CounterPark = Cars.Where(c => c.Status == "В парке").Count();
            CounterDeparure = Departures.Count();
            CounterRepair = Repairs.Count();
            CounterBorder = Cars.Where(c => c.Status.StartsWith("В распо.")).Count();
        }

        private Cars _selectedCar;
        public Cars SelectedCar
        {
            get { return _selectedCar; }
            set { 
                _selectedCar = value;
                OnPropertyChanged("SelectedCar");
            }
        }

        private int _counterAll;

        public int CounterAll
        {
            get { return _counterAll; }
            set { 
                _counterAll = value;
                OnPropertyChanged("CounterAll");
            }
        }
        private int _counterPark;

        public int CounterPark
        {
            get { return _counterPark; }
            set { 
                _counterPark = value;
                OnPropertyChanged("CounterPark");
            }
        }

        private int _counterDeparture;

        public int CounterDeparure
        {
            get { return _counterDeparture; }
            set { 
                _counterDeparture = value;
                OnPropertyChanged("CounterDeparture");
            }
        }

        private int _counterRepair;

        public int CounterRepair
        {
            get { return _counterRepair; }
            set { 
                _counterRepair = value;
                OnPropertyChanged("CounterRepair");
            }
        }

        private int _counterBorder;

        public int CounterBorder
        {
            get { return _counterBorder; }
            set { 
                _counterBorder = value;
                OnPropertyChanged("CounterBorder");
            }
        }


        private RelayCommand _selectAllCarsCommand;

        public RelayCommand SelectAllCarsCommand
        {
            get
            {
                return _selectAllCarsCommand ?? (_selectAllCarsCommand = new RelayCommand(obj =>
                {
                    ((DataGrid)obj).ItemsSource = Cars;
                }));
            }
        }

        private RelayCommand _selectParkCarsCommand;

        public RelayCommand SelectParkCarsCommand
        {
            get
            {
                return _selectParkCarsCommand ?? (_selectParkCarsCommand = new RelayCommand(obj =>
                {
                    ((DataGrid)obj).ItemsSource = Cars.Where(c => c.Status == "В парке");
                }));
            }
        }

        private RelayCommand _selectDeparturedCarsCommand;

        public RelayCommand SelectDeparutredCarsCommand
        {
            get
            {
                return _selectDeparturedCarsCommand ?? (_selectDeparturedCarsCommand = new RelayCommand(obj =>
                {
                    ((DataGrid)obj).ItemsSource = Departures.Where(c => c.Status == "На выезде");
                }));
            }
        }

        private RelayCommand _selectRepairCarsCommand;

        public RelayCommand SelectReapirCarsCommand
        {
            get
            {
                return _selectRepairCarsCommand ?? (_selectRepairCarsCommand = new RelayCommand(obj =>
                {
                    ((DataGrid)obj).ItemsSource = Repairs;
                }));
            }
        }

        private RelayCommand _selectBorderCarsCommand;

        public RelayCommand SelectBorderCarsCommand
        {
           get
            {
                return _selectBorderCarsCommand ?? (_selectBorderCarsCommand = new RelayCommand(obj =>
                {
                    ((DataGrid)obj).ItemsSource = Cars.Where(c => c.Status.StartsWith("В распо."));
                }));
            }
        }

        private RelayCommand _horizontalLinesVisibleCommand;

        public RelayCommand HorizontalLinesVisibleCommand
        {
            get
            {
                return _horizontalLinesVisibleCommand ?? (_horizontalLinesVisibleCommand = new RelayCommand(obj =>
                {
                    if (((DataGrid)obj).GridLinesVisibility == DataGridGridLinesVisibility.Vertical)
                    {
                        ((DataGrid)obj).GridLinesVisibility = DataGridGridLinesVisibility.All;
                    }
                    else if(((DataGrid)obj).GridLinesVisibility == DataGridGridLinesVisibility.None)
                    {
                        ((DataGrid)obj).GridLinesVisibility = DataGridGridLinesVisibility.Horizontal;
                    }
                    else if (((DataGrid)obj).GridLinesVisibility == DataGridGridLinesVisibility.All)
                    {
                        ((DataGrid)obj).GridLinesVisibility = DataGridGridLinesVisibility.Vertical;
                    }
                    else if (((DataGrid)obj).GridLinesVisibility == DataGridGridLinesVisibility.Horizontal)
                    {
                        ((DataGrid)obj).GridLinesVisibility = DataGridGridLinesVisibility.None;
                    }
                }));
            }
        }

        private RelayCommand _verticalLinesVisibleCommand;

        public RelayCommand VertialLinesVisibleCommand
        {
            get
            {
                return _verticalLinesVisibleCommand ?? (_verticalLinesVisibleCommand = new RelayCommand(obj =>
                {
                    if (((DataGrid)obj).GridLinesVisibility == DataGridGridLinesVisibility.Horizontal)
                    {
                        ((DataGrid)obj).GridLinesVisibility = DataGridGridLinesVisibility.All;
                    }
                    else if (((DataGrid)obj).GridLinesVisibility == DataGridGridLinesVisibility.None)
                    {
                        ((DataGrid)obj).GridLinesVisibility = DataGridGridLinesVisibility.Vertical;
                    }
                    else if (((DataGrid)obj).GridLinesVisibility == DataGridGridLinesVisibility.All)
                    {
                        ((DataGrid)obj).GridLinesVisibility = DataGridGridLinesVisibility.Horizontal;
                    }
                    else if (((DataGrid)obj).GridLinesVisibility == DataGridGridLinesVisibility.Vertical)
                    {
                        ((DataGrid)obj).GridLinesVisibility = DataGridGridLinesVisibility.None;
                    }
                }));
            }
        }


        private RelayCommand _exitAppCommand;

        public RelayCommand ExitAppCommand
        {
            get
            {
                return _exitAppCommand ?? (_exitAppCommand = new RelayCommand(obj =>
                {
                    MessageBoxResult res = MessageBox.Show("Вы действительно хотите выйти ?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (res == MessageBoxResult.Yes)
                    {
                        Application.Current.Shutdown();
                    }
                }));
            }
        }

        private RelayCommand _onDepartureCommand;

        public RelayCommand OnDepartureCommand
        {
            get
            {
                return _onDepartureCommand ?? (_onDepartureCommand = new RelayCommand(obj =>
               {
                   if (SelectedCar == null)
                   {
                       ((MainWindow)currentWindow).InfoBar.ShowMessage("Для начала выберите машину", InfoBarStatus.ATTENTION, 7000);
                       return;
                   }
                   else if (SelectedCar.Status == "На выезде")
                   {
                       ((MainWindow)currentWindow).InfoBar.ShowMessage("Эта машина уже на выезде", InfoBarStatus.ATTENTION, 7000);
                       return;
                   }
                   var carOnDeparture = Cars.FirstOrDefault(c=>c.ID == SelectedCar.ID);
                   carOnDeparture.Status = "На выезде";
                   
               }));
            }
        }


    }
}
