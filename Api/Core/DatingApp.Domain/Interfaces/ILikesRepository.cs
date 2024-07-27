

using DatingApp.Domain.Entities;
using DatingApp.Domain.Entities.Pagination;
using DatingApp.Domain.Entities.Params;
using DatingApp.Domain.Params;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Domain.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<IQueryable<UserLike>> GetUserLikes(IParams likesParams);
    }
}
