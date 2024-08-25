using AutoMapper;
using AutoMapper.QueryableExtensions;
using DatingApp.Application.DTOs.Member;
using DatingApp.Domain.Entities;
using DatingApp.Application.Params;
using DatingApp.Domain.Interfaces;
using DatingApp.Domain.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Application.Pagination;
using MediatR;
using Serilog;
using DatingApp.Common.Exceptions;

namespace DatingApp.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public UserRepository(DataContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<AppUser> GetUseByIdAsync(int id)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Photos).FirstOrDefaultAsync(u => u.Id == id);
                return user;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "{id}", id);
                throw new RepositoryException($"Exception in: {nameof(this.GetUseByIdAsync)}", ex);
            }
            
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Photos).SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{username}", username);
                throw new RepositoryException($"Exception in: {nameof(this.GetUserByUsernameAsync)}", ex);
            }
            
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Photos).SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{email}", email);
                throw new RepositoryException($"Exception in: {nameof(this.GetUserByUsernameAsync)}", ex);
            }

        }

        public async Task<AppUser> GetUserPhotoIdAsync(int photoId)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Photos).IgnoreQueryFilters().SingleOrDefaultAsync(x => x.Photos.FirstOrDefault(p => p.Id == photoId) != null);
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{photoId}", photoId);
                throw new RepositoryException($"Exception in: {nameof(this.GetUserPhotoIdAsync)}", ex);
            }
            
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            try
            {
                return await _context.Users.Include(u => u.Photos).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{Message}", ex.Message);
                throw new RepositoryException($"Exception in: {nameof(this.GetUsersAsync)}", ex);
            }
            
        }

        public void Udpate(AppUser user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{@user}", user);
                throw new RepositoryException($"Exception in: {nameof(this.Udpate)}", ex);
            }
            
        }

        public async Task<IQueryable<AppUser>> GetMembersAsync(IParams userParams)
        {
            try
            {
                var filterParams = (UserParams)userParams;
                var query = _context.Users.Include(u => u.Photos).AsQueryable();
                query = query.Where(u => u.UserName != filterParams.CurrentUsername);
                query = query.Where(u => u.Gender == filterParams.Gender);
                query.IgnoreQueryFilters();

                var minDateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-filterParams.MaxAge - 1));
                var maxDateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-filterParams.MinAge - 1));

                query = query.Where(u => u.DateOfBirth >= minDateOfBirth && u.DateOfBirth <= maxDateOfBirth);

                query = filterParams.OrderBy switch
                {
                    "created" => query.OrderByDescending(u => u.Created),
                    _ => query.OrderByDescending(u => u.LastActive),
                };

                return query.AsNoTracking();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{@userParams}", userParams);
                throw new RepositoryException($"Exception in: {nameof(this.GetMembersAsync)}", ex);
            }
            
        }

        public async Task<AppUser> GetMemberAsync(string username, string currentUser)
        {
            try
            {
                var query = _context.Users.Where(x => x.UserName == username);
                if (username.Equals(currentUser)) query = query.IgnoreQueryFilters();

                var user = await query.SingleOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{username}, {currentUser}", username, currentUser);
                throw new RepositoryException($"Exception in: {nameof(this.GetMemberAsync)}", ex);
            }
            


        }

        public async Task<string> GetUserGender(string username)
        {
            try
            {
                var user = await _context.Users.Where(x => x.UserName == username).Select(x => x.Gender).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{username}", username);
                throw new RepositoryException($"Exception in: {nameof(this.GetUserGender)}", ex);
            }
            
        }

        public async Task<AppUser> UpdateUserLastActive(int userId)
        {
            try
            {
                var user = await GetUseByIdAsync(userId);

                user.LastActive = DateTime.UtcNow;

                return user;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{userId}", userId);
                throw new RepositoryException($"Exception in: {nameof(this.UpdateUserLastActive)}", ex);
            }
            
        }
    }
}
