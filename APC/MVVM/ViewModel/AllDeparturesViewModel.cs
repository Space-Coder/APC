using APC.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.MVVM.ViewModel
{
    public class AllDeparturesViewModel : BaseViewModel
    {
        private ObservableCollection<Departure> _departures;

        public ObservableCollection<Departure> Departures
        {
            get {
                if (_departures == null)
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



        public AllDeparturesViewModel()
        {
            foreach (var item in App.db.Departures)
            {
                Departures.Add(item);
            }
        }
    }
}
