using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.AdminPanel.ViewModels.Controls;
using PaymentOrder.AdminPanel.ViewModels.Windows;
using PaymentOrder.AdminPanel.Views.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.AdminPanel.Commands.Main
{
    public class OpenBankBranchsCommand : BaseCommand
    {
        private readonly MainWindowViewModel _viewModel;
        public OpenBankBranchsCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.CenterGrid.Children.Clear();

            BankBranchsControlViewModel banksControlViewModel = new BankBranchsControlViewModel(Kernel.DB);

            banksControlViewModel.AllValues = _viewModel.DataProvider.GetBankBranchs();
            banksControlViewModel.Banks = _viewModel.DataProvider.GetBanks();

            banksControlViewModel.Initialize();

            BankBranchsControl banksControl = new BankBranchsControl();

            banksControlViewModel.ErrorDialog = banksControl.ErrorDialog;
            banksControl.DataContext = banksControlViewModel;

            _viewModel.CenterGrid.Children.Add(banksControl);
        }
    }
}
