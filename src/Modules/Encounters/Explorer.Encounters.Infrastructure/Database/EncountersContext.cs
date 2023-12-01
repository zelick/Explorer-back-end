using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Explorer.Encounters.Infrastructure.Database
{
    public class EncountersContext : DbContext
    {
        public DbSet<Encounter> Encounter { get; set; }
        public DbSet<HiddenLocationEncounter> HiddenLocationEncounter { get; set; }
        public DbSet<SocialEncounter> SocialEncounter { get; set; }


        public EncountersContext(DbContextOptions<EncountersContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("encounters");

            modelBuilder.Entity<Encounter>().ToTable("Encounter");
            modelBuilder.Entity<HiddenLocationEncounter>().ToTable("HiddenLocationEncounter");
            modelBuilder.Entity<SocialEncounter>().ToTable("SocialEncounter");



            // modelBuilder.Entity<Encounter>().Property(item => item.HiddenLocationEncounter).HasColumnType("jsonb");
            modelBuilder.Entity<Encounter>().Property(item => item.CompletedEncounter).HasColumnType("jsonb");
            //modelBuilder.Entity<Encounter>().Property(item => item.SocialEncounter).HasColumnType("jsonb");



        }
    }
}
