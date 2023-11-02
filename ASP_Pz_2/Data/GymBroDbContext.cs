using ASP_Pz_2.Entity;
using Microsoft.EntityFrameworkCore;

namespace ASP_Pz_2.Data
{
    public class GymBroDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<ClientMembership> ClientMemberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientMembership>()
                .HasKey(e => new { e.ClientId, e.MembershipId });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GymBroDb;Trusted_Connection=True;MultipleActiveResultSets=True");
        }
    }
}
