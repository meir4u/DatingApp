//using AutoMapper;
//using AutoMapper.QueryableExtensions;
//using DatingApp.Api.DTOs;
//using DatingApp.Api.Entities;
//using DatingApp.Api.Interfaces;
//using DatingApp.Domain.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace DatingApp.Api.Data.Repository
//{
//    public class PhotoRepository : IPhotoRepository
//    {
//        private readonly DataContext _context;
//        private readonly IMapper _mapper;

//        public PhotoRepository(DataContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }
//        public async Task<Photo> GetPhotoById(int id)
//        {
//            var query = _getPhotosQuery().Where(p => p.Id == id);
//            var data = await query.SingleOrDefaultAsync();
//            return data;
//        }

//        public async Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos()
//        {
//            var query = _getPhotosQuery().Include(x=>x.AppUser).Where(p => p.IsApproved == false);
//            var data = await query.ProjectTo<PhotoForApprovalDto>(_mapper.ConfigurationProvider).ToListAsync();
//            return data;
//        }

//        public void Update(Photo photo)
//        {
//            _context.Entry(photo).State = EntityState.Modified;
//        }

//        public void RemovePhoto(Photo photo)
//        {
//            var query = _context.Photos.Remove(photo);
//        }

//        private IQueryable<Photo> _getPhotosQuery()
//        {
//            var query = _context.Photos.AsQueryable().OrderBy(x=>x.Id).IgnoreQueryFilters();
//            return query;
//        }
//    }
//}
