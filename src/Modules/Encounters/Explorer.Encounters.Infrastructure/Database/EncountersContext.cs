using Explorer.Encounters.Core.Domain.Encounters;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Encounters.Infrastructure.Database
{
    public class EncountersContext : DbContext
    {
        public DbSet<Encounter> Encounter { get; set; }
        public DbSet<EncounterRequest> EncounterRequests { get; set; }

        public EncountersContext(DbContextOptions<EncountersContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("encounters");

            modelBuilder.Entity<Encounter>().Property(item => item.HiddenLocationEncounter).HasColumnType("jsonb");
            modelBuilder.Entity<Encounter>().Property(item => item.CompletedEncounter).HasColumnType("jsonb");
            modelBuilder.Entity<Encounter>().Property(item => item.SocialEncounter).HasColumnType("jsonb");



        }
    }
}
