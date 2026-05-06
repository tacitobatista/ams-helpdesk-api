using AmsHelpdeskApi.Application.Tickets.Common;
using AmsHelpdeskApi.Infrastructure.Data;

namespace AmsHelpdeskApi.Application.Tickets.TakeTicket
{
    public class TakeTicketUseCase
    {
        private readonly AppDbContext _context;

        public TakeTicketUseCase(AppDbContext context)
        {
            _context = context;
        }

        public TicketResponse Execute(int ticketId, int userId)
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

            ticket.AssignedToUserId = userId;

            _context.SaveChanges();
            return TicketMapper.ToResponse(ticket);
        }
    }
}
