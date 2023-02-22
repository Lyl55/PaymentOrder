using PaymentOrder.AdminPanel.Models;
using PaymentOrder.AdminPanel.Utils;
using PaymentOrder.Core.Domain.Abstract;
using PaymentOrder.Core.Domain.Entities;
using PaymentOrder.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentOrder.AdminPanel.Infrastructure
{
    public class DataValidator
    {
        private readonly IUnitOfWork _db;
        public DataValidator(IUnitOfWork db)
        {
            _db = db;
        }

        public bool IsBankValid(BankModel currentBank, out string message)
        {
            if (currentBank.IsValid(out message) == false)
                return false;

            var allBanks = _db.BankRepository.GetAll();

            if (allBanks.Any(x => x.Id != currentBank.Id && x.Name == currentBank.Name))
            {
                message = ValidationHelper.GetUniqueMessage("Ad");
                return false;
            }

            if (allBanks.Any(x => x.Id != currentBank.Id && x.VOEN == currentBank.VOEN))
            {
                message = ValidationHelper.GetUniqueMessage("VÖEN");
                return false;
            }

            if (allBanks.Any(x => x.Id != currentBank.Id && x.CorrespondentAccount == currentBank.CorrespondentAccount))
            {
                message = ValidationHelper.GetUniqueMessage("Hesab nömrəsi");
                return false;
            }

            if (allBanks.Any(x => x.Id != currentBank.Id && x.SWIFT == currentBank.SWIFT))
            {
                message = ValidationHelper.GetUniqueMessage("SWIFT");
                return false;
            }

            return true;
        }

        public bool IsBranchValid(BankBranchModel currentBankBranch, out string message)
        {
            if (currentBankBranch.IsValid(out message) == false)
                return false;

            var allBranchs = _db.BankBranchRepository.GetAll();

            if (allBranchs.Any(x => x.Id != currentBankBranch.Id && x.Name == currentBankBranch.Name && x.Bank.Name == currentBankBranch.Bank.Name))
            {
                message = ValidationHelper.GetUniqueMessage("Ad");
                return false;
            }

            if (allBranchs.Any(x => x.Id != currentBankBranch.Id && x.Phone == currentBankBranch.Phone))
            {
                message = ValidationHelper.GetUniqueMessage("Telefon nömrəsi");
                return false;
            }

            if (allBranchs.Any(x => x.Id != currentBankBranch.Id && x.Fax == currentBankBranch.Fax))
            {
                message = ValidationHelper.GetUniqueMessage("FAX");
                return false;
            }
            return true;
        }

        #region resident validation

        public bool IsResidentValid(ResidentModel currentResident, out string message)
        {
            if (currentResident.IsValid(out message) == false)
                return false;

            var allResidents = _db.ResidentRepository.GetAll();

            if (currentResident.FirstName.Any(x => char.IsLetter(x) == false))
            {
                message = ValidationHelper.GetLetterMessage("Ad");
                return false;
            }

            if (currentResident.LastName.Any(x => char.IsLetter(x) == false))
            {
                message = ValidationHelper.GetLetterMessage("Soyad");
                return false;
            }

            if (currentResident.FatherName.Any(x => char.IsLetter(x) == false))
            {
                message = ValidationHelper.GetLetterMessage("Ata adı");
                return false;
            }

            if (IsSerialNumberValid(currentResident.SerialNumber, currentResident.Id, currentResident.DocumentType, allResidents, out message) == false)
                return false;

            if (IsFinValid(currentResident.FIN, currentResident.Id, currentResident.DocumentType, allResidents, out message))
                return false;

            // Phone Number
            if (currentResident.PhoneNumber.Length != 10)
            {
                message = ValidationHelper.GetRequiredLength("Telefon nömrəsi", 10);
                return false;
            }

            if (currentResident.PhoneNumber.All(x => char.IsNumber(x)) == false)
            {
                message = "Telefon nömrəsi yalnız rəqəmlərdən ibarət olmalıdır";
                return false;
            }

            if (allResidents.Any(x => x.Id != currentResident.Id && x.PhoneNumber == currentResident.PhoneNumber))
            {
                message = ValidationHelper.GetUniqueMessage("Telefon nömrəsi");
                return false;
            }

            //Email
            if (currentResident.Email.Contains('@') == false || currentResident.Email.Contains('.') == false)
            {
                message = "Email adında . və @ işarəsi mütləq olmalıdır";
                return false;
            }

            char[] permittedSymbols = { '@', '-', '.', '+', '_' };

            if (currentResident.Email.All(x => (x >= 'A' && x <= 'Z')
                                           || (x >= 'a' && x <= 'z')
                                           || char.IsNumber(x)
                                           || permittedSymbols.Contains(x)) == false)
            {
                message = "Email adı yalnız hərf, rəqəm və @ - . + _ simvollarından ibarət ola bilər";
                return false;
            }

            if (allResidents.Any(x => x.Id != currentResident.Id && x.Email == currentResident.Email))
            {
                message = ValidationHelper.GetUniqueMessage("Email");
                return false;
            }

            int age = DateUtil.GetAge(currentResident.BirthDate.Value);

            if (age < 18)
            {
                if (string.IsNullOrWhiteSpace(currentResident.GUARDIAN))
                {
                    message = "Yaşınız 18-dən kiçik olduğu üçün GUARDİAN hissəsində himayədarın ad,soyad və ata adını daxil edin";
                    return false;
                }

                if (string.IsNullOrWhiteSpace(currentResident.NATIONID))
                {
                    message = "Yaşınız 18-dən kiçik olduğu üçün NATIONID hissəsində himayədarın şəxsiyyətini təsdiq edən sənədin seriya nömrəsini daxil edin daxil edin";
                    return false;
                }
            }

            return true;
        }

        private bool IsSerialNumberValid(string serialNumber, int residentId, DocumentType documentType, List<Resident> allResidents, out string message)
        {
            if (documentType == DocumentType.IDCard)
            {
                if (string.IsNullOrWhiteSpace(serialNumber))
                {
                    message = ValidationHelper.GetRequiredMessage("Seriya nömrəsi");
                    return false;
                }

                if (serialNumber.StartsWith("AZE") == false)
                {
                    message = "Şəxsiyyət vəsiqəsinin seriya nömrəsi AZE ilə başlamalıdır";
                    return false;
                }

                if (serialNumber.Length != 11 || serialNumber.Substring(3).Any(x => char.IsNumber(x) == false))
                {
                    message = "Şəxsiyyət vəsiqəsinin seriya nömrəsi üçün AZE prefiksindən sonra 8 rəqəm daxil edilməlidir";
                    return false;
                }

                if (allResidents.Any(x => x.Id != residentId && x.SerialNumber == serialNumber))
                {
                    message = ValidationHelper.GetUniqueMessage("Seriya nömrəsi");
                    return false;
                }
            }
            else if (documentType == DocumentType.NewIDCard)
            {
                if (string.IsNullOrWhiteSpace(serialNumber))
                {
                    message = ValidationHelper.GetRequiredMessage("Seriya nömrəsi");
                    return false;
                }

                if (serialNumber.StartsWith("AA") == false)
                {
                    message = "Yeni şəxsiyyət vəsiqəsinin seriya nömrəsi AA ilə başlamalıdır";
                    return false;
                }

                if (serialNumber.Length != 9 || serialNumber.Substring(2).Any(x => char.IsNumber(x) == false))
                {
                    message = "Yeni şəxsiyyət vəsiqəsinin seriya nömrəsi üçün AA prefiksindən sonra 7 rəqəm daxil edilməlidir";
                    return false;
                }

                if (allResidents.Any(x => x.Id != residentId && x.SerialNumber == serialNumber))
                {
                    message = ValidationHelper.GetUniqueMessage("Seriya nömrəsi");
                    return false;
                }
            }
            else if (documentType == DocumentType.ForeignIdCard)
            {
                if (string.IsNullOrWhiteSpace(serialNumber))
                {
                    message = ValidationHelper.GetRequiredMessage("Seriya nömrəsi");
                    return false;
                }

                if (serialNumber.StartsWith("AR") == false)
                {
                    message = "Yeni şəxsiyyət vəsiqəsinin seriya nömrəsi AR ilə başlamalıdır";
                    return false;
                }

                if (serialNumber.Length != 8 || serialNumber.Substring(2).Any(x => char.IsNumber(x) == false))
                {
                    message = "Yeni şəxsiyyət vəsiqəsinin seriya nömrəsi üçün AR prefiksindən sonra 6 rəqəm daxil edilməlidir";
                    return false;
                }

                if (allResidents.Any(x => x.Id != residentId && x.SerialNumber == serialNumber))
                {
                    message = ValidationHelper.GetUniqueMessage("Seriya nömrəsi");
                    return false;
                }
            }
            else if (documentType == DocumentType.ResidencePermit)
            {
                if (string.IsNullOrWhiteSpace(serialNumber))
                {
                    message = ValidationHelper.GetRequiredMessage("Seriya nömrəsi");
                    return false;
                }

                if (serialNumber.Length != 7 || serialNumber.Any(x => char.IsNumber(x) == false))
                {
                    message = "Daimi yaşayış icazəsi sənədinin seriya nömrəsi 7 rəqəmdən ibarət olmalıdır";
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(serialNumber) == false)
                {
                    message = ValidationHelper.GetProhibitedMessage("Seriya nömrəsi");
                    return false;
                }
            }

            message = string.Empty;
            return true;
        }

        private bool IsFinValid(string fin, int residentId, DocumentType documentType, List<Resident> residents, out string message)
        {
            if(documentType == DocumentType.IDCard || documentType == DocumentType.NewIDCard)
            {
                if (string.IsNullOrWhiteSpace(fin))
                {
                    message = ValidationHelper.GetRequiredMessage("FIN");
                    return false;
                }

                if(fin.Length != 7)
                {
                    message = ValidationHelper.GetRequiredLength("FIN", 7);
                    return false;
                }

                char[] prohibitedLetters = { '0', 'I' };

                if (fin.All(x => (x >= 'A' && x <= 'Z' && prohibitedLetters.Contains(x) == false) || char.IsNumber(x)) == false)
                {
                    message = "FIN yalnız böyük hərf (O, İ və I hərfləri istisna) və ya rəqəmlərdən ibarət ola bilər";
                    return false;
                }

                if (residents.Any(x => x.Id != residentId && x.FIN == fin))
                {
                    message = ValidationHelper.GetUniqueMessage("FIN");
                    return false;
                }
            }
            else if(documentType == DocumentType.ResidencePermit)
            {
                if (string.IsNullOrWhiteSpace(fin))
                {
                    message = ValidationHelper.GetRequiredMessage("FIN");
                    return false;
                }

                if(fin.Length != 5 && fin.Length != 6)
                {
                    message = "FIN mütləq 5 və ya 6 simvol olmalıdır";
                    return false;
                }

                if (fin.All(x => (x >= 'A' && x <= 'Z') || char.IsNumber(x)) == false)
                {
                    message = "FIN yalnız böyük hərf və ya rəqəmdən ibarət olmalıdır";
                }

                if (residents.Any(x => x.Id != residentId && x.FIN == fin))
                {
                    message = ValidationHelper.GetUniqueMessage("FIN");
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(fin) == false)
                {
                    message = ValidationHelper.GetProhibitedMessage("Seriya nömrəsi");
                    return false;
                }
            }

            message = string.Empty;
            return true;
        }

        #endregion
    }
}

