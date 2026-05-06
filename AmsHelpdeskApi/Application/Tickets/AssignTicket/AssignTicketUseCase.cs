using AmsHelpdeskApi.Application.Tickets.Common;
using AmsHelpdeskApi.Domain.Entities;
using AmsHelpdeskApi.Infrastructure.Data;

namespace AmsHelpdeskApi.Application.Tickets.AssignTicket
{
    public class AssignTicketUseCase
    {
        private readonly AppDbContext _context;

        public AssignTicketUseCase(AppDbContext context)
        {
            _context = context;
        }

        public TicketResponse Execute(int ticketId, int targetUserId)
        {
            var ticket = _context.Tickets.Find(ticketId);
            if (ticket == null)
            {
                return null;
            }
            if(ticket.AssignedToUserId != null)
            {
                throw new InvalidOperationException("Ticket já está atribuído a outro usuário.");
            }

            ticket.AssignedToUserId = targetUserId;
            _context.SaveChanges();
            return TicketMapper.ToResponse(ticket);
        }
    }
}
