using Rent_a_Car.core;
using Rent_a_Car.MVVM.ViewModel;

namespace Rent_a_Car.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ReleyCommand CarsViewCommand { get; set; }
        public ReleyCommand SettingsViewCommand { get; set; }
        public ReleyCommand ActivityFeedViewCommand { get; set; }
        public ReleyCommand LogOutViewCommand { get; set; }
        public ReleyCommand ProfileViewCommand { get; set; }


        private static object _currentView;
        private object _tbT;
        private object _overlayView;
        private bool _isOverlayVisible;

        public CarsViewModel CarsVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }
        public LogOutViewModel LogOutVM { get; set; }
        public ActivityFeedViewModel ActivityFeedVM { get; set; }
        
        public ProfileViewModel ProfileVM { get; set; }
        public  object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public object OverlayView
        {
            get => _overlayView;
            set
            {
                _overlayView = value;
                OnPropertyChanged();
                IsOverlayVisible = value != null; 
            }
        }

        public bool IsOverlayVisible
        {
            get => _isOverlayVisible;
            set
            {
                _isOverlayVisible = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            CarsVM = new CarsViewModel(this);
            SettingsVM = new SettingsViewModel();
            LogOutVM = new LogOutViewModel();
            ActivityFeedVM = new ActivityFeedViewModel();
            ProfileVM = new ProfileViewModel();

            LogOutVM.NoButtonClicked += OnNoButtonClicked;


            CurrentView = CarsVM;

            CarsViewCommand = new ReleyCommand(o => { CurrentView = CarsVM; });
            SettingsViewCommand = new ReleyCommand(o => { CurrentView = SettingsVM; });
            LogOutViewCommand = new ReleyCommand(o => { OverlayView = LogOutVM; });
            ActivityFeedViewCommand = new ReleyCommand(o => { CurrentView = ActivityFeedVM; });
            ProfileViewCommand = new ReleyCommand(o => { CurrentView = ProfileVM; });
        }
        private void OnNoButtonClicked()
        {
            OverlayView = null; // Zatvaranje Overlay-a
        }
    }
}