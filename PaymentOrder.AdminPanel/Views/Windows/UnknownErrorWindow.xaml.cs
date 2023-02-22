using System;
using System.Timers;
using System.Windows;
using System.Windows.Media.Animation;

namespace PaymentOrder.AdminPanel.Views.Windows
{
    /// <summary>
    /// Interaction logic for UnknownErrorWindow.xaml
    /// </summary>
    public partial class UnknownErrorWindow : Window
    {
        public UnknownErrorWindow(double width, double height, string errorMessage)
        {
            Width = width;
            Height = height;

            WindowStyle = WindowStyle.None;

            InitializeComponent();
            
            txtErrorMessage.Text = errorMessage;

            StartClosingAnimation();
        }

        private void StartClosingAnimation()
        {
            var anim = new DoubleAnimation(0, TimeSpan.FromSeconds(3));

            anim.Completed += (s, _) => Close();

            BeginAnimation(OpacityProperty, anim);
        }
    }
}
