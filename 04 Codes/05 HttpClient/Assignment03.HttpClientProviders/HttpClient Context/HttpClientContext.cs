namespace Assignment03.HttpClientProviders;

public class HttpClientContext
{
    #region [ Properties ]
    public ICategoryHttpClientProviders Category { get; set; }
    public IOrderHttpClientProviders Order { get; set; }
    public IOrderItemHttpClientProviders OrderItem { get; set; }
    public IProductHttpClientProviders Product { get; set; }
    public IRefreshTokenHttpClientProviders RefreshToken { get; set; }
    public IUserHttpClientProviders User { get; }
    public IUserPhoneHttpClientProviders UserPhone { get; }

    #endregion

    #region [ CTor ]
    public HttpClientContext(ICategoryHttpClientProviders category,
                                IOrderHttpClientProviders order,
                                IOrderItemHttpClientProviders orderItem,
                                IProductHttpClientProviders product,
                                IRefreshTokenHttpClientProviders refreshToken,
                                IUserHttpClientProviders user, 
                                IUserPhoneHttpClientProviders userPhone) {
        Category = category;
        Order = order;
        OrderItem = orderItem;
        Product = product;
        RefreshToken = refreshToken;
        User = user;
        UserPhone = userPhone;
    }
    #endregion
}
