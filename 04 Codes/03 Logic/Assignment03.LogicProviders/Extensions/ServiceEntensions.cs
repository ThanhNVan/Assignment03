using Microsoft.Extensions.DependencyInjection;

namespace Assignment03.LogicProviders;

public static class ServiceEntensions
{
    #region [ Methods -  ]
    public static void AddLogicProviders(this IServiceCollection services) {
        services.AddTransient<ICategoryLogicProviders, CategoryLogicProviders>();
        services.AddTransient<IOrderItemLogicProviders, OrderItemLogicProviders>();
        services.AddTransient<IOrderLogicProviders, OrderLogicProviders>();
        services.AddTransient<IProductLogicProviders, ProductLogicProviders>();
        services.AddTransient<IRefreshTokenLogicProviders, RefreshTokenLogicProviders>();
        services.AddTransient<IUserLogicProviders, UserLogicProviders>();
        services.AddTransient<IUserPhoneLogicProviders, UserPhoneLogicProviders>();

        services.AddTransient<LogicContext>();
    }
    #endregion
}
