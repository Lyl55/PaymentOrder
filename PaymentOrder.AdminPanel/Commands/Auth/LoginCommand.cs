using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.AdminPanel.ViewModels;
using PaymentOrder.AdminPanel.ViewModels.Windows;
using PaymentOrder.AdminPanel.Views.Windows;
using PaymentOrder.Core.Domain.Entities;
using PaymentOrder.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PaymentOrder.AdminPanel.Commands.Auth
{
    public class LoginCommand : BaseCommand
    {
        private readonly LoginViewModel viewModel;
        public LoginCommand(LoginViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;

            if (passwordBox == null)
                return;

            User user = viewModel.DB.UserRepository.Get(viewModel.Email);

            if(user == null)
            {
                viewModel.LoginErrorVisibility = Visibility.Visible;
                return;
            }

            string password = passwordBox.Password;

            string passwordHash = SecurityUtil.ComputeSha256Hash(password);

            if(user.PasswordHash == passwordHash)
            {
                Kernel.CurrentUser = user;

                MainWindow mainWindow = new MainWindow();
                MainWindowViewModel mainViewModel = new MainWindowViewModel(Kernel.DB, mainWindow);

                mainViewModel.CenterGrid = mainWindow.grdCenter;
                mainWindow.DataContext = mainViewModel;

                viewModel.Window.Close();
                mainWindow.Show();
            }
            else
            {
                viewModel.LoginErrorVisibility = Visibility.Visible;
                return;
            }
        }
    }
}
