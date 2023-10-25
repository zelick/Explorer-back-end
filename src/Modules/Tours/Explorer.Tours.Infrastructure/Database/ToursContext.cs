using Explorer.Tours.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Tours.Infrastructure.Database;

public class ToursContext : DbContext
{
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<TourRating> TourRating { get; set; }

    public ToursContext(DbContextOptions<ToursContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("tours");

        //ConfigureTourRatings(modelBuilder);
    }
    //private static void ConfigureTourRatings(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<TourRating>().HasOne(t => t.Tour).WithMany().HasForeignKey(t => t.TourId);
    //}
}