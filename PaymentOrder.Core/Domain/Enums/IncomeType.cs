using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.Core.Domain.Enums
{
    public enum IncomeType : byte
    {
        [Description("Əmək haqqı")]
        Salary = 1,
        [Description("Digər")]
        Other = 2
    }
}
