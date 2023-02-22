using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.AdminPanel.Attributes
{
    public class ExcelDisplayAttribute : Attribute
    {
        public int ColumnNo { get; set; }
        public string Name { get; set; }
    }
}
