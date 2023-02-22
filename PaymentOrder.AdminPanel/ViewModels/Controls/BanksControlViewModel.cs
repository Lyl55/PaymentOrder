using PaymentOrder.AdminPanel.Commands.BankCommands;
using PaymentOrder.AdminPanel.Commands.ResidentCommands;
using PaymentOrder.AdminPanel.Models;
using PaymentOrder.Core.Domain.Abstract;
using System.Collections.ObjectModel;
using System.Linq;

namespace PaymentOrder.AdminPanel.ViewModels.Controls
{
    public class BanksControlViewModel : BaseControlViewModel<BankModel>
    {
        public BanksControlViewModel(IUnitOfWork db) : base(db)
        {

        }

        public SaveBankCommand Save => new SaveBankCommand(this);
        public DeleteBankCommand Delete => new DeleteBankCommand(this);

        #region properties

        public override string Header => "Banks";

        #endregion

        #region methods

        public override void OnSearch()
        {
            var models = AllValues.Where(x => IsCompatibleWithFilter(x.Name) ||
                                           IsCompatibleWithFilter(x.VOEN)  ||
                                           IsCompatibleWithFilter(x.CorrespondentAccount) ||
                                           IsCompatibleWithFilter(x.SWIFT));

            Values = new ObservableCollection<BankModel>(models);
        }

        #endregion
    }
}