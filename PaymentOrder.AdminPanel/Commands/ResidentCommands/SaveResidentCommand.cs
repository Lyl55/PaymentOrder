using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.AdminPanel.Mappers;
using PaymentOrder.AdminPanel.Utils;
using PaymentOrder.AdminPanel.ViewModels.Windows;
using System;
using System.Windows;

namespace PaymentOrder.AdminPanel.Commands.ResidentCommands
{
    public class SaveResidentCommand : BaseCommand
    {
        private readonly AddResidentViewModel _viewModel;

        public SaveResidentCommand(AddResidentViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            Save();
        }

        private void Save()
        {
            DataValidator dataValidator = new DataValidator(_viewModel.DB);

            try
            {
                ResidentMapper residentMapper = new ResidentMapper();
                var resident = residentMapper.Map(_viewModel.CurrentValue);
            
                if (dataValidator.IsResidentValid(_viewModel.CurrentValue, out string message) == false)
                {
                    MessageBox.Show(message, "Validasiya xətası", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (resident.Id != 0)
                {
                    _viewModel.DB.ResidentRepository.Update(resident);
                }
                else
                {
                    _viewModel.DB.ResidentRepository.Add(resident);
                }

                MessageBox.Show(Constants.OperationSuccessMessage, "Uğurlu!", MessageBoxButton.OK, MessageBoxImage.Error);

                _viewModel.Window.Close();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}