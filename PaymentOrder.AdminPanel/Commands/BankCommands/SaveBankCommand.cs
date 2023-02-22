using PaymentOrder.AdminPanel.Enums;
using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.AdminPanel.Mappers;
using PaymentOrder.AdminPanel.Utils;
using PaymentOrder.AdminPanel.ViewModels.Controls;
using PaymentOrder.AdminPanel.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PaymentOrder.AdminPanel.Commands.BankCommands
{
    public class SaveBankCommand : BaseCommand
    {
        private readonly BanksControlViewModel _viewModel;
        public SaveBankCommand(BanksControlViewModel viewModel)
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

            if (dataValidator.IsBankValid(_viewModel.CurrentValue, out string message) == false)
            {
                _viewModel.Message = message;
                DoAnimation(_viewModel.ErrorDialog);
                return;
            }

            BankMapper bankMapper = new BankMapper();

            var bank = bankMapper.Map(_viewModel.CurrentValue);

            bank.ModifiedDate = DateTime.Now;
            bank.CreatorId = Kernel.CurrentUser.Id;

            if (bank.Id != 0)
            {
                _viewModel.DB.BankRepository.Update(bank);
            }
            else
            {
                _viewModel.DB.BankRepository.Add(bank);
            }

            _viewModel.Message = Constants.OperationSuccessMessage;
            DoAnimation(_viewModel.ErrorDialog);

            _viewModel.AllValues = _viewModel.DataProvider.GetBanks();

            _viewModel.Initialize();
        }
    }
}