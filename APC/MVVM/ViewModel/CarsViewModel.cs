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
using APC.MVVM.View;
using System.Windows.Data;

namespace APC.MVVM.ViewModel
{
    public class CarsViewModel : BaseViewModel
    {
        private Window currentWindow;
        private readonly Binding bindDeparures = new Binding();
        private readonly Binding bindCars = new Binding();
        private readonly Binding bindReapir = new Binding();
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
            foreach(Window item in App.Current.Windows)
            {
                if (item is MainWindow)
                {
                    currentWindow = item;
                }
            }
            bindCars.Path = new PropertyPath("SelectedCar");
            bindDeparures.Path = new PropertyPath("SelectedDepartureCar");
            bindReapir.Path = new PropertyPath("SelectedRepairCar");
            LoadCars();
            CountCars();
        }

        public void LoadCars()
        {
            Cars.Clear();
            Departures.Clear();
            Repairs.Clear();
            foreach (var item in App.db.Cars)
            {
                Cars.Add(item);
            }
            foreach (var item in App.db.Departures.Where(c => c.Status == "На выезде"))
            {
                Departures.Add(item);
            }
            foreach (var item in App.db.Repair)
            {
                Repairs.Add(item);
            }
        }

        public void CountCars()
        {
            CounterAll = Cars.Count;
            CounterPark = Cars.Where(c => c.Status == "В парке").Count() + Repairs.Count();
            CounterDeparture = Departures.Count();
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

        private Departure _selecetedDeparutreCar;

        public Departure SelectedDepartureCar
        {
            get { return _selecetedDeparutreCar; }
            set { 
                _selecetedDeparutreCar = value;
                OnPropertyChanged("SelectedDepartureCar");
            }
        }

        private Repair _selectedRepairCar;

        public Repair SelectedRepairCar
        {
            get { return _selectedRepairCar; }
            set { 
                _selectedRepairCar = value;
                OnPropertyChanged("SelectedRepairCar");
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
        public int CounterDeparture
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
                    ((DataGrid)obj).SetBinding(DataGrid.SelectedItemProperty, bindCars);
                    ((MainWindow)currentWindow).OnDepartureMenu.Visibility = Visibility.Collapsed;
                    ((MainWindow)currentWindow).OnParkMenu.Visibility = Visibility.Collapsed;
                    ((MainWindow)currentWindow).OnRepairMenu.Visibility = Visibility.Collapsed;
                    ((MainWindow)currentWindow).OnBoderMenu.Visibility = Visibility.Collapsed;
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
                    ((DataGrid)obj).SetBinding(DataGrid.SelectedItemProperty, bindCars);
                    ((MainWindow)currentWindow).OnDepartureMenu.Visibility = Visibility.Visible;
                    ((MainWindow)currentWindow).OnParkMenu.Visibility = Visibility.Collapsed;
                    ((MainWindow)currentWindow).OnRepairMenu.Visibility = Visibility.Visible;
                    ((MainWindow)currentWindow).OnBoderMenu.Visibility = Visibility.Visible;
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
                    
                    ((DataGrid)obj).ItemsSource = Departures;
                    ((DataGrid)obj).SetBinding(DataGrid.SelectedItemProperty, bindDeparures);
                    ((MainWindow)currentWindow).OnDepartureMenu.Visibility = Visibility.Collapsed;
                    ((MainWindow)currentWindow).OnParkMenu.Visibility = Visibility.Visible;
                    ((MainWindow)currentWindow).OnRepairMenu.Visibility = Visibility.Collapsed;
                    ((MainWindow)currentWindow).OnBoderMenu.Visibility = Visibility.Collapsed;
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
                    ((DataGrid)obj).SetBinding(DataGrid.SelectedItemProperty, bindReapir);
                    ((MainWindow)currentWindow).OnDepartureMenu.Visibility = Visibility.Collapsed;
                    ((MainWindow)currentWindow).OnParkMenu.Visibility = Visibility.Visible;
                    ((MainWindow)currentWindow).OnRepairMenu.Visibility = Visibility.Collapsed;
                    ((MainWindow)currentWindow).OnBoderMenu.Visibility = Visibility.Collapsed;
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
                    ((DataGrid)obj).SetBinding(DataGrid.SelectedItemProperty, bindCars);
                    ((MainWindow)currentWindow).OnDepartureMenu.Visibility = Visibility.Visible;
                    ((MainWindow)currentWindow).OnParkMenu.Visibility = Visibility.Collapsed;
                    ((MainWindow)currentWindow).OnRepairMenu.Visibility = Visibility.Visible;
                    ((MainWindow)currentWindow).OnBoderMenu.Visibility = Visibility.Collapsed;
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
                   DepartureWindow departureWindow = new DepartureWindow();
                   departureWindow.DataContext = new DepartureViewModel(SelectedCar);
                   if (departureWindow.ShowDialog() == true)
                   {
                       var carOnDeparture = Cars.FirstOrDefault(c => c.Number == SelectedCar.Number);
                       carOnDeparture.Status = "На выезде";
                       App.db.Cars.Update(carOnDeparture);
                       App.db.SaveChanges();
                       ((MainWindow)currentWindow).InfoBar.ShowMessage("Автомобиль успешно отправлен на выезд", InfoBarStatus.SUCCESS, 10000);
                       LoadCars();
                       CountCars();
                   }
                   else
                   {
                       ((MainWindow)currentWindow).InfoBar.ShowMessage("Действие отменено пользователем или из-за ошибки", InfoBarStatus.CAUTION, 10000);
                   }
                  
               }));
            }
        }

        private RelayCommand _onParkCommand;
        public RelayCommand OnParkCommand
        {
            get
            {
                return _onParkCommand ?? (_onParkCommand = new RelayCommand(obj =>
                {
                    if (SelectedDepartureCar == null && SelectedRepairCar == null)
                    {
                        ((MainWindow)currentWindow).InfoBar.ShowMessage("Для начала выберите машину", InfoBarStatus.ATTENTION, 7000);
                        return;
                    }
                    if (SelectedDepartureCar != null)
                    {
                        if (SelectedDepartureCar.Status == "В парке")
                        {
                            ((MainWindow)currentWindow).InfoBar.ShowMessage("Эта машина уже в парке", InfoBarStatus.ATTENTION, 7000);
                            return;
                        }
                    }
                    if (SelectedRepairCar != null)
                    {
                        if (SelectedRepairCar.Status == "В ремонте")
                        {
                            var repairedCar = Repairs.FirstOrDefault(c => c.Number == SelectedRepairCar.Number);
                            var car = App.db.Cars.FirstOrDefault(c => c.Number == SelectedRepairCar.Number);
                            car.Status = "В парке";
                            App.db.Repair.Remove(repairedCar);
                            App.db.Cars.Update(car);
                            App.db.SaveChanges();
                            ((MainWindow)currentWindow).InfoBar.ShowMessage("Автомобиль успешно отправлен из ремонта, в парк", InfoBarStatus.SUCCESS, 10000);
                            LoadCars();
                            CountCars();
                            return;
                        }
                       
                    }
                    ParkWindow parkWindow = new ParkWindow();
                    parkWindow.DataContext = new ParkViewModel(SelectedDepartureCar);
                    if (parkWindow.ShowDialog() == true)
                    {
                        var carOnPark = Cars.FirstOrDefault(c => c.Number == SelectedDepartureCar.Number);
                        carOnPark.Status = "В парке";
                        App.db.Cars.Update(carOnPark);
                        App.db.SaveChanges();
                        ((MainWindow)currentWindow).InfoBar.ShowMessage("Автомобиль успешно отправлен в парк", InfoBarStatus.SUCCESS, 10000);
                        LoadCars();
                        CountCars();
                    }
                    else
                    {
                        ((MainWindow)currentWindow).InfoBar.ShowMessage("Действие отменено пользователем или из-за ошибки", InfoBarStatus.CAUTION, 10000);
                    }
                }));
            }
        }

        private RelayCommand _onRepairCommand;
        public RelayCommand OnRepairCommand
        {
            get
            {
                return _onRepairCommand ?? (_onRepairCommand = new RelayCommand(obj =>
                {
                    if (SelectedCar == null)
                    {
                        ((MainWindow)currentWindow).InfoBar.ShowMessage("Для начала выберите автомобиль", InfoBarStatus.ATTENTION, 7000);
                        return;
                    }
                    else if (SelectedCar.Status == "В ремонте")
                    {
                        ((MainWindow)currentWindow).InfoBar.ShowMessage("Эта машина уже находится в ремонте", InfoBarStatus.ATTENTION, 8000);
                        return;
                    }
                    RepairWindow repairWindow = new RepairWindow();
                    repairWindow.DataContext = new RepairViewModel(SelectedCar);
                    if (repairWindow.ShowDialog() == true)
                    {
                        var carOnRepair = Cars.FirstOrDefault(c => c.Number == SelectedCar.Number);
                        carOnRepair.Status = "В ремонте";
                        App.db.Cars.Update(carOnRepair);
                        App.db.SaveChanges();
                        ((MainWindow)currentWindow).InfoBar.ShowMessage("Автомобиль успешно отправлен в ремонт", InfoBarStatus.SUCCESS, 10000);
                        LoadCars();
                        CountCars();
                    }
                    else
                    {
                        ((MainWindow)currentWindow).InfoBar.ShowMessage("Действие отменено пользователем или из-за ошибки", InfoBarStatus.CAUTION, 10000);
                    }
                }));
            }
        }

        private RelayCommand _onBorderCommand;
        public RelayCommand OnBorderCommand
        {
            get
            {
                return _onBorderCommand ?? (_onBorderCommand = new RelayCommand(obj =>
                {
                    if (SelectedCar == null)
                    {
                        ((MainWindow)currentWindow).InfoBar.ShowMessage("Для начала выберите автомобиль", InfoBarStatus.ATTENTION, 7000);
                        return;
                    }
                    else if (SelectedCar.Status.StartsWith("В распо."))
                    {
                        ((MainWindow)currentWindow).InfoBar.ShowMessage("Автомобиль уже находится на заставе", InfoBarStatus.ATTENTION, 7000);
                        return;
                    }
                    BorderWindow borderWindow = new BorderWindow();
                    borderWindow.DataContext = new BorderViewModel(SelectedCar);
                    if (borderWindow.ShowDialog() == true)
                    {
                        ((MainWindow)currentWindow).InfoBar.ShowMessage("Автомобиль успешно отправлен на заставу", InfoBarStatus.SUCCESS, 10000);
                        LoadCars();
                        CountCars();
                    }
                    else
                    {
                        ((MainWindow)currentWindow).InfoBar.ShowMessage("Действие отменено пользователем или из-за ошибки", InfoBarStatus.CAUTION, 10000);
                    }
                }));
            }
        }

        private RelayCommand _aboutProgCommand;

        public RelayCommand AboutProgCommand
        {
            get
            {
                return _aboutProgCommand ?? (_aboutProgCommand = new RelayCommand(obj =>
                {
                    About about = new About();
                    about.Show();
                }));
            }
        }

        private RelayCommand _openAdminCommand;

        public RelayCommand OpenAdminCommand
        {
            get
            {
                return _openAdminCommand ?? (_openAdminCommand = new RelayCommand(obj =>
                {
                    SecurityWindow securityWindow = new SecurityWindow();
                    securityWindow.DataContext = new SecurityViewModel(false);
                    securityWindow.Show();
                }));
            }
        }

        private RelayCommand _openAllDeparturesCommand;

        public RelayCommand OpenAllDeparuresCommand
        {
            get
            {
                return _openAllDeparturesCommand ?? (_openAllDeparturesCommand = new RelayCommand(obj =>
                {
                    AllDeparturesWindow allDepartures = new AllDeparturesWindow();
                    APC.Properties.Settings.Default.Save();
                    allDepartures.Show();
                }));
            }
        }

        private RelayCommand _changeThemeCommand;

        public RelayCommand ChangeThemeCommand
        {
            get
            {
                return _changeThemeCommand ?? (_changeThemeCommand = new RelayCommand(obj =>
                {
                    App.ChangeTheme(!APC.Properties.Settings.Default.ThemeSettings);
                }));
            }
        }



    }
}
