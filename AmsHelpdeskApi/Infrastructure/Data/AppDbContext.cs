using Microsoft.EntityFrameworkCore;
using AmsHelpdeskApi.Domain.Entities;

namespace AmsHelpdeskApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
