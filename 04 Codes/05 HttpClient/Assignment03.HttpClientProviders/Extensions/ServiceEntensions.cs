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

        services.AddScoped<ICategoryHttpClientProviders, CategoryHttpClientProviders>();
        services.AddScoped<IOrderHttpClientProviders, OrderHttpClientProviders>();
        services.AddScoped<IOrderItemHttpClientProviders, OrderItemHttpClientProviders>();
        services.AddScoped<IProductHttpClientProviders, ProductHttpClientProviders>();
        services.AddScoped<IRefreshTokenHttpClientProviders, RefreshTokenHttpClientProviders>();
        services.AddScoped<IUserHttpClientProviders, UserHttpClientProviders>();
        services.AddScoped<IUserPhoneHttpClientProviders, UserPhoneHttpClientProviders>();
        services.AddScoped<IEncriptionProvider, EncriptionProvider>();

        services.AddScoped<HttpClientContext>();
    }
    #endregion
}
