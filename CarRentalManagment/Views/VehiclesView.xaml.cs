using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CarRentalManagment.Utilities;
using CarRentalManagment.ViewModels;

namespace CarRentalManagment.Views
{
    public partial class VehiclesView : UserControl
    {
        private VehiclesViewModel? _viewModel;

        public VehiclesView()
        {
            InitializeComponent();

            // CRITICAL FIX: Set DataContext immediately in constructor
            try
            {
                _viewModel = AppServices.GetRequiredService<VehiclesViewModel>();
                DataContext = _viewModel;
                System.Diagnostics.Debug.WriteLine("? VehiclesView DataContext set to VehiclesViewModel");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"? ERROR setting DataContext: {ex.Message}");
                MessageBox.Show($"Failed to initialize Vehicles view: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("=== VehiclesView LOADED ===");

            if (_viewModel != null)
            {
                _viewModel.AddVehicleRequested += OnAddVehicleRequested;
                _viewModel.ReserveRequested += OnReserveRequested;

                await InitializeAsync(_viewModel);
            }
        }

        private async Task InitializeAsync(VehiclesViewModel viewModel)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Calling VehiclesViewModel.InitializeAsync...");
                await viewModel.InitializeAsync();
                System.Diagnostics.Debug.WriteLine($"? Initialization complete. Vehicles loaded: {viewModel.VehicleList.Vehicles.Count}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"? ERROR in InitializeAsync: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void OnAddVehicleRequested(object? sender, EventArgs e)
        {
            if (_viewModel == null)
            {
                return;
            }

            var dialog = CreateAddVehicleDialog();
            dialog.Owner = Window.GetWindow(this);

            if (dialog.DataContext is AddVehicleViewModel addVehicleViewModel)
            {
                void OnDialogClosed(object? _, DialogCloseRequestedEventArgs args)
                {
                    dialog.DialogResult = args.DialogResult;
                    dialog.Close();
                }

                addVehicleViewModel.CloseRequested += OnDialogClosed;
                dialog.Closed += (_, _) => addVehicleViewModel.CloseRequested -= OnDialogClosed;
            }

            var result = dialog.ShowDialog();
            if (result == true)
            {
                await _viewModel.ReloadVehiclesAsync();
            }
        }

        private async void OnReserveRequested(object? sender, VehicleCardViewModel? e)
        {
            if (_viewModel == null || e == null)
            {
                return;
            }

            var dialog = CreateReservationDialog();
            dialog.Owner = Window.GetWindow(this);

            if (dialog.DataContext is CreateReservationViewModel reservationViewModel)
            {
                await reservationViewModel.InitializeAsync(e);

                void OnDialogClosed(object? _, DialogCloseRequestedEventArgs args)
                {
                    dialog.DialogResult = args.DialogResult;
                    dialog.Close();
                }

                reservationViewModel.CloseRequested += OnDialogClosed;
                dialog.Closed += (_, _) => reservationViewModel.CloseRequested -= OnDialogClosed;
            }

            var result = dialog.ShowDialog();
            if (result == true)
            {
                MessageBox.Show($"Reservation created for {e.DisplayName}.", "Reservation Confirmed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private AddVehicleDialog CreateAddVehicleDialog()
        {
            var dialog = new AddVehicleDialog
            {
                DataContext = AppServices.GetRequiredService<AddVehicleViewModel>()
            };

            return dialog;
        }

        private CreateReservationDialog CreateReservationDialog()
        {
            return new CreateReservationDialog
            {
                DataContext = AppServices.GetRequiredService<CreateReservationViewModel>()
            };
        }
    }
}