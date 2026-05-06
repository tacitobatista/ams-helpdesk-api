using AmsHelpdeskApi.Domain.Entities;
using AmsHelpdeskApi.Infrastructure.Data;

namespace AmsHelpdeskApi.Application.Tickets.DeleteTicket
{
    public class DeleteTicketUseCase
    {
        private readonly AppDbContext _context;

        public DeleteTicketUseCase(AppDbContext context)
        {
            _context = context;
        }

        public bool Execute (int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);

            if (ticket == null)
            {
                return false;
            }

            if(ticket.Status != Ticket.TicketStatus.Open)
            {
                throw new InvalidOperationException("Só é possível deletar tickets abertos.");
            }

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();

            return true;
        }
    }
}
