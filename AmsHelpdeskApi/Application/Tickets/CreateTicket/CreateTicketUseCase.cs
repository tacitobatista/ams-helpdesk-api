using AmsHelpdeskApi.Application.Tickets.Common;
using AmsHelpdeskApi.Application.Tickets.UpdateTicket;
using AmsHelpdeskApi.Domain.Entities;
using AmsHelpdeskApi.Infrastructure.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AmsHelpdeskApi.Application.Tickets.CreateTicket
{
    public class CreateTicketUseCase
    {
        private readonly AppDbContext _context;

        public CreateTicketUseCase(AppDbContext context)
        {
            _context = context;
        }

        public Result<TicketResponse> Execute(CreateTicketRequest request,int userId)
        {

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Title))
                errors.Add("Título é obrigatório.");

            if (string.IsNullOrWhiteSpace(request.Description))
                errors.Add("Descrição é obrigatória.");

            if (errors.Any())
                return Result<TicketResponse>.Failure(string.Join(" | ", errors));

            var ticket = new Ticket
            {
                Title = request.Title,
                Description = request.Description,
                AssignedToUserId = userId,
                Status = Ticket.TicketStatus.Open
            };

            _context.Tickets.Add(ticket);

            var changes = _context.SaveChanges();

            if (changes == 0)
                return Result<TicketResponse>.Failure("Erro ao salvar ticket.");

            return Result<TicketResponse>.Success(TicketMapper.ToResponse(ticket));
        }
    }
}
