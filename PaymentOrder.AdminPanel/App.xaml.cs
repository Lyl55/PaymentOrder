using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.AdminPanel.Utils;
using PaymentOrder.AdminPanel.ViewModels.Windows;
using PaymentOrder.AdminPanel.Views.Windows;
using PaymentOrder.Core.Factories;
using System.Configuration;
using System.Windows;
using System.Windows.Threading;

namespace PaymentOrder.AdminPanel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Dispatcher.UnhandledException += HandleException;

            var connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            Kernel.DB = DbFactory.Create(connectionString);

            LoginWindow loginWindow = new LoginWindow();

            LoginViewModel loginViewModel = new LoginViewModel(loginWindow, Kernel.DB);

            loginWindow.DataContext = loginViewModel;

            loginWindow.Show();
        }

        public void HandleException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Log(e.Exception);

            UnknownErrorWindow error = new UnknownErrorWindow(SystemParameters.WorkArea.Width * 0.2, SystemParameters.WorkArea.Height * 0.2, Constants.ErrorMessage);
            error.ShowDialog();

            e.Handled = true;
        }
    }
}