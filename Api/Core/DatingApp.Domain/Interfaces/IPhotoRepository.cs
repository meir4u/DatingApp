using DatingApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Domain.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IQueryable<Photo>> GetUnapprovedPhotos();
        Task<Photo> GetPhotoById(int id);
        void Update(Photo photo);
        void RemovePhoto(Photo photo);
    }
}
