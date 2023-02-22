using System.Collections.Generic;
using PaymentOrder.AdminPanel.Commands.ResidentCommands;
using PaymentOrder.AdminPanel.Enums;
using PaymentOrder.AdminPanel.Models;
using PaymentOrder.Core.Domain.Abstract;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PaymentOrder.AdminPanel.Commands;

namespace PaymentOrder.AdminPanel.ViewModels.Controls
{
    public class ResidentsControlViewModel : BaseControlViewModel<ResidentModel>
    {
        public ResidentsControlViewModel(IUnitOfWork db) : base(db)
        {

        }

        #region commands

        public ICommand Save => new OpenAddResidentCommand(this);
        public ICommand Delete => new DeleteResidentCommand(this);
        public ICommand Unselect => new RejectCommand<ResidentModel>(this);

        #endregion

        public override string Header => "Residents";

        public override void OnSearch()
        {
            var models = AllValues.Where(x => IsCompatibleWithFilter(x.FirstName) ||
                                              IsCompatibleWithFilter(x.LastName) ||
                                              IsCompatibleWithFilter(x.FatherName) ||
                                              IsCompatibleWithFilter(x.DocumentType.ToString()) ||
                                              IsCompatibleWithFilter(x.SerialNumber) ||
                                              IsCompatibleWithFilter(x.FIN) ||
                                              IsCompatibleWithFilter(x.Authority) ||
                                              IsCompatibleWithFilter(x.GivingDate.ToString()) ||
                                              IsCompatibleWithFilter(x.ReliabilityDate.ToString()) ||
                                              IsCompatibleWithFilter(x.MartialStatus.ToString()) ||
                                              IsCompatibleWithFilter(x.Gender.ToString()) ||
                                              IsCompatibleWithFilter(x.BirthDate.ToString()) ||
                                              IsCompatibleWithFilter(x.BirthCountry) ||
                                              IsCompatibleWithFilter(x.RegistrationAddressCountry) ||
                                              IsCompatibleWithFilter(x.RegistrationAddressCity) ||
                                              IsCompatibleWithFilter(x.RegistrationAddressStreet) ||
                                              IsCompatibleWithFilter(x.MailingAddress1) ||
                                              IsCompatibleWithFilter(x.Citizenship) ||
                                              IsCompatibleWithFilter(x.PhoneNumber) ||
                                              IsCompatibleWithFilter(x.Email) ||
                                              IsCompatibleWithFilter(x.ActualAddressCountry) ||
                                              IsCompatibleWithFilter(x.ActualAddressCity) ||
                                              IsCompatibleWithFilter(x.ActualAddressStreet) ||
                                              IsCompatibleWithFilter(x.MailingAddress2) ||
                                              IsCompatibleWithFilter(x.MonthlySalary.ToString()) ||
                                              IsCompatibleWithFilter(x.IncomeSource.ToString()) ||
                                              IsCompatibleWithFilter(x.Education.ToString()) ||
                                              IsCompatibleWithFilter(x.GUARDIAN) ||
                                              IsCompatibleWithFilter(x.NATIONID) ||
                                              IsCompatibleWithFilter(x.IsConviction.ToString()) ||
                                              IsCompatibleWithFilter(x.Currency.ToString()) ||
                                              IsCompatibleWithFilter(x.Code));
            Values = new ObservableCollection<ResidentModel>(models);
        }
    }
}