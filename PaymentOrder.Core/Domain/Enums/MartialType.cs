using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.Core.Domain.Enums
{
    public enum MartialType : byte
    {
        [Description("Subay")]
        Single = 1,
        [Description("Evli")]
        Married = 2
    }
}
