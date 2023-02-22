using Microsoft.AspNetCore.Identity;
using PaymentOrder.Core.Domain.Entities;
using PaymentOrder.Core.Utils;

namespace PaymentOrder.WebCore.IdentityServer
{
    public class CustomPasswordHasher : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            throw new System.NotImplementedException();
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            var providedPasswordHash = SecurityUtil.ComputeSha256Hash(providedPassword);

            if (hashedPassword == providedPasswordHash)
                return PasswordVerificationResult.Success;

            return PasswordVerificationResult.Failed;
        }
    }
}
