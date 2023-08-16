using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public static class ServiceEntensions
{
    #region [ Methods -  ]
    public static void AddHttpClientProviders(this IServiceCollection services, IConfiguration configuration) {
        services.AddHttpClient(RoutingUrl.BaseClientName, clients => {
            clients.BaseAddress = new Uri(configuration["BaseUrl"]);
        });
    }
    #endregion
}
