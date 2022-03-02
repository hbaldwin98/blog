using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public UsersController(DataContext context, IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _context = context;
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetUsers() 
        {
            var users = await _userRepository.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id) 
        {
            var user = await _userRepository.GetMemberByIdAsync(id);

            if (user == null) return NotFound("User does not exist");

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto) 
        {
            if (await _userRepository.UserExists(registerDto.Username)) return BadRequest("User with that username already exists");

            var user = _mapper.Map<User>(registerDto);
            user.UserName = user.UserName.ToLower();

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            
            if (user == null) return Unauthorized("Invalid username");

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [Authorize]
        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(i => i.Id == id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete user");
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id, UpdateUserDto userUpdate)
        {
            var user = await _userRepository.GetUserByNameAsync(User.GetUsername());
            
            _mapper.Map(userUpdate, user);

            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");
        }
    }
}