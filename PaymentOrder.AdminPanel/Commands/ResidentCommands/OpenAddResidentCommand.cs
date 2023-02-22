using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.AdminPanel.ViewModels.Controls;
using PaymentOrder.AdminPanel.ViewModels.Windows;
using PaymentOrder.AdminPanel.Views.Windows;

namespace PaymentOrder.AdminPanel.Commands.ResidentCommands
{
    public class OpenAddResidentCommand : BaseCommand
    {
        private readonly ResidentsControlViewModel _viewModel;

        public OpenAddResidentCommand(ResidentsControlViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            AddResidentWindow window = new AddResidentWindow();
            var viewModel = new AddResidentViewModel(_viewModel.CurrentValue, window, Kernel.DB);
            window.DataContext = viewModel;
            window.ShowDialog();

            _viewModel.AllValues = _viewModel.DataProvider.GetResidents();
            _viewModel.Initialize();
        }
    }
}