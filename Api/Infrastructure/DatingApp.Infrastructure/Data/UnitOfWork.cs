using AutoMapper;
using DatingApp.Domain.Interfaces;
using DatingApp.Infrastructure.Data.Repository;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;

namespace DatingApp.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UnitOfWork(DataContext context, IMapper mapper, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public IUserRepository UserRepository => new UserRepository(_context, _mapper, _logger);

        public IMessageRepository MessageRepository => new MessageRepository(_context, _mapper, _logger);

        public ILikesRepository LikesRepository => new LikesRepository(_context, _logger);
        public IPhotoRepository PhotoRepository => new PhotoRepository(_context, _mapper, _logger);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
