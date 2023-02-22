using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PaymentOrder.AdminPanel.Views.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ShowPasswordChecked(object sender, RoutedEventArgs e)
        {
            passwordTxtBox.Text = passwordTxt.Password;
            passwordTxt.Visibility = Visibility.Collapsed;
            passwordTxtBox.Visibility = Visibility.Visible;
        }

        private void ShowPasswordUnchecked(object sender, RoutedEventArgs e)
        {
            passwordTxt.Password = passwordTxtBox.Text;
            passwordTxtBox.Visibility = Visibility.Collapsed;
            passwordTxt.Visibility = Visibility.Visible;
        }

        private void btnSignInClick(object sender, RoutedEventArgs e)
        {
            if(showPassword.IsChecked == true)
            {
                passwordTxt.Password = passwordTxtBox.Text;
            }
        }
    }
}
