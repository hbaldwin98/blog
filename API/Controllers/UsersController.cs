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
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(DataContext context, IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _mapper = mapper;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetUsers() 
        {
            var users = await _unitOfWork.UserRespository.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id) 
        {
            var user = await _unitOfWork.UserRespository.GetMemberByIdAsync(id);

            if (user == null) return NotFound("User does not exist");

            return Ok(user);
        }


        [Authorize]
        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(i => i.Id == id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);

            if (await _unitOfWork.Update()) return Ok();

            return BadRequest("Failed to delete user");
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateUser(int id, UpdateUserDto userUpdate)
        {
            var user = await _unitOfWork.UserRespository.GetUserByNameAsync(User.GetUsername());

            _mapper.Map(userUpdate, user);

            _unitOfWork.UserRespository.Update(user);

            if (await _unitOfWork.Update()) return NoContent();

            return BadRequest("Failed to update user");
        }
    }
}