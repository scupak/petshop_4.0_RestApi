using Petshop.Core.Entity;

namespace Petshop.core.DomainServices
{
    public interface IAuthenticationHelper
    {
        string GenerateToken(User user);
    }
}
