using PaymentOrder.AdminPanel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.AdminPanel.Models
{
    public class BankBranchModel : BaseModel
    {
        public BankModel Bank { get; set; } = new BankModel();
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }

        public override object Clone()
        {
            return new BankBranchModel()
            {
                Id = Id,
                Name = Name,
                No = No,
                Address = Address,
                Fax = Fax,
                Phone = Phone,
                Bank = Bank.Clone() as BankModel
            };
        }

        public override bool IsValid(out string message)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                message = ValidationHelper.GetRequiredMessage("Ad");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Phone))
            {
                message = ValidationHelper.GetRequiredMessage("Telefon nömrəsi");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Fax))
            {
                message = ValidationHelper.GetRequiredMessage("Faks");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Address))
            {
                message = ValidationHelper.GetRequiredMessage("Ünvan");
                return false;
            }

            message = string.Empty;

            return true;
        }

        public override string ToExcelString()
        {
            return $"{Bank?.Name} {Name}";
        }
    }
}
