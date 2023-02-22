using PaymentOrder.AdminPanel.Commands.Auth;
using PaymentOrder.AdminPanel.Views.Windows;
using PaymentOrder.Core.Domain.Abstract;
using System.Windows;

namespace PaymentOrder.AdminPanel.ViewModels.Windows
{
    public class LoginViewModel : BaseWindowViewModel
    {
        public LoginViewModel(LoginWindow loginWindow, IUnitOfWork db) : base(db, loginWindow)
        {
        }

        public LoginCommand SignIn => new LoginCommand(this);
        public string Email { get; set; } = "javidaliev.dev@gmail.com";

        private Visibility loginErrorVisibility = Visibility.Collapsed;
        public Visibility LoginErrorVisibility
        {
            get
            {
                return loginErrorVisibility;
            }
            set
            {
                loginErrorVisibility = value;
                OnPropertyChanged(nameof(LoginErrorVisibility));
            }
        }
    }
}
