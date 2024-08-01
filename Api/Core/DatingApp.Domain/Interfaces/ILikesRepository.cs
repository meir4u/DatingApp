

using DatingApp.Domain.Entities;
using DatingApp.Domain.Params;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Domain.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<IQueryable<AppUser>> GetUserLikes(IParams likesParams);
    }
}
