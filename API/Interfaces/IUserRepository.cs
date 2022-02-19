using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);

        Task<UserDto> GetMemberByIdAsync(int id);
        Task<bool> UserExists(string username);
        void Update(User user);
        Task<bool> SaveAllAsync();
    }
}