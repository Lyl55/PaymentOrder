using PaymentOrder.AdminPanel.Infrastructure;
using PaymentOrder.Core.Domain.Abstract;
using System.ComponentModel;

namespace PaymentOrder.AdminPanel.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}