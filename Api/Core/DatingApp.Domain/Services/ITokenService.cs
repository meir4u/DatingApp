using DatingApp.Domain.Entities;
using System.Threading.Tasks;

namespace DatingApp.Domain.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
