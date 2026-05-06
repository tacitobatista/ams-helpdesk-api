using AmsHelpdeskApi.Domain.Entities;
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

    public List<Ticket> Execute()
        {
            return _context.Tickets.ToList();
        }
    }
}
