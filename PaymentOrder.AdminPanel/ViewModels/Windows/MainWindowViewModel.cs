using PaymentOrder.AdminPanel.Commands.Main;
using PaymentOrder.Core.Domain.Abstract;
using System.Windows;
using System.Windows.Controls;

namespace PaymentOrder.AdminPanel.ViewModels.Windows
{
    public class MainWindowViewModel : BaseWindowViewModel
    {
        public MainWindowViewModel(IUnitOfWork db, Window window) : base(db, window)
        {

        }

        public Grid CenterGrid { get; set; }

        public OpenBanksCommand OpenBanks => new OpenBanksCommand(this);
        public OpenBankBranchsCommand OpenBankBranchs => new OpenBankBranchsCommand(this);
        public OpenResidentsCommand OpenResidents => new OpenResidentsCommand(this);
    }
}
