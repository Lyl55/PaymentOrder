using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.Core.Domain.Enums
{
    public enum GenderType : byte
    {
        [Description("Qadın")]
        Female = 1,
        [Description("Kişi")]
        Male = 2
    }
}
