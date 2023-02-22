using PaymentOrder.AdminPanel.Models;
using PaymentOrder.Core.Domain.Entities;
using System;

namespace PaymentOrder.AdminPanel.Mappers
{
    public class ResidentMapper : BaseMapper<Resident, ResidentModel>
    {
        public override Resident Map(ResidentModel model)
        {

            return new Resident()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FatherName = model.FatherName,
                DocumentType = model.DocumentType,
                SerialNumber = model.SerialNumber,
                FIN = model.FIN,
                Authority = model.Authority,
                GivingDate = model.GivingDate.Value,
                ReliabilityDate = model.ReliabilityDate.Value,
                MartialStatus = model.MartialStatus,
                Gender = model.Gender,
                BirthDate = model.BirthDate.Value,
                BirthCountry = model.BirthCountry,
                RegistrationAddressCountry = model.RegistrationAddressCountry,
                RegistrationAddressCity = model.RegistrationAddressCity,
                RegistrationAddressStreet = model.RegistrationAddressStreet,
                MailingAddress1 = model.MailingAddress1,
                Citizenship = model.Citizenship,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                ActualAddressCountry = model.ActualAddressCountry,
                ActualAddressCity = model.ActualAddressCity,
                ActualAddressStreet = model.ActualAddressStreet,
                MailingAddress2 = model.MailingAddress2,
                MonthlySalary = model.MonthlySalary.Value,
                IncomeSource = model.IncomeSource,
                Education = model.Education,
                Position = model.Position,
                GUARDIAN = model.GUARDIAN,
                NATIONID = model.NATIONID,
                IsConviction = model.IsConviction,
                Currency = model.Currency,
                Code = model.Code
            };
        }

        public override ResidentModel Map(Resident entity)
        {
            return new ResidentModel()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                FatherName = entity.FatherName,
                DocumentType = entity.DocumentType,
                SerialNumber = entity.SerialNumber,
                FIN = entity.FIN,
                Authority = entity.Authority,
                GivingDate = entity.GivingDate,
                ReliabilityDate = entity.ReliabilityDate,
                MartialStatus = entity.MartialStatus,
                Gender = entity.Gender,
                BirthDate = entity.BirthDate,
                BirthCountry = entity.BirthCountry,
                RegistrationAddressCountry = entity.RegistrationAddressCountry,
                RegistrationAddressCity = entity.RegistrationAddressCity,
                RegistrationAddressStreet = entity.RegistrationAddressStreet,
                MailingAddress1 = entity.MailingAddress1,
                Citizenship = entity.Citizenship,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                ActualAddressCountry = entity.ActualAddressCountry,
                ActualAddressCity = entity.ActualAddressCity,
                ActualAddressStreet = entity.ActualAddressStreet,
                MailingAddress2 = entity.MailingAddress2,
                MonthlySalary = entity.MonthlySalary,
                IncomeSource = entity.IncomeSource,
                Education = entity.Education,
                Position = entity.Position,
                GUARDIAN = entity.GUARDIAN,
                NATIONID = entity.NATIONID,
                IsConviction = entity.IsConviction,
                Currency = entity.Currency,
                Code = entity.Code
            };
        }
    }
}