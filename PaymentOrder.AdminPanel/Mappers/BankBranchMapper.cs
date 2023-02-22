using PaymentOrder.AdminPanel.Models;
using PaymentOrder.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.AdminPanel.Mappers
{
    public class BankBranchMapper : BaseMapper<BankBranch, BankBranchModel>
    {
        private readonly BankMapper _bankMapper = new BankMapper();
        public override BankBranch Map(BankBranchModel model)
        {
            return new BankBranch()
            {
                Id = model.Id,
                Name = model.Name,
                Fax = model.Fax,
                Address = model.Address,
                Phone = model.Phone,
                Bank = _bankMapper.Map(model.Bank)
            };
        }

        public override BankBranchModel Map(BankBranch entity)
        {
            return new BankBranchModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Fax = entity.Fax,
                Address = entity.Address,
                Phone = entity.Phone,
                Bank = _bankMapper.Map(entity.Bank)
            };
        }
    }
}
