using PaymentOrder.WebCore.Models;
using System.Collections.Generic;

namespace PaymentOrder.WebAdminPanel.Models
{
    public class BankViewModel
    {
        public List<BankModel> Banks { get; set; }
        public BankModel Deleted { get; set; }
    }
}
