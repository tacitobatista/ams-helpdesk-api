using AmsHelpdeskApi.Application.Tickets.Common;
using AmsHelpdeskApi.Infrastructure.Data;

namespace AmsHelpdeskApi.Application.Tickets.GetTicket
{
    public class GetTicketUseCase
    {
        private readonly AppDbContext _context;

        public GetTicketUseCase(AppDbContext context)
        {
            _context = context;
        }

    public Result<List<TicketResponse>> Execute()
        {
            var tickets = _context.Tickets.Select(ticket => TicketMapper.ToResponse(ticket)).ToList();

            return Result<List<TicketResponse>>.Success(tickets);
        }
    }
}
