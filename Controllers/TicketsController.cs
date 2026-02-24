using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend_Project.Data;
using Backend_Project.DTOs;
using Backend_Project.Enums;
using Backend_Project.Models;
using System.Security.Claims;

namespace Backend_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE TICKET
        [Authorize(Roles = "USER,MANAGER")]
        [HttpPost]
        public IActionResult Create(CreateTicketDto dto)
        {
            if (dto.Title.Length < 5 || dto.Description.Length < 10)
                return BadRequest("Validation failed");

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var ticket = new Ticket
            {
                Title = dto.Title,
                Description = dto.Description,
                CreatedBy = userId,
                Priority = dto.Priority
            };

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return StatusCode(201, ticket);
        }

        // GET ALL
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Tickets.ToList());
        }

        // CHANGE STATUS
        [Authorize(Roles = "MANAGER,SUPPORT")]
        [HttpPatch("{id}/status")]
        public IActionResult ChangeStatus(int id, ChangeStatusDto dto)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null) return NotFound();

            if ((int)dto.NewStatus != (int)ticket.Status + 1)
                return BadRequest("Invalid status transition");

            var oldStatus = ticket.Status;
            ticket.Status = dto.NewStatus;

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            _context.TicketStatusLogs.Add(new TicketStatusLog
            {
                TicketId = id,
                OldStatus = oldStatus,
                NewStatus = dto.NewStatus,
                ChangedBy = userId
            });

            _context.SaveChanges();

            return NoContent();
        }
    }
}