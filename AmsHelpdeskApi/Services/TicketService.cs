using AmsHelpdeskApi.Data;
using AmsHelpdeskApi.Models;

namespace AmsHelpdeskApi.Services
{
    public class TicketService
    {
        private readonly AppDbContext _context;
        
        public TicketService(AppDbContext context)
        {
            _context = context;
        }

        public List<Ticket> GetAll()
        {
            return _context.Tickets.ToList();
        }

        public Ticket GetById(int id)
        {
            return _context.Tickets.Find(id);
        }

        public Ticket Create(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
            _context.SaveChanges();
            return ticket;
        }

        public Ticket Update(int id, Ticket updated)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return null;
            }

            ticket.Title = updated.Title;
            ticket.Description = updated.Description;
            ticket.Status = updated.Status;
            ticket.AssignedToUserId = updated.AssignedToUserId;

            _context.SaveChanges();
            return ticket;
        }

        public bool Delete(int id)
        {
            var ticket = _context.Tickets.Find(id);
            if (ticket == null)
            {
                return false;
            }
            _context.Tickets.Remove(ticket);
            _context.SaveChanges();
            return true;
        }
    }
}
