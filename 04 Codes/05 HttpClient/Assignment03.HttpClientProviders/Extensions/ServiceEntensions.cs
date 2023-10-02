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

        services.AddTransient<ICategoryHttpClientProviders, CategoryHttpClientProviders>();
        services.AddTransient<IOrderHttpClientProviders, OrderHttpClientProviders>();
        services.AddTransient<IOrderItemHttpClientProviders, OrderItemHttpClientProviders>();
        services.AddTransient<IProductHttpClientProviders, ProductHttpClientProviders>();
        services.AddTransient<IRefreshTokenHttpClientProviders, RefreshTokenHttpClientProviders>();
        services.AddTransient<IUserHttpClientProviders, UserHttpClientProviders>();
        services.AddTransient<IUserPhoneHttpClientProviders, UserPhoneHttpClientProviders>();
        services.AddTransient<IEncriptionProvider, EncriptionProvider>();

        services.AddTransient<HttpClientContext>();
    }
    #endregion
}
