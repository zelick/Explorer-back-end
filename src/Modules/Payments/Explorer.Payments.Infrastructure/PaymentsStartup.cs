using Microsoft.Extensions.DependencyInjection;
using Explorer.Payments.Core.Mappers;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Explorer.Payments.API.Internal;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Infrastructure.Database.Repositories;

namespace Explorer.Payments.Infrastructure;

public static class PaymentsStartup
{
    public static IServiceCollection ConfigurePaymentsModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(PaymentsProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IItemOwnershipService, ItemOwnershipService>();
        services.AddScoped<IInternalTourOwnershipService, ItemOwnershipService>();
        services.AddScoped<IInternalShoppingSetupService, ShoppingSetupService>();
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped<ITourPurchaseTokenRepository, TourPurchaseTokenDatabaseRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingCartDatabaseRepository>();

        services.AddDbContext<PaymentsContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));
    }
}