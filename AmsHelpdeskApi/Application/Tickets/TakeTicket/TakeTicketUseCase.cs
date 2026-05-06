using AmsHelpdeskApi.Domain.Entities;
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

        public Ticket Execute(int ticketId, int userId)
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
            return ticket;
        }
    }
}
