using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.Core.Domain.Enums
{
    public enum DocumentType : byte
    {
        [Description("Şəxsiyyət vəsiqəsi")]
        IDCard = 1,
        Passport = 2,
        [Description("Hərbi bilet")]
        MilitaryTicket = 3,
        [Description("DYİ")]
        ResidencePermit = 4,
        [Description("Yeni Şəxsiyyət vəsiqəsi")]
        NewIDCard = 5,
        UNHCR = 6,
        [Description("TR Şəxsiyyət vəsiqəsi")]
        TRIdCard = 7,
        [Description("Azərbaycan vətəndaşı olmayan Şəxsiyyət vəsiqəsi")]
        ForeignIdCard = 8
    }
}
