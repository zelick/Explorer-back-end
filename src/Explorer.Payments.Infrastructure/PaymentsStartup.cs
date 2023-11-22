using Microsoft.Extensions.DependencyInjection;
using Explorer.Payments.Core.Mappers;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

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
        // DI for services
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        // DI for repos

        services.AddDbContext<PaymentsContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));
    }
}

