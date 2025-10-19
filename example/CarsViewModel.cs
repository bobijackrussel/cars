using Rent_a_Car.core;
using Rent_a_Car.MVVM.ViewModel;

namespace Rent_a_Car.MVVM.ViewModel
{
    class CarsViewModel : ObservableObject
    {
        public ReleyCommand CarInfoViewCommand { get; set; }
      
        private object _currentView;

        public CarInfoViewModel CarInfoVM { get; set; }
       
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        static MainViewModel mm;
        public CarsViewModel(MainViewModel m)
        {
            mm = m;
            CarInfoVM = new CarInfoViewModel();
            CarInfoViewCommand = new ReleyCommand(o =>
            { m.CurrentView = CarInfoVM;
            });
        }
       public CarsViewModel() {
            CarInfoVM = new CarInfoViewModel();

            CarInfoViewCommand = new ReleyCommand(o =>
            {
                mm.CurrentView = CarInfoVM;
            });
        }
    }
}

