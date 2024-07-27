

using DatingApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.Domain.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetUnapprovedPhotos();
        Task<Photo> GetPhotoById(int id);
        void Update(Photo photo);
        void RemovePhoto(Photo photo);
    }
}
