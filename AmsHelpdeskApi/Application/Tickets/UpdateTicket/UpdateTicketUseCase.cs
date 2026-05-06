using AmsHelpdeskApi.Domain.Entities;
using AmsHelpdeskApi.Infrastructure.Data;

namespace AmsHelpdeskApi.Application.Tickets.UpdateTicket
{
    public class UpdateTicketUseCase
    {
        private readonly AppDbContext _context;

        public UpdateTicketUseCase(AppDbContext context)
        {
            _context = context;
        }

        public Ticket Execute (int ticketId, UpdateTicketRequest request)
        {
            var ticket = _context.Tickets.Find(ticketId);

            if (ticket == null)
            {
                return null;
            }

            if(ticket.Status == Ticket.TicketStatus.Closed)
            {
                throw new InvalidOperationException("Não é possível editar um ticket fechado.");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new InvalidOperationException("Título é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                throw new InvalidOperationException("Descrição é obrigatória.");
            }

            ticket.Title = request.Title;
            ticket.Description = request.Description;

            _context.SaveChanges();
            return ticket;
        }
    }
}
