using PaymentOrder.Core.Domain.Abstract;
using PaymentOrder.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.AdminPanel.Infrastructure
{
    public static class Kernel
    {
        public static IUnitOfWork DB { get; set; }
        public static User CurrentUser { get; set; }
    }
}
