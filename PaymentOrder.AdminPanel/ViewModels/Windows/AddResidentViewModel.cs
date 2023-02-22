using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PaymentOrder.AdminPanel.Commands.ResidentCommands;
using PaymentOrder.AdminPanel.Models;
using PaymentOrder.AdminPanel.ViewModels.Controls;
using PaymentOrder.Core.Domain.Abstract;

namespace PaymentOrder.AdminPanel.ViewModels.Windows
{
    public class AddResidentViewModel : BaseWindowViewModel
    {
        public AddResidentViewModel(ResidentModel currentValue, Window window, IUnitOfWork db) : base(db, window)
        {
            CurrentValue = currentValue;
        }

        public ICommand Add => new SaveResidentCommand(this);

        public ResidentModel CurrentValue { get; }
    }
}
