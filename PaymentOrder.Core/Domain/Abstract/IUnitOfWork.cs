using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.Core.Domain.Abstract
{
    public interface IUnitOfWork
    {
        IBankRepository BankRepository { get; }
        IUserRepository UserRepository { get; }
        IBankBranchRepository BankBranchRepository { get; }
        IResidentRepository ResidentRepository { get; }
    }
}
