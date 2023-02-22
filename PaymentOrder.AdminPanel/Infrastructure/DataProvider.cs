using PaymentOrder.AdminPanel.Mappers;
using PaymentOrder.AdminPanel.Models;
using PaymentOrder.Core.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentOrder.AdminPanel.Infrastructure
{
    public class DataProvider
    {
        private readonly IUnitOfWork _db;

        public DataProvider(IUnitOfWork db)
        {
            _db = db;
        }

        public List<ResidentModel> GetResidents()
        {
            var entities = Kernel.DB.ResidentRepository.GetAll();

            var residents = new List<ResidentModel>();

            ResidentMapper residentMapper = new ResidentMapper();

            for (int i = 0; i < entities.Count; i++)
            {
                var resident = entities[i];
                var residentModel = residentMapper.Map(resident);
                residentModel.No = i + 1;
                residents.Add(residentModel);
            }

            return residents;
        }

        public List<BankModel> GetBanks()
        {
            var banks = _db.BankRepository.GetAll();

            BankMapper bankMapper = new BankMapper();
            List<BankModel> bankModels = new List<BankModel>();

            for (int i = 0; i < banks.Count; i++)
            {
                var bank = banks[i];

                var bankModel = bankMapper.Map(bank);

                bankModel.No = i + 1;

                bankModels.Add(bankModel);
            }

            return bankModels;
        }

        public List<BankBranchModel> GetBankBranchs()
        {
            var entities = _db.BankBranchRepository.GetAll();

            BankBranchMapper mapper = new BankBranchMapper();
            List<BankBranchModel> models = new List<BankBranchModel>();

            for (int i = 0; i < entities.Count; i++)
            {
                var bank = entities[i];
                
                var bankModel = mapper.Map(bank);

                bankModel.No = i + 1;

                models.Add(bankModel);
            }

            return models;
        }
    }
}
