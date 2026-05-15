using AmsHelpdeskApi.Application.Tickets.AssignTicket;
using AmsHelpdeskApi.Application.Tickets.CreateTicket;
using AmsHelpdeskApi.Application.Tickets.DeleteTicket;
using AmsHelpdeskApi.Application.Tickets.GetTicket;
using AmsHelpdeskApi.Application.Tickets.TakeTicket;
using AmsHelpdeskApi.Application.Tickets.UpdateTicket;
using AmsHelpdeskApi.Domain.Entities;
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
            var result = _getUseCase.Execute();

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Create(CreateTicketRequest request)
        {
            if (!TryGetUserId(out var userId))
                return Unauthorized();

            var result = _createUseCase.Execute(request, userId);

            if(!result.IsSuccess)
                return BadRequest(new { message = result.Error });

            return CreatedAtAction(nameof(Get), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateTicketRequest request)
        {
            var result = _updateUseCase.Execute(id, request);

            if(!result.IsSuccess)
                return BadRequest(new { message = result.Error });

            return Ok(result.Data);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _deleteUseCase.Execute(id);

            if (!result.IsSuccess)
            {
                if (result.Error == "Ticket não encontrado.")
                {
                    return NotFound(new { message = result.Error });
                }

                return BadRequest(new { message = result.Error });
            }

            return NoContent();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id}/assign/{userId}")]
        public IActionResult Assign(int id, int userId)
        {
            var result = _assignUseCase.Execute(id, userId);

            if (!result.IsSuccess)
            {
                if (result.Error == "Ticket não encontrado.")
                {
                    return NotFound(new { message = result.Error });
                }

                return BadRequest(new { message = result.Error });
            }

            return Ok(result.Data);
        }

        [Authorize]
        [HttpPut("{id}/take")]
        public IActionResult Take(int id)
        {
            if(!TryGetUserId(out var userId))
            {
                return Unauthorized();
            }

            var result = _takeUseCase.Execute(id, userId);

            if (!result.IsSuccess)
            {
                if(result.Error == "Ticket não encontrado")
                {
                    return NotFound(new { message = result.Error });
                }

                return BadRequest(new { message = result.Error });
            }

            return Ok(result.Data);
        }

        private bool TryGetUserId(out int userId)
        {
            userId = 0;

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return false;

            return int.TryParse(userIdClaim.Value, out userId);
        }
    }
}
