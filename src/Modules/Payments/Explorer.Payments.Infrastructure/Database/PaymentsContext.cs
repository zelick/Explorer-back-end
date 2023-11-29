using Explorer.Payments.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database;

public class PaymentsContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Sale> Sales { get; set; }

    public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");

        modelBuilder.Entity<Customer>()
            .Property(item => item.TourPurchaseTokens).HasColumnType("jsonb");

        modelBuilder.Entity<ShoppingCart>()
            .Property(item => item.Items).HasColumnType("jsonb");

        ConfigurePayments(modelBuilder);
    }

    private static void ConfigurePayments(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasOne<ShoppingCart>()
            .WithOne()
            .HasForeignKey<Customer>(e => e.ShoppingCartId)
            .IsRequired();
    }
}