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

        public Result<TicketResponse> Execute(int ticketId, int targetUserId)
        {
            var ticket = _context.Tickets.Find(ticketId);

            if (ticket == null)
            {
                return Result<TicketResponse>.Failure("Ticket não encontrado.");
            }

            if (ticket.Status == Ticket.TicketStatus.Closed)
            {
                return Result<TicketResponse>.Failure("Não é possível atribuir um ticket fechado.");
            }

            var user = _context.Users.Find(targetUserId);

            if (user == null)
            {
                return Result<TicketResponse>.Failure("Usuário destino não encontrado.");
            }

            if (ticket.AssignedToUserId == targetUserId)
            {
                return Result<TicketResponse>.Failure("Ticket já está atribuído para este usuário.");
            }

            ticket.AssignedToUserId = targetUserId;

            _context.SaveChanges();

            return Result<TicketResponse>.Success(TicketMapper.ToResponse(ticket));
        }
    }
}
