using Microsoft.Extensions.DependencyInjection;

namespace Assignment03.BlazorWebApp;

public static class ServiceExtension
{
    #region [ Methods -  ]
    public static void AddLocalService(this IServiceCollection services) { 
        services.AddTransient<ICartLocalStorageService, CartLocalStorageService>();
    }
    #endregion
}
