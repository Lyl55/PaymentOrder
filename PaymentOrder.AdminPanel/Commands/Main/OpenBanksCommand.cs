using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.AdminPanel.ViewModels.Controls;
using PaymentOrder.AdminPanel.ViewModels.Windows;
using PaymentOrder.AdminPanel.Views.Controls;
namespace PaymentOrder.AdminPanel.Commands.Main
{
    public class OpenBanksCommand : BaseCommand
    {
        private readonly MainWindowViewModel _viewModel;
        public OpenBanksCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.CenterGrid.Children.Clear();

            BanksControlViewModel banksControlViewModel = new BanksControlViewModel(Kernel.DB);

            banksControlViewModel.AllValues = _viewModel.DataProvider.GetBanks();

            banksControlViewModel.Initialize();

            BanksControl banksControl = new BanksControl();

            banksControlViewModel.ErrorDialog = banksControl.ErrorDialog;
            banksControl.DataContext = banksControlViewModel;

            _viewModel.CenterGrid.Children.Add(banksControl);
        }
    }
}
