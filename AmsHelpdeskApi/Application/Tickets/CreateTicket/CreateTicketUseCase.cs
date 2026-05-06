using AmsHelpdeskApi.Infrastructure.Data;
using AmsHelpdeskApi.Domain.Entities;
using AmsHelpdeskApi.Application.Tickets.Common;
using AmsHelpdeskApi.Application.Tickets.UpdateTicket;

namespace AmsHelpdeskApi.Application.Tickets.CreateTicket
{
    public class CreateTicketUseCase
    {
        private readonly AppDbContext _context;

        public CreateTicketUseCase(AppDbContext context)
        {
            _context = context;
        }

        public TicketResponse Execute(CreateTicketRequest request,int ticketId)
        {
            var ticket = _context.Tickets.Find(ticketId);

            if (ticket == null)
            {
                return null;
            }

            if (ticket.Status == Ticket.TicketStatus.Closed)
            {
                throw new InvalidOperationException("Não é possível atualizar um ticket fechado.");
            }

            ticket.Title = request.Title;
            ticket.Description = request.Description;

            _context.SaveChanges();

            return TicketMapper.ToResponse(ticket);
        }
    }
}
