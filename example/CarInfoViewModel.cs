using Rent_a_Car.core;
using Rent_a_Car.MVVM.View;
using Rent_a_Car.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Rent_a_Car.MVVM.ViewModel
{
        class CarInfoViewModel  : ObservableObject
        {
            public ReleyCommand CarsEquipmentViewCommand { get; set; }
            public ReleyCommand CarsSpecificationViewCommand { get; set; }
            public ReleyCommand CarsExperienceViewCommand { get; set; }
            public ReleyCommand CarsReservationViewCommand { get; set; }
            public ReleyCommand CarsDetailsViewCommand { get; set; }

        private object _currentInfoView;

            public CarsEquipmentViewModel CarsEquipmentVM { get; set; }
            public CarsSpecificationViewModel CarsSpecificationVM { get; set; }
            public CarsExperienceViewModel CarsExperienceVM { get; set; }
            public CarsReservationViewModel CarsReservationVM { get; set; }
            public CarDetailsVeiwModel CarsDetailsVM { get; set; }

        public object CurrentInfoView
            {
                get { return _currentInfoView; }
                set
                {
                    _currentInfoView = value;
                    OnPropertyChanged();
                }
            }

            public CarInfoViewModel()
            {
                CarsEquipmentVM = new CarsEquipmentViewModel();
                CarsSpecificationVM = new CarsSpecificationViewModel();
                CarsExperienceVM = new CarsExperienceViewModel();
                CarsReservationVM = new CarsReservationViewModel();
                CarsDetailsVM = new CarDetailsVeiwModel();

                CurrentInfoView = CarsDetailsVM;

                CarsEquipmentViewCommand = new ReleyCommand(o =>     { CurrentInfoView = CarsEquipmentVM; });
                CarsSpecificationViewCommand = new ReleyCommand(o => { CurrentInfoView = CarsSpecificationVM; });
                CarsExperienceViewCommand = new ReleyCommand(o =>  { CurrentInfoView = CarsExperienceVM; });
                CarsReservationViewCommand = new ReleyCommand(o => { CurrentInfoView = CarsReservationVM; });
                CarsDetailsViewCommand = new ReleyCommand(o =>     { CurrentInfoView = CarsDetailsVM; });

        }
    }
}

