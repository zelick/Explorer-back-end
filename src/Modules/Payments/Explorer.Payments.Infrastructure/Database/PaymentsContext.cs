using Explorer.Payments.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Payments.Infrastructure.Database;

public class PaymentsContext : DbContext
{
    public DbSet<TourPurchaseToken> PurchaseTokens { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<PaymentRecord> PaymentRecords { get; set; }

    public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");

        modelBuilder.Entity<ShoppingCart>()
            .Property(item => item.Items).HasColumnType("jsonb");

        ConfigurePayments(modelBuilder);
    }
    private static void ConfigurePayments(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasIndex(c => c.Code).IsUnique();

        modelBuilder.Entity<Item>()
            .HasIndex(i => new { i.ItemId, i.Type })
            .IsUnique();
    }
}