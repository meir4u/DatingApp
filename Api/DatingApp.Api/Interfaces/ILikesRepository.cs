using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;
using DatingApp.Application.Pagination;
using DatingApp.Domain.Params;

namespace DatingApp.Api.Interfaces
{
    public interface ILikesRepository
    {
        Task<UserLike> GetUserLike(int sourceUserId, int targetUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(IParams likesParams);
    }
}
