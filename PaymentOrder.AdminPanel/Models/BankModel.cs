using PaymentOrder.AdminPanel.Attributes;
using PaymentOrder.AdminPanel.Utils;

namespace PaymentOrder.AdminPanel.Models
{
    public class BankModel : BaseModel
    {
        [ExcelDisplay(ColumnNo = 1, Name = "Ad")]
        public string Name { get; set; }

        [ExcelDisplay(ColumnNo = 2, Name = "VÖEN")]
        public string VOEN { get; set; }
        
        [ExcelDisplay(ColumnNo = 3, Name = "Hesab nömrəsi")]
        public string CorrespondentAccount { get; set; }

        [ExcelDisplay(ColumnNo = 4, Name = "SWIFT")]
        public string SWIFT { get; set; }

        public override bool IsValid(out string message)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                message = ValidationHelper.GetRequiredMessage("Ad");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(VOEN))
            {
                message = ValidationHelper.GetRequiredMessage("VÖEN");
                return false;
            }
            
            if (VOEN.Length != 10)
            {
                message = ValidationHelper.GetRequiredLength("VÖEN", 10);
                return false;
            }

            if (string.IsNullOrWhiteSpace(CorrespondentAccount))
            {
                message = ValidationHelper.GetRequiredMessage("Hesab nömrəsi");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(SWIFT))
            {
                message = ValidationHelper.GetRequiredMessage("SWIFT");
                return false;
            }
            
            message = string.Empty;

            return true;
        }

        public override object Clone()
        {
            return new BankModel()
            {
                Id = Id,
                Name = Name,
                CorrespondentAccount = CorrespondentAccount,
                No = No,
                SWIFT = SWIFT,
                VOEN = VOEN
            };
        }

        public override string ToExcelString()
        {
            return Name;
        }
    }
}
