using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;

namespace DatingApp.Api.Interfaces
{
    public interface IUserRepository
    {
        void Udpate(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUseByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<AppUser> AddUserAsync(AppUser user);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);
    }
}
