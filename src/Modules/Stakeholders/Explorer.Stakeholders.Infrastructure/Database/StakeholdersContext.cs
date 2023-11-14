using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Shopping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Explorer.Stakeholders.Infrastructure.Database;

public class StakeholdersContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<ClubInvitation> ClubInvitations { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<UserClub> UserClubs { get; set; }
    public DbSet<ClubRequest> Requests { get; set; }
    public DbSet<ApplicationGrade> ApplicationGrades { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<ObjectRequest> ObjectRequests { get; set; }
    public DbSet<CheckpointRequest> CheckpointRequests { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public StakeholdersContext(DbContextOptions<StakeholdersContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("stakeholders");

        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
		modelBuilder.Entity<UserClub>()
	        .HasKey(uc => new { uc.UserId, uc.ClubId });

		modelBuilder.Entity<UserClub>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<UserClub>()
            .HasOne<Club>()
            .WithMany()
            .HasForeignKey(uc => uc.ClubId);
    
        modelBuilder.Entity<Customer>()
           .Property(item => item.PurchaseTokens).HasColumnType("jsonb");

        modelBuilder.Entity<ShoppingCart>()
          .Property(item => item.Items).HasColumnType("jsonb");

        ConfigureStakeholder(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);

        modelBuilder.Entity<ShoppingCart>()
        .HasOne<User>()
        .WithMany()
        .HasForeignKey(s => s.TouristId)
        .IsRequired();

        modelBuilder.Entity<Customer>()
        .HasOne<User>()
        .WithMany()
        .HasForeignKey(s => s.TouristId)
        .IsRequired();

        /*
        modelBuilder.Entity<Customer>()
        .HasOne<ShoppingCart>()
        .WithMany()
        .HasForeignKey(s => s.ShoppingCartId)
        .IsRequired();
        */

        /*modelBuilder.Entity<Customer>()
           .HasOne<ShoppingCart>()
           .WithOne()
           .HasForeignKey<Customer>(s => s.ShoppingCartId);
        */

        /*modelBuilder.Entity<OrderItem>() 
        .HasOne<Tour>()
        .WithMany()
        .HasForeignKey(o => o.TourId)*/
    }
}
