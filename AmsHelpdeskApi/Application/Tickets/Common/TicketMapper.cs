using AmsHelpdeskApi.Domain.Entities;

namespace AmsHelpdeskApi.Application.Tickets.Common
{
    public class TicketMapper
    {
        public static TicketResponse ToResponse(Ticket ticket)
        {
            return new TicketResponse
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status switch
                {
                    Ticket.TicketStatus.Open => "Open",
                    Ticket.TicketStatus.InProgress => "In Progress",
                    Ticket.TicketStatus.Closed => "Closed",
                    _ => "Unknown"
                },
                AssignedToUserId = ticket.AssignedToUserId
            };
        }
    }
}
