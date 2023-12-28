using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Encounters.API.Internal;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain.Encounters;
using Explorer.Encounters.Core.Domain.RepositoryInterfaces;
using Explorer.Encounters.Core.Mappers;
using Explorer.Encounters.Core.UseCases;
using Explorer.Encounters.Infrastructure.Database;
using Explorer.Encounters.Infrastructure.Database.Repositories;
using Explorer.Stakeholders.Core.Domain;
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
            services.AddScoped<IEncounterRequestService, EncounterRequestService>();
            //  services.AddScoped<IInternalCheckpointService, InternalCheckpointService>();
            services.AddScoped<IInternalEncounterExecutionService, EncounterExecutionService>();
            services.AddScoped<IEncounterExecutionService, EncounterExecutionService>();
        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            services.AddScoped(typeof(ICrudRepository<EncounterRequest>), typeof(CrudDatabaseRepository<EncounterRequest, EncountersContext>));
            services.AddScoped(typeof(IEncounterRepository), typeof(EncounterDatabaseRepository));
            services.AddScoped<IEncounterRequestRepository, EncounterRequestDatabaseRepository>();
            services.AddScoped(typeof(IEncounterExecutionRepository), typeof(EncounterExecutionDatabaseRepository));
            services.AddScoped(typeof(ICrudRepository<SocialEncounter>), typeof(CrudDatabaseRepository<SocialEncounter, EncountersContext>));
            services.AddScoped(typeof(ICrudRepository<HiddenLocationEncounter>), typeof(CrudDatabaseRepository<HiddenLocationEncounter, EncountersContext>));
            services.AddScoped(typeof(ISocialEncounterRepository), typeof(SocialEncounterDatabaseRepository));
            services.AddScoped(typeof(IHiddenLocationEncounterRepository), typeof(HiddenLocationEncounterDatabaseRepository));

            services.AddDbContext<EncountersContext>(opt =>
                opt.UseNpgsql(DbConnectionStringBuilder.Build("encounters"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "encounters")));
        }
    }
}
