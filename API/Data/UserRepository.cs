using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        ///<returns>A list of all users as UserDto</returns>
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
        
        ///<returns>Searches and returns a User with a specified id from the database</returns>
        public async Task<User> GetUserByIdAsync(int id) 
        {
            return await _context.Users
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }
        ///<returns>Searches and returns a UserDto with a specified id from the database</returns>
        public async Task<UserDto> GetMemberByIdAsync(int id)
        {
            return await _context.Users
                .Where(x => x.Id == id)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
        ///<summary>Saves changed to the database</summary>
        ///<returns>A boolean indicating a successful or failed database save.</returns>
        public async Task<bool> SaveAllAsync() 
        {
            return await _context.SaveChangesAsync() > 0;
        }
        ///<summary>Searches the database for a user with the specified username</summary>
        ///<returns>A boolean indicating whether the user exists or not.</returns>
        public async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
        ///<summary>Flags the user entry as a modified entry in the database.</summary>
        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

    }
}