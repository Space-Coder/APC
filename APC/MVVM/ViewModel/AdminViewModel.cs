using APC.Command;
using APC.MVVM.Model;
using APC.MVVM.View;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace APC.MVVM.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        private Window currentWindow;
        private Window mainWindow;
        public AdminViewModel()
        {
            foreach (Window item in App.Current.Windows)
            {
                if (item is AdminWindow)
                {
                    currentWindow = item;
                }
                if (item is MainWindow)
                {
                    mainWindow = item;
                }
            }
            App.db.Cars.Load();
            App.db.Departures.Load();
            App.db.Repair.Load();
        }

        private RelayCommand _loadCarsCommand;

        public RelayCommand LoadCarsCommand
        {
            get
            {
                return _loadCarsCommand ?? (_loadCarsCommand = new RelayCommand(obj =>
                {
                    ((DataGrid)obj).ItemsSource = App.db.Cars.Local.ToBindingList();
                }));
            }
        }

        private RelayCommand _loadDeparturesCommand;

        public RelayCommand LoadDeparturesCommand
        {
            get
            {
                return _loadDeparturesCommand ?? (_loadDeparturesCommand = new RelayCommand(obj =>
               {
                   ((DataGrid)obj).ItemsSource = App.db.Departures.Local.ToBindingList();
               }));
            }
        }

        private RelayCommand _loadReapirCommad;

        public RelayCommand LoadRepairCommand
        {
            get
            {
                return _loadReapirCommad ?? (_loadReapirCommad = new RelayCommand(obj =>
                {
                    ((DataGrid)obj).ItemsSource = App.db.Repair.Local.ToBindingList();
                }));
            }
        }

        private RelayCommand _saveChangesCommand;

        public RelayCommand SaveChangesCommand
        {
            get
            {
                return _saveChangesCommand ?? (_saveChangesCommand = new RelayCommand(obj =>
                {
                    App.db.SaveChanges();
                    (((MainWindow)mainWindow).DataContext as CarsViewModel).LoadCars();
                    (((MainWindow)mainWindow).DataContext as CarsViewModel).CountCars();
                    ((AdminWindow)currentWindow).InfoBarAdmin.ShowMessage("Данные успешно сохранены", InfoBarStatus.SUCCESS, 7000);
                }));
            }
        }


    }
}
