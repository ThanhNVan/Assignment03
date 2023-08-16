using Microsoft.Extensions.DependencyInjection;

namespace Assignment03.DataProviders;

public static class ServiceEntensions
{
    #region [ Methods -  ]
    public static void AddDataProviders(this IServiceCollection services) {
        services.AddTransient<ICategoryDataProviders, CategoryDataProviders>();
        services.AddTransient<IOrderDataProviders, OrderDataProviders>();
        services.AddTransient<IOrderItemDataProviders, OrderItemDataProviders>();
        services.AddTransient<IProductDataProviders, ProductDataProviders>();
        services.AddTransient<IRefreshTokenDataProviders, RefreshTokenDataProviders>();
        services.AddTransient<IUserDataProviders, UserDataProviders>();
        services.AddTransient<IUserPhoneDataProviders, UserPhoneDataProviders>();

        services.AddTransient<DataContext>();
    }
    #endregion
}
