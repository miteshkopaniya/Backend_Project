using Microsoft.EntityFrameworkCore;
using Backend_Project.Models;

namespace Backend_Project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketStatusLog> TicketStatusLogs { get; set; }
    }
}