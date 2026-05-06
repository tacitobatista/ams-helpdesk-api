using AmsHelpdeskApi.Infrastructure.Data;
using AmsHelpdeskApi.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace AmsHelpdeskApi.Application.Tickets.CreateTicket
{
    public class CreateTicketUseCase
    {
        private readonly AppDbContext _context;

        public CreateTicketUseCase(AppDbContext context)
        {
            _context = context;
        }

        public Ticket Execute(CreateTicketRequest request, int userId)
        {
            var ticket = new Ticket
            {
                Title = request.Title,
                Description = request.Description,
                AssignedToUserId = userId,
                Status = Ticket.TicketStatus.Open
            };
            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            return ticket;
        }
    }
}
