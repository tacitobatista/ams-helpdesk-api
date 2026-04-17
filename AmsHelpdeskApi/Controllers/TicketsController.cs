using AmsHelpdeskApi.Data;
using AmsHelpdeskApi.Models;
using AmsHelpdeskApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AmsHelpdeskApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketService _service;

        public TicketsController(TicketService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var tickets = _service.GetAll();
            return Ok(tickets);
        }

        [HttpPost]
        public IActionResult Create(Ticket ticket)
        {
            return Ok(_service.Create(ticket));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Ticket updatedTicket)
        {
            var ticket = _service.Update(id, updatedTicket);

            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _service.Delete(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{id}/assign/{userId}")]
        public IActionResult Assign(int id, int userId)
        {
            var ticket = _service.GetById(id);
            if(ticket == null)
            {
                return NotFound();
            }

            ticket.AssignedToUserId = userId;

            _service.Update(id, ticket);

            return Ok(ticket);
        }

        [Authorize]
        [HttpPut("{id}/take")]
        public IActionResult Take(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdClaim.Value);

            var ticket = _service.GetById(id);
            
            if(ticket == null)
            {
                return NotFound();
            }

            ticket.AssignedToUserId = userId;

            _service.Update(id, ticket);

            return Ok(ticket);
        }
    }
}
