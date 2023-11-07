using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<MapObject> MapObjects { get; set; }
    public DbSet<Checkpoint> Checkpoints { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<TourEquipment> TourEquipment { get; set; }
    public DbSet<TourPreference> TourPreference { get; set; }
    public DbSet<ReportedIssue> ReportedIssues { get; set; }
    public DbSet<TourRating> TourRating { get; set; }
    public DbSet<TouristPosition> TouristPosition { get; set; }
    public DbSet<TourExecution> TourExecution { get; set; }



    public ToursContext(DbContextOptions<ToursContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        modelBuilder.Entity<Tour>()
           .HasMany(t => t.Checkpoints)
           .WithOne(t => t.Tour)
           .HasForeignKey(t => t.TourId)
           .IsRequired();


        modelBuilder.Entity<TourExecution>()
           .HasMany(t => t.CompletedCheckpoints)
           .WithOne()
           .HasForeignKey(t => t.TourExecutionId)
           .IsRequired();

        modelBuilder.Entity<TourExecution>()
            .HasOne(t => t.Tour)
            .WithMany()
            .HasForeignKey(t => t.TourId)
            .IsRequired();

        //ConfigureReportedIssues(modelBuilder);
        //ConfigureTourRatings(modelBuilder);

        modelBuilder.Entity<Tour>()
            .HasMany(t => t.Equipment)
            .WithMany()
            .UsingEntity<TourEquipment>();
        ConfigureReportedIssues(modelBuilder);
        ConfigureTourRatings(modelBuilder);

        modelBuilder.Entity<Tour>()
           .Property(item => item.PublishedTours).HasColumnType("jsonb");
        modelBuilder.Entity<Tour>()
           .Property(item => item.ArchivedTours).HasColumnType("jsonb");
        modelBuilder.Entity<Tour>()
           .Property(item => item.TourTimes).HasColumnType("jsonb");
    }

    private static void ConfigureReportedIssues(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReportedIssue>().HasOne(t => t.Tour).WithMany().HasForeignKey(t => t.TourId);
    }
    private static void ConfigureTourRatings(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<TourRating>().HasOne(t => t.Tour).WithMany().HasForeignKey(t => t.TourId);
        modelBuilder.Entity<Tour>().HasMany(t => t.TourRatings).WithOne(t=>t.Tour).HasForeignKey(t => t.TourId).IsRequired();
    }
}