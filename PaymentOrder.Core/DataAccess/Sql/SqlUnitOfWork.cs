using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaymentOrder.Core.Domain.Abstract;

namespace PaymentOrder.Core.DataAccess.Sql
{
    public class SqlUnitOfWork:IUnitOfWork
    {
        private readonly string _connectionString;

        public SqlUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IBankRepository BankRepository => new SqlBankRepository(_connectionString);
        public IUserRepository UserRepository => new SqlUserRepository(_connectionString);
        public IBankBranchRepository BankBranchRepository => new SqlBankBranchRepository(_connectionString);
        public IResidentRepository ResidentRepository => new SqlResidentRepository(_connectionString);
    }
}
