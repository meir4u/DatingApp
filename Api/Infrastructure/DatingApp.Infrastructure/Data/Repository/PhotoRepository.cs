using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.QueryableExtensions;
using DatingApp.Application.DTOs.Photo;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DatingApp.Infrastructure.Data.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PhotoRepository(DataContext context, IMapper mapper, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Photo> GetPhotoById(int id)
        {
            var query = _getPhotosQuery().Where(p => p.Id == id);
            var data = await query.SingleOrDefaultAsync();
            return data;
        }

        public async Task<IQueryable<Photo>> GetUnapprovedPhotos()
        {
            var query = _getPhotosQuery().Include(x=>x.AppUser).Where(p => p.IsApproved == false);

            return query;
        }

        public void Update(Photo photo)
        {
            _context.Entry(photo).State = EntityState.Modified;
        }

        public void RemovePhoto(Photo photo)
        {
            var query = _context.Photos.Remove(photo);
        }

        private IQueryable<Photo> _getPhotosQuery()
        {
            var query = _context.Photos.AsQueryable().OrderBy(x=>x.Id).IgnoreQueryFilters();
            return query;
        }
    }
}
