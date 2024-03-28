using AuthAPI.Entities.Dto;
using AuthAPI.Models;

namespace AuthAPI.Repositories.Interface
{
    public interface ITokenRepository
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> role);
    }
}
