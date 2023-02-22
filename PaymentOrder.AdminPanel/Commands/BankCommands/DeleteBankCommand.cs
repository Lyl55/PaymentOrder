using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.AdminPanel.Utils;
using PaymentOrder.AdminPanel.ViewModels.Controls;
using System;

namespace PaymentOrder.AdminPanel.Commands.BankCommands
{
    public class DeleteBankCommand : BaseCommand
    {
        private readonly BanksControlViewModel _viewModel;
        public DeleteBankCommand(BanksControlViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (CommonFunctions.MakeDeleteSure() == false)
                return;

            var currentBank = _viewModel.DB.BankRepository.Get(_viewModel.CurrentValue.Id);

            currentBank.ModifiedDate = DateTime.Now;
            currentBank.CreatorId = Kernel.CurrentUser.Id;
            currentBank.IsDeleted = true;

            _viewModel.DB.BankRepository.Update(currentBank);

            _viewModel.Message = Constants.OperationSuccessMessage;
            DoAnimation(_viewModel.ErrorDialog);

            _viewModel.AllValues = _viewModel.DataProvider.GetBanks();

            _viewModel.Initialize();
        }
    }
}
