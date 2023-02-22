using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.AdminPanel.Utils;
using PaymentOrder.AdminPanel.ViewModels.Controls;

namespace PaymentOrder.AdminPanel.Commands.BankBranchs
{
    public class DeleteBankBranchCommand:BaseCommand
    {
        private readonly BankBranchsControlViewModel _viewModel;

        public DeleteBankBranchCommand(BankBranchsControlViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public override void Execute(object parameter)
        {
            if (CommonFunctions.MakeDeleteSure() == false)
                return;

            var bankBranch = _viewModel.DB.BankBranchRepository.Get(_viewModel.CurrentValue.Id);

            bankBranch.ModifiedDate = DateTime.Now;
            bankBranch.CreatorId = Kernel.CurrentUser.Id;
            bankBranch.IsDeleted = true;

            _viewModel.DB.BankBranchRepository.Update(bankBranch);

            _viewModel.Message = Constants.OperationSuccessMessage;
            DoAnimation(_viewModel.ErrorDialog);

            _viewModel.AllValues = _viewModel.DataProvider.GetBankBranchs();

            _viewModel.Initialize();
        }
    }
}
