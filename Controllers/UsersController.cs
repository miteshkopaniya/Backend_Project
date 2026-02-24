using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend_Project.Data;
using Backend_Project.DTOs;
using Backend_Project.Models;

namespace Backend_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "MANAGER")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(CreateUserDto dto)
        {
            var role = _context.Roles.FirstOrDefault(x => x.Name == dto.Role);
            if (role == null) return BadRequest("Invalid role");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = role.Id
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return StatusCode(201, "User Created");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Users.ToList());
        }
    }
}