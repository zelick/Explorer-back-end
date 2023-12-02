using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Mappers;
using Explorer.Encounters.Core.UseCases;
using Explorer.Encounters.Infrastructure.Database;
using Explorer.Encounters.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Encounters.Infrastructure
{
    public static class EncountersStartup
    {
        public static IServiceCollection ConfigureEncountersModule(this IServiceCollection services)
        {
            // Registers all profiles since it works on the assembly
            services.AddAutoMapper(typeof(EncountersProfile).Assembly);
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {
            services.AddScoped<IEncounterService, EncounterService>();
            //  services.AddScoped<IInternalCheckpointService, InternalCheckpointService>();
            services.AddScoped<IEncounterExecutionService, EncounterExecutionService>();
        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(IEncounterRepository), typeof(EncounterDatabaseRepository));
            services.AddScoped(typeof(IEncounterExecutionRepository), typeof(EncounterExecutionDatabaseRepository));
            services.AddScoped(typeof(ICrudRepository<SocialEncounter>), typeof(CrudDatabaseRepository<SocialEncounter, EncountersContext>));

            services.AddDbContext<EncountersContext>(opt =>
                opt.UseNpgsql(DbConnectionStringBuilder.Build("encounters"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "encounters")));
        }
    }
}
