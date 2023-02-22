using PaymentOrder.AdminPanel.Enums;
using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.AdminPanel.Mappers;
using PaymentOrder.AdminPanel.Utils;
using PaymentOrder.AdminPanel.ViewModels.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.AdminPanel.Commands.BankBranchs
{
    public class SaveBankBranchCommand : BaseCommand
    {
        private readonly BankBranchsControlViewModel _viewModel;
        
        public SaveBankBranchCommand(BankBranchsControlViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            switch (_viewModel.CurrentSituation)
            {
                case (int)Situations.NORMAL:
                    _viewModel.CurrentSituation = (int)Situations.ADD;
                    break;
                case (int)Situations.SELECTED:
                    _viewModel.CurrentSituation = (int)Situations.EDIT;
                    break;
                case (int)Situations.ADD:
                case (int)Situations.EDIT:
                    {
                        Save();
                        break;
                    }
            }
        }

        private void Save()
        {
            DataValidator dataValidator = new DataValidator(_viewModel.DB);

            _viewModel.CurrentValue.Bank = _viewModel.SelectedBank;
            if (dataValidator.IsBranchValid(_viewModel.CurrentValue, out string message) == false)
            {
                _viewModel.Message = message;
                DoAnimation(_viewModel.ErrorDialog);
                return;
            }

            BankBranchMapper map = new BankBranchMapper();

            _viewModel.CurrentValue.Bank = _viewModel.SelectedBank;

            var entity = map.Map(_viewModel.CurrentValue);

            entity.ModifiedDate = DateTime.Now;
            entity.CreatorId = Kernel.CurrentUser.Id;
            entity.Bank.Id = _viewModel.CurrentValue.Bank.Id;
            if (entity.Id != 0)
            {
                _viewModel.DB.BankBranchRepository.Update(entity);
            }
            else
            {
                _viewModel.DB.BankBranchRepository.Add(entity);
            }

            _viewModel.Message = Constants.OperationSuccessMessage;
            DoAnimation(_viewModel.ErrorDialog);

            _viewModel.AllValues = _viewModel.DataProvider.GetBankBranchs();

            _viewModel.Initialize();
        }
    }
}
