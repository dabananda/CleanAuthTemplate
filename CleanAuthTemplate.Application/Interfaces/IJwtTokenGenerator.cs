using CleanAuthTemplate.Domain.Entities;

namespace CleanAuthTemplate.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}
