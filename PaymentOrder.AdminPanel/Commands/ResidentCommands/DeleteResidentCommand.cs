using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentOrder.AdminPanel.Utils;
using PaymentOrder.AdminPanel.ViewModels.Controls;

namespace PaymentOrder.AdminPanel.Commands.ResidentCommands
{
    public class DeleteResidentCommand:BaseCommand
    {
        private readonly ResidentsControlViewModel _viewModel;

        public DeleteResidentCommand(ResidentsControlViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            if (CommonFunctions.MakeDeleteSure() == false)
                return;
            var resident = _viewModel.DB.ResidentRepository.Get(_viewModel.CurrentValue.Id);
            resident.IsDeleted = true;
            _viewModel.DB.ResidentRepository.Update(resident);
            _viewModel.Message = Constants.OperationSuccessMessage;
            DoAnimation(_viewModel.ErrorDialog);

            _viewModel.AllValues = _viewModel.DataProvider.GetResidents();

            _viewModel.Initialize();
        }
    }
}
