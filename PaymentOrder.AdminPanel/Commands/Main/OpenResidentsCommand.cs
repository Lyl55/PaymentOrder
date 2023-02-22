using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Printing;
using System.Windows.Documents;
using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.AdminPanel.Models;
using PaymentOrder.AdminPanel.ViewModels.Controls;
using PaymentOrder.AdminPanel.ViewModels.Windows;
using PaymentOrder.AdminPanel.Views.Controls;

namespace PaymentOrder.AdminPanel.Commands.Main
{
    public class OpenResidentsCommand:BaseCommand
    {
        private readonly MainWindowViewModel _viewModel;
        public OpenResidentsCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            _viewModel.CenterGrid.Children.Clear();

            ResidentsControlViewModel residentControlViewModel = new ResidentsControlViewModel(Kernel.DB);
            residentControlViewModel.AllValues = _viewModel.DataProvider.GetResidents();
            residentControlViewModel.Initialize();
            ResidentsControl residentsControl = new ResidentsControl();
            residentControlViewModel.ErrorDialog = residentsControl.ErrorDialog;
            residentsControl.DataContext = residentControlViewModel;
            _viewModel.CenterGrid.Children.Add(residentsControl);
        }
    }
}
