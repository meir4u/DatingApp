using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            var user = await _context.Users.Include(u => u.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users.Include(u => u.Photos).SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
            return user;
        }

        public async Task<AppUser> GetUserPhotoIdAsync(int photoId)
        {
            var user = await _context.Users.Include(u => u.Photos).IgnoreQueryFilters().SingleOrDefaultAsync(x => x.Photos.FirstOrDefault(p => p.Id == photoId) != null);
            return user;
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.Include(u => u.Photos).ToListAsync();
        }

        public void Udpate(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<IQueryable<AppUser>> GetMembersAsync(IParams userParams)
        {
            var filterParams = (UserParams) userParams;
            var query = _context.Users.AsQueryable();
            query = query.Where(u=>u.UserName != filterParams.CurrentUsername);
            query = query.Where(u=> u.Gender == filterParams.Gender);
            query.IgnoreQueryFilters();

            var minDateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-filterParams.MaxAge - 1));
            var maxDateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-filterParams.MinAge - 1));

            query = query.Where(u=>u.DateOfBirth >= minDateOfBirth && u.DateOfBirth <= maxDateOfBirth);

            query = filterParams.OrderBy switch
            {
                "created" => query.OrderByDescending(u => u.Created),
                _ => query.OrderByDescending(u => u.LastActive),
            };

            return query.AsNoTracking();
        }

        public async Task<AppUser> GetMemberAsync(string username, string currentUser)
        {
            var query = _context.Users.Where(x => x.UserName == username);
            if (username.Equals(currentUser)) query = query.IgnoreQueryFilters();

            var user = await query.SingleOrDefaultAsync();
            return user;


        }

        public async Task<string> GetUserGender(string username)
        {
            var user = await _context.Users.Where(x => x.UserName == username).Select(x => x.Gender).FirstOrDefaultAsync();
            return user;
        }

        public async Task<AppUser> UpdateUserLastActive(int userId)
        {
            var user = await GetUseByIdAsync(userId);

            user.LastActive = DateTime.UtcNow;

            return user;
        }
    }
}
