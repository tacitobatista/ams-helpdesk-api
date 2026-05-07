using AmsHelpdeskApi.Application.Tickets.Common;
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

        public Result<TicketResponse> Execute(int ticketId, int userId)
        {
            var ticket = _context.Tickets.Find(ticketId);

            if (ticket == null)
            {
                return Result<TicketResponse>.Failure("Ticket não encontrado.");
            }

            if (ticket.Status == Ticket.TicketStatus.Closed)
            {
                return Result<TicketResponse>.Failure("Não é possível assumir um ticket fechado.");
            }

            if (ticket.AssignedToUserId == userId)
            {
                return Result<TicketResponse>.Failure("Você já é responsável por este ticket.");
            }
                
            if (ticket.AssignedToUserId != null)
            {
                return Result<TicketResponse>.Failure("Ticket já está atribuído a outro usuário.");
            }

            ticket.AssignedToUserId = userId;

            _context.SaveChanges();

            return Result<TicketResponse>.Success(TicketMapper.ToResponse(ticket));
        }
    }
}
