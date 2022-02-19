using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UsersController(DataContext context, IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _userRepository = userRepository;
        }

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

        [HttpPost("add-user")]
        public async Task<ActionResult<RegisterDto>> AddUser(RegisterDto registerDto) 
        {
            var user = _mapper.Map<User>(registerDto);
            user.UserName = user.UserName.ToLower();

            if (await _userRepository.UserExists(registerDto.Username)) 
                return BadRequest("User with that username already exists");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new RegisterDto
            {
                Username = user.UserName
            };
        }

        [HttpPost("post-article/{id}")]
        
        public async Task<ActionResult> PostArticle(int id, ArticleDto articleDto)
        {

            var user = await _context.Users
                .Where(i => i.Id == id)
                .Include(a => a.Articles)
                .SingleOrDefaultAsync();
                
            if (user == null) return BadRequest("User does not exist");

            user.Articles.Add(new Article
            {
                AuthorId = user.Id,
                Title = articleDto.Title,
                Contents = articleDto.Contents,
                Tags = articleDto.Tags
            });

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to create article");
        }

        [HttpDelete("delete-user/{id}")]

        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(i => i.Id == id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete user");
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateUser(int id, UpdateUserDto userUpdate)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            _mapper.Map(userUpdate, user);

            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user");

        }
    }
}