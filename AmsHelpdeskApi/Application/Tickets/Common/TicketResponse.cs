namespace AmsHelpdeskApi.Application.Tickets.Common
{
    public class TicketResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int? AssignedToUserId { get; set; }
    }
}
