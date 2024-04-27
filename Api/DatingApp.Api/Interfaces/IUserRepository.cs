using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;
using DatingApp.Api.Helpers;

namespace DatingApp.Api.Interfaces
{
    public interface IUserRepository
    {
        void Udpate(AppUser user);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUseByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<AppUser> GetUserPhotoIdAsync(int photoId);
        Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        Task<MemberDto> GetMemberAsync(string username, string currentUser);
        Task<string> GetUserGender(string username);
    }
}
