using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<MemberDto>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<MemberDto> GetMemberByIdAsync(int id);
        Task<User> GetUserByNameAsync(string name);
        Task<bool> UserExists(string username);
        void Update(User user);
    }
}