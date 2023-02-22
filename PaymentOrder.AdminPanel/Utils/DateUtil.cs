using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.AdminPanel.Utils
{
    public class DateUtil
    {
        public static int GetAge(DateTime birthDate)
        {
            DateTime now = DateTime.Today;

            var age = now.Year - birthDate.Year;

            if (birthDate.Date > now.AddYears(-age))
                age--;

            return age;
        }
    }
}
