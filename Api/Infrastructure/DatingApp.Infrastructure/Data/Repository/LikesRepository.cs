using DatingApp.Application.DTOs.Like;
using DatingApp.Domain.Entities;
using DatingApp.Application.Params;
using DatingApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Application.Pagination;
using DatingApp.Domain.Params;
using DatingApp.Application.Helpers;
using Serilog;

namespace DatingApp.Infrastructure.Data.Repository
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public LikesRepository(DataContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
        {
            var userLikes = await _context.Likes.FindAsync(sourceUserId, targetUserId);
            return userLikes;
        }

        public async Task<IQueryable<AppUser>> GetUserLikes(IParams likesParams)
        {
            var filterParams = (LikesParams)likesParams;
            var users = _context.Users.OrderBy(u=>u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if(filterParams.Predicate == "liked")
            {
                likes = likes.Where(like=>like.SourceUserId == filterParams.UserId);
                users = likes.Select(like=>like.TargetUser);
            }else if (filterParams.Predicate == "likedBy")
            {
                likes = likes.Where(like => like.TargetUserId == filterParams.UserId);
                users = likes.Select(like => like.SourceUser);
            }

            return users;

            //var likedUsers = users.Select(user => new LikeDto
            //{
            //    UserName = user.UserName,
            //    KnownAs = user.KnownAs,
            //    Age = user.DateOfBirth.CalculateAge(),
            //    PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
            //    City = user.City,
            //    Id = user.Id,

            //});

            //var result = await PagedList<LikeDto>.CreateAsync(likedUsers, filterParams.PageNumber, filterParams.PageSize);

            //return result;
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            var userWithLikes = await _context.Users
                .Include(x => x.LikedUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
            return userWithLikes;
        }
    }
}
