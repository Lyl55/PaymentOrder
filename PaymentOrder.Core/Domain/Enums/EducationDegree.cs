using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.Core.Domain.Enums
{
    public enum EducationDegree : byte
    {
        [Description("Ali təhsil-Magistr")]
        HigherEducationMaster = 1,
        [Description("Orta təhsil")]
        SecondaryEducation = 2,
        [Description("Ali təhsil-Doktorantura")]
        HigherEducationDoctoral = 3
    }
}
