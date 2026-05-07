using AmsHelpdeskApi.Application.Tickets.Common;
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

        public Result<TicketResponse> Execute (int ticketId, UpdateTicketRequest request)
        {
            var ticket = _context.Tickets.Find(ticketId);

            if (ticket == null)
            {
                return Result<TicketResponse>.Failure("Ticket não encontrado.");
            }

            if(ticket.Status == Ticket.TicketStatus.Closed)
            {
                return Result<TicketResponse>.Failure("Não é possível editar um ticket fechado.");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return Result<TicketResponse>.Failure("Título é obrigatório.");
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                return Result<TicketResponse>.Failure("Descrição é obrigatória.");
            }

            ticket.Title = request.Title;
            ticket.Description = request.Description;

            _context.SaveChanges();
            return Result<TicketResponse>.Success(TicketMapper.ToResponse(ticket));
        }
    }
}
