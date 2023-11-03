using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

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
    //public DbSet<UserFollower> UserFollower { get; set; }

    public DbSet<Message> Messages { get; set; }

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

        //User follower
        /*
        modelBuilder.Entity<UserFollower>()
            .HasKey(uf => new { uf.UserId, uf.FollowerId });
        */

        modelBuilder.Entity<User>()
            .HasMany(u => u.Followers)
            .WithMany()
            .UsingEntity(j =>
            {
                j.ToTable("UserFollowers"); // Set the name of the join table
            }); ;

        // Configure the many-to-many relationship
        /*
        modelBuilder.Entity<User>()
            .HasMany(u => u.Followers)
            .WithMany()
            .UsingEntity<UserFollower>();
        */

        /*
        modelBuilder.Entity<UserFollower>()
            .HasKey(uf => new { uf.UserId, uf.FollowerId });
        */

        //Massage table
        //modelBuilder.Entity<Message>().HasOne<User>()
        //.WithMany()
        //.HasForeignKey(m => m.SenderId);
        //modelBuilder.Entity<Message>()
        //  .Property<long>("SenderId");
        //modelBuilder.Entity<Message>()
        //.HasOne<User>()
        //.WithMany()
        //.HasForeignKey("SenderId");

        modelBuilder.Entity<Message>()
            .HasOne<User>()
            .WithMany(u => u.Messages)
            .HasForeignKey(m => m.SenderId);

        //modelBuilder.Entity<Message>().HasOne<User>()
        //.WithMany()
        //.HasForeignKey("SenderId")
        //.HasPrincipalKey("Id");


        ConfigureStakeholder(modelBuilder);
    }

    private static void ConfigureStakeholder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne<User>()
            .WithOne()
            .HasForeignKey<Person>(s => s.UserId);
    }
}
