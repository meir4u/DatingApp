using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapper.QueryableExtensions;
using DatingApp.Application.DTOs.Photo;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DatingApp.Infrastructure.Data.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _context;
        private readonly ILogger _logger;

        public PhotoRepository(DataContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Photo> GetPhotoById(int id)
        {
            try
            {
                var query = _getPhotosQuery().Where(p => p.Id == id);
                var data = await query.SingleOrDefaultAsync();
                return data;
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "{id}", id);
                throw;
            }
            
        }

        public async Task<IQueryable<Photo>> GetUnapprovedPhotos()
        {
            try
            {
                var query = _getPhotosQuery().Include(x => x.AppUser).Where(p => p.IsApproved == false);

                return query;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{message}", ex.Message);
                throw;
            }
            
        }

        public void Update(Photo photo)
        {
            try
            {
                _context.Entry(photo).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{@photo}", photo);
                throw;
            }
            
        }

        public void RemovePhoto(Photo photo)
        {
            try
            {
                var query = _context.Photos.Remove(photo);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{@photo}", photo);
                throw;
            }
            
        }

        private IQueryable<Photo> _getPhotosQuery()
        {
            try
            {
                var query = _context.Photos.AsQueryable().OrderBy(x => x.Id).IgnoreQueryFilters();
                return query;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "{message}", ex.Message);
                throw;
            }
            
        }
    }
}
