using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;
using DatingApp.Api.Extensions;
using DatingApp.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Data.Repository
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;

        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
        {
            var userLikes = await _context.Likes.FindAsync(sourceUserId, targetUserId);
            return userLikes;
        }

        public async Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId)
        {
            var users = _context.Users.OrderBy(u=>u.UserName).AsQueryable();
            var likes = _context.Likes.AsQueryable();

            if(predicate == "liked")
            {
                likes = likes.Where(like=>like.SourceUserId == userId);
                users = likes.Select(like=>like.TargetUser);
            }else if (predicate == "likedBy")
            {
                likes = likes.Where(like => like.TargetUserId == userId);
                users = likes.Select(like => like.SourceUser);
            }

            var results = await users.Select(user => new LikeDto
            {
                UserName = user.UserName,
                KnownAs = user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
                City = user.City,
                Id = user.Id,

            }).ToListAsync();

            return results;
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users
                .Include(x=>x.LikedUsers)
                .FirstOrDefaultAsync(x=>x.Id == userId); 
        }
    }
}
