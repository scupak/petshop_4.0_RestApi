using Petshop.Core.Entity;

namespace Petshop.core.DomainServices
{
    public interface IAuthenticationHelper
    {
        string GenerateToken(User user);

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
