using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<MemberDto>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<MemberDto> GetMemberByIdAsync(int id);
        Task<AppUser> GetUserByNameAsync(string name);
        Task<bool> UserExists(string username);
        void Update(AppUser user);
    }
}