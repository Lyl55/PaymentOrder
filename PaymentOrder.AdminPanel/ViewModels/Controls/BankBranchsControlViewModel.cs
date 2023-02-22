using PaymentOrder.AdminPanel.Commands.BankBranchs;
using PaymentOrder.AdminPanel.Models;
using PaymentOrder.Core.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.AdminPanel.ViewModels.Controls
{
    public class BankBranchsControlViewModel : BaseControlViewModel<BankBranchModel>
    {
        public BankBranchsControlViewModel(IUnitOfWork db) : base(db)
        {

        }

        public SaveBankBranchCommand Save => new SaveBankBranchCommand(this);
        public DeleteBankBranchCommand Delete => new DeleteBankBranchCommand(this);

        #region properties

        public override string Header => "Bank Branches";

        public List<BankModel> Banks { get; set; }

        private BankModel _selectedBank;
        public BankModel SelectedBank
        {
            get => _selectedBank;
            set
            {
                _selectedBank = value;
                OnPropertyChanged(nameof(SelectedBank));
            }
        }

        #endregion

        #region methods

        public override void OnSearch()
        {
            var models = AllValues.Where(x => IsCompatibleWithFilter(x.Name) ||
                                              IsCompatibleWithFilter(x.Address) || IsCompatibleWithFilter(x.Fax) ||
                                              IsCompatibleWithFilter(x.Phone) ||
                                              IsCompatibleWithFilter(x.Bank.ToString())|| IsCompatibleWithFilter(x.Bank.Name));
            Values = new ObservableCollection<BankBranchModel>(models);
        }

        #endregion
    }
}
