using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Mappers;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Stakeholders.Infrastructure;

public static class StakeholdersStartup
{
    public static IServiceCollection ConfigureStakeholdersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StakeholderProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenGenerator, JwtGenerator>();
        services.AddScoped<IClubRequestService, ClubRequestService>();
        services.AddScoped<IClubService, ClubService>();
        services.AddScoped<IClubInvitationService, ClubInvitationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IApplicationGradeService, ApplicationGradeService>();
        services.AddScoped<IPersonEditingService, PersonEditingService>();
        services.AddScoped<IAccountsManagementService, AccountsManagementService>();
        services.AddScoped<ISocialProfileService, SocialProfileService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IInternalNotificationService, InternalNotificationService>();
        services.AddScoped<IPersonRepository, PersonDatabaseRepository>();
        services.AddScoped<IObjectRequestService, ObjectRequestService>();
        services.AddScoped<ICheckpointRequestService, CheckpointRequestService>();
        services.AddScoped<IInternalObjectRequestService, ObjectRequestService>();
        services.AddScoped<IInternalCheckpointRequestService, CheckpointRequestService>();
        services.AddScoped<IInternalPersonService, InternalPersonService>();
        services.AddScoped<IInternalUserService, UserService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IVerificationService, VerificationService>();
        services.AddScoped<IInternalTouristService, InternalTouristService>();
        services.AddScoped<ITouristService, TouristService>();
        services.AddScoped<IInternalFollowersService, InternalFollowersService>();
    }


    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<ApplicationGrade>), typeof(CrudDatabaseRepository<ApplicationGrade, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, StakeholdersContext>));
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped<IUserClubRepository, UserClubDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<ClubInvitation>), typeof(CrudDatabaseRepository<ClubInvitation, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<ClubRequest>), typeof(CrudDatabaseRepository<ClubRequest, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, StakeholdersContext>));
        services.AddScoped<IClubRepository, ClubDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<Notification>), typeof(CrudDatabaseRepository<Notification, StakeholdersContext>));
        services.AddScoped(typeof(INotificationRepository), typeof(NotificationDatabaseRepository));
        services.AddScoped(typeof(ICrudRepository<ObjectRequest>), typeof(CrudDatabaseRepository<ObjectRequest, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<CheckpointRequest>), typeof(CrudDatabaseRepository<CheckpointRequest, StakeholdersContext>));
        services.AddScoped<IObjectRequestRepository, ObjectRequestDatabaseRepository>();
        services.AddScoped<ICheckpointRequestRepository, CheckpointRequestDatabaseRepository>();
        services.AddScoped<IPersonRepository, PersonDatabaseRepository>();
        services.AddScoped(typeof(ICrudRepository<Message>), typeof(CrudDatabaseRepository<Message, StakeholdersContext>));
        services.AddScoped(typeof(IMessageRepository), typeof(MessageDatabaseRepository));
        services.AddScoped(typeof(ISocialProfileRepository), typeof(SocialProfileDatabaseRepository));
        services.AddScoped(typeof(IVerificationTokenRepository), typeof(VerificationTokenDatabaseRepository));
        services.AddScoped(typeof(ITouristRepository), typeof(TouristDatabaseRepository));

        services.AddDbContext<StakeholdersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("stakeholders"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stakeholders")));
    }
}