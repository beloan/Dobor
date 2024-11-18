using Application.Abstractions.ServiceAbstractions;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Services
{
    public class HasherService : IHasherService
    {
        public string GetHash(string? str, byte[]? salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: str!,
                salt: salt!,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 32));
        }

        public byte[] GetSalt()
        {
            return RandomNumberGenerator.GetBytes(16);
        }
    }
}
