using DatingApp.Domain.Entities;
using DatingApp.Domain.Params;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        void Udpate(AppUser user);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUseByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<AppUser> GetUserPhotoIdAsync(int photoId);
        Task<IQueryable<AppUser>> GetMembersAsync(IParams userParams);
        Task<AppUser> GetMemberAsync(string username, string currentUser);
        Task<string> GetUserGender(string username);
    }
}
