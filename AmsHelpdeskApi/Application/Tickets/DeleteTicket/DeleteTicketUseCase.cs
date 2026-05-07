using AmsHelpdeskApi.Application.Tickets.Common;
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

        public Result<bool> Execute (int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);

            if (ticket == null)
            {
                return Result<bool>.Failure("Ticket não encontrado.");
            }

            if(ticket.Status != Ticket.TicketStatus.Open)
            {
                return Result<bool>.Failure("Só é possível deletar tickets abertos.");
            }

            _context.Tickets.Remove(ticket);
            _context.SaveChanges();

            return Result<bool>.Success(true);
        }
    }
}
