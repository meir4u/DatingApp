//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using DatingApp.Api.DTOs;
//using DatingApp.Api.Entities;
//using DatingApp.Api.Helpers;
//using DatingApp.Api.Interfaces;
//using DatingApp.Domain.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace DatingApp.Api.Data.Repository
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly DataContext _context;
//        private readonly IMapper _mapper;

//        public UserRepository(DataContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//        public async Task<AppUser> GetUseByIdAsync(int id)
//        {
//            var user = await _context.Users.Include(u => u.Photos).FirstOrDefaultAsync(u => u.Id == id);
//            return user;
//        }

//        public async Task<AppUser> GetUserByUsernameAsync(string username)
//        {
//            var user = await _context.Users.Include(u => u.Photos).SingleOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
//            return user;
//        }

//        public async Task<AppUser> GetUserPhotoIdAsync(int photoId)
//        {
//            var user = await _context.Users.Include(u => u.Photos).IgnoreQueryFilters().SingleOrDefaultAsync(x => x.Photos.FirstOrDefault(p => p.Id == photoId) != null);
//            return user;
//        }

//        public async Task<IEnumerable<AppUser>> GetUsersAsync()
//        {
//            var user = await _context.Users.Include(u => u.Photos).ToListAsync();
//            return user;
//        }

//        public void Udpate(AppUser user)
//        {
//            _context.Entry(user).State = EntityState.Modified;
//        }

//        public async Task<PagedList<MemberDto>> GetMembersAsync(IParams userParams)
//        {
//            var filterParams = (UserParams)userParams;
//            var query = _context.Users.AsQueryable();
//            query = query.Where(u=>u.UserName != filterParams.CurrentUsername);
//            query = query.Where(u=> u.Gender == filterParams.Gender);
//            query.IgnoreQueryFilters();

//            var minDateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-filterParams.MaxAge - 1));
//            var maxDateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-filterParams.MinAge - 1));

//            query = query.Where(u=>u.DateOfBirth >= minDateOfBirth && u.DateOfBirth <= maxDateOfBirth);

//            query = filterParams.OrderBy switch
//            {
//                "created" => query.OrderByDescending(u => u.Created),
//                _ => query.OrderByDescending(u => u.LastActive),
//            };

//            return await PagedList<MemberDto>.CreateAsync(
//                query.AsNoTracking().ProjectTo<MemberDto>(_mapper.ConfigurationProvider),
//                filterParams.PageNumber,
//                filterParams.PageSize);

//        }

//        public async Task<MemberDto> GetMemberAsync(string username, string currentUser)
//        {
//            var query = _context.Users.Where(x => x.UserName == username);
//            if (username.Equals(currentUser)) query = query.IgnoreQueryFilters();

//            return await query
//                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            
//        }

//        public async Task<string> GetUserGender(string username)
//        {
//            var user = await _context.Users.Where(x => x.UserName == username).Select(x => x.Gender).FirstOrDefaultAsync();
//            return user;
//        }
//    }
//}
