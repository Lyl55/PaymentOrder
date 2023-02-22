using PaymentOrder.AdminPanel.Utils;
using PaymentOrder.Core.Domain.Enums;
using System;

namespace PaymentOrder.AdminPanel.Models
{
    public class ResidentModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public DocumentType DocumentType { get; set; }
        public string SerialNumber { get; set; }
        public string FIN { get; set; }
        public string Authority { get; set; }
        public DateTime? GivingDate { get; set; }=DateTime.Now;
        public DateTime? ReliabilityDate { get; set; }=DateTime.Now;
        public MartialType MartialStatus { get; set; }
        public GenderType Gender { get; set; }
        public DateTime? BirthDate { get; set; }=DateTime.Now;
        public string BirthCountry { get; set; }
        public string RegistrationAddressCountry { get; set; }
        public string RegistrationAddressCity { get; set; }
        public string RegistrationAddressStreet { get; set; }
        public string MailingAddress1 { get; set; }
        public string Citizenship { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ActualAddressCountry { get; set; }
        public string ActualAddressCity { get; set; }
        public string ActualAddressStreet { get; set; }
        public string MailingAddress2 { get; set; }
        public decimal? MonthlySalary { get; set; } = 0;
        public IncomeType IncomeSource { get; set; }
        public EducationDegree Education { get; set; }
        public string Position { get; set; }
        public string GUARDIAN { get; set; }
        public string NATIONID { get; set; }
        public bool IsConviction { get; set; }
        public CurrencyType Currency { get; set; }
        public string Code { get; set; }



        public override object Clone()
        {
            return new ResidentModel()
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                FatherName = FatherName,
                DocumentType = DocumentType,
                SerialNumber = SerialNumber,
                FIN = FIN,
                Authority = Authority,
                GivingDate = GivingDate,
                ReliabilityDate = ReliabilityDate,
                MartialStatus = MartialStatus,
                Gender = Gender,
                BirthDate = BirthDate,
                BirthCountry = BirthCountry,
                RegistrationAddressCountry = RegistrationAddressCountry,
                RegistrationAddressCity = RegistrationAddressCity,
                RegistrationAddressStreet = RegistrationAddressStreet,
                MailingAddress1 = MailingAddress1,
                Citizenship = Citizenship,
                PhoneNumber = PhoneNumber,
                Email = Email,
                ActualAddressCountry = ActualAddressCountry,
                ActualAddressCity = ActualAddressCity,
                ActualAddressStreet = ActualAddressStreet,
                MailingAddress2 = MailingAddress2,
                MonthlySalary = MonthlySalary,
                IncomeSource = IncomeSource,
                Education = Education,
                Position = Position,
                GUARDIAN = GUARDIAN,
                NATIONID = NATIONID,
                IsConviction = IsConviction,
                Currency = Currency,
                Code = Code
            };
        }
        
        public override bool IsValid(out string message)
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                message = ValidationHelper.GetRequiredMessage("Ad");
                return false;
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                message = ValidationHelper.GetRequiredMessage("Soyad");
                return false;
            }

            if (string.IsNullOrWhiteSpace(FatherName))
            {
                message = ValidationHelper.GetRequiredMessage("Ata adı");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Authority))
            {
                message = ValidationHelper.GetRequiredMessage("Sənədi verən orqan");
                return false;
            }

            if (GivingDate.HasValue == false)
            {
                message = ValidationHelper.GetRequiredMessage("Sənədin verilmə tarixi");
                return false;
            }
            
            if (ReliabilityDate.HasValue == false)
            {
                message = ValidationHelper.GetRequiredMessage("Sənədin etibarlılıq tarixi");
                return false;
            }

            if (BirthDate.HasValue == false)
            {
                message = ValidationHelper.GetRequiredMessage("Doğum tarixi");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(BirthCountry))
            {
                message = ValidationHelper.GetRequiredMessage("Doğulduğu ölkə");
                return false;
            }

            if (string.IsNullOrWhiteSpace(RegistrationAddressCountry))
            {
                message = ValidationHelper.GetRequiredMessage("Qeydiyyat ünvanı:Ölkə");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(RegistrationAddressCity))
            {
                message = ValidationHelper.GetRequiredMessage("Qeydiyyat ünvanı:Şəhər/Rayon/Ştat");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(RegistrationAddressStreet))
            {
                message = ValidationHelper.GetRequiredMessage("Qeydiyyat ünvanı:Ev,bina,küçə adı/kənd");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(ActualAddressCountry))
            {
                message = ValidationHelper.GetRequiredMessage("Faktiki ünvanı:Ölkə");
                return false;
            }

            if (string.IsNullOrWhiteSpace(ActualAddressCity))
            {
                message = ValidationHelper.GetRequiredMessage("Faktiki ünvanı:Şəhər/Rayon/Ştat");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(ActualAddressStreet))
            {
                message = ValidationHelper.GetRequiredMessage("Faktiki ünvanı:Ev,bina,küçə adı/kənd");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(Citizenship))
            {
                message = ValidationHelper.GetRequiredMessage("Vətəndaşlıq");
                return false;
            }

            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                message = ValidationHelper.GetRequiredMessage("Telefon nömrəsi");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(Email))
            {
                message = ValidationHelper.GetRequiredMessage("Email");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(MailingAddress1))
            {
                message = ValidationHelper.GetRequiredMessage("Poçt indeksi(1)");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(MailingAddress2))
            {
                message = ValidationHelper.GetRequiredMessage("Poçt indeksi(2)");
                return false;
            }
            
            if (MonthlySalary.HasValue == false)
            {
                message = ValidationHelper.GetRequiredMessage("Aylıq əmək haqqı");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(Position))
            {
                message = ValidationHelper.GetRequiredMessage("Vəzifə");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Code))
            {
                message = ValidationHelper.GetRequiredMessage("Kod sözü");
                return false;
            }

            message = string.Empty;
            return true;
        }
    
        public override string ToExcelString()
        {
            return $"{FirstName} {LastName} {FatherName}";
        }
    }
}