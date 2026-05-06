using AmsHelpdeskApi.Application.Tickets.AssignTicket;
using AmsHelpdeskApi.Application.Tickets.CreateTicket;
using AmsHelpdeskApi.Application.Tickets.DeleteTicket;
using AmsHelpdeskApi.Application.Tickets.GetTicket;
using AmsHelpdeskApi.Application.Tickets.TakeTicket;
using AmsHelpdeskApi.Application.Tickets.UpdateTicket;
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
        private readonly CreateTicketUseCase _createUseCase;
        private readonly TakeTicketUseCase _takeUseCase;
        private readonly AssignTicketUseCase _assignUseCase;
        private readonly UpdateTicketUseCase _updateUseCase;
        private readonly DeleteTicketUseCase _deleteUseCase;
        private readonly GetTicketUseCase _getUseCase;

        public TicketsController(CreateTicketUseCase createUseCase, TakeTicketUseCase takeUseCase, AssignTicketUseCase assignUseCase, UpdateTicketUseCase updateUseCase, DeleteTicketUseCase deleteUseCase, GetTicketUseCase getUseCase)
        {
            _createUseCase = createUseCase;
            _takeUseCase = takeUseCase;
            _assignUseCase = assignUseCase;
            _updateUseCase = updateUseCase;
            _deleteUseCase = deleteUseCase;
            _getUseCase = getUseCase;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var tickets = _getUseCase.Execute();
            return Ok(tickets);
        }

        [HttpPost]
        public IActionResult Create(CreateTicketRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            if(!int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            var result = _createUseCase.Execute(request, userId);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateTicketRequest request)
        {
            try
            {
                var result = _updateUseCase.Execute(id, request);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var success = _deleteUseCase.Execute(id);

                if (!success)
                {
                    return NotFound();
                }
                return NoContent();
            }

            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }            
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{id}/assign/{userId}")]
        public IActionResult Assign(int id, int userId)
        {
            try
            {
                var result = _assignUseCase.Execute(id, userId);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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

            if(!int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized();
            }

            try
            {
                var result = _takeUseCase.Execute(id, userId);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }

            catch(InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
