using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment03.EntityProviders;

public static class ServiceExtensions
{
    public static void AddSqlServerProviders(this IServiceCollection services,
              IConfiguration configuration,
              string connectionStringKey = "DefaultConnection") {
        var connectionString = configuration.GetConnectionString(connectionStringKey);

        var options = new DbContextOptions<AppDbContext>();
        var builder = new DbContextOptionsBuilder<AppDbContext>(options);
        builder.UseSqlServer(connectionString);
        builder.EnableSensitiveDataLogging();

        services.AddPooledDbContextFactory<AppDbContext>(options => {
            options.UseSqlServer(connectionString,
                sqlServerOptionsAction => {
                    sqlServerOptionsAction.EnableRetryOnFailure();
                });
            options.EnableSensitiveDataLogging();
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        var adminEntity = new SignInModel();
        configuration.GetSection("Admin").Bind(adminEntity);

        var appSettingModel = new AppSettingModel();
        configuration.GetSection("AppSettingModel").Bind(appSettingModel);

        services.AddSingleton(adminEntity);
        services.AddSingleton(appSettingModel);
    }
}
