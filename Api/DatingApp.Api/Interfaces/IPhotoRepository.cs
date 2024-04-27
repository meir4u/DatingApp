using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;

namespace DatingApp.Api.Interfaces
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<PhotoForApprovalDto>> GetUnapprovedPhotos();
        Task<Photo> GetPhotoById(int id);
        void Update(Photo photo);
        void RemovePhoto(Photo photo);
    }
}
