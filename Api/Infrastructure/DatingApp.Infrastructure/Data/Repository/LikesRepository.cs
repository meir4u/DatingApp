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
using System;
using DatingApp.Common.Exceptions;

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
            try
            {
                var userLikes = await _context.Likes.FindAsync(sourceUserId, targetUserId);
                return userLikes;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "{sourceUserId}, {targetUserId}", sourceUserId, targetUserId);
                throw new RepositoryException($"Exception in: {nameof(this.GetUserLike)}", ex);
            }
            
        }

        public async Task<IQueryable<AppUser>> GetUserLikes(IParams likesParams)
        {
            try
            {
                var filterParams = (LikesParams)likesParams;
                var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();
                var likes = _context.Likes.AsQueryable();

                if (filterParams.Predicate == "liked")
                {
                    likes = likes.Where(like => like.SourceUserId == filterParams.UserId);
                    users = likes.Select(like => like.TargetUser);
                }
                else if (filterParams.Predicate == "likedBy")
                {
                    likes = likes.Where(like => like.TargetUserId == filterParams.UserId);
                    users = likes.Select(like => like.SourceUser);
                }

                return users;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "{@likesParams}", likesParams);
                throw new RepositoryException($"Exception in: {nameof(this.GetUserLikes)}" , ex);
            }
            
        }

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            try
            {
                var userWithLikes = await _context.Users
                .Include(x => x.LikedUsers)
                .FirstOrDefaultAsync(x => x.Id == userId);
                return userWithLikes;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "{userId}", userId);
                throw new RepositoryException($"Exception in: {nameof(this.GetUserWithLikes)}", ex);
            }
            
        }
    }
}
