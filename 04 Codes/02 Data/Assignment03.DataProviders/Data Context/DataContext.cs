namespace Assignment03.DataProviders;

public class DataContext
{
	#region [ Properties ]
	public ICategoryDataProviders Category { get; set; }
    public IOrderDataProviders Order { get; }
    public IOrderItemDataProviders OrderItem { get; }
    public IProductDataProviders Product { get; }
    public IRefreshTokenDataProviders RefreshToken { get; }
    public IUserDataProviders User { get; }
    public IUserPhoneDataProviders UserPhone { get; }
    #endregion

    #region [ CTor ]
    public DataContext(ICategoryDataProviders category,
						IOrderDataProviders order,
                        IOrderItemDataProviders orderItem,
                        IProductDataProviders product,
                        IRefreshTokenDataProviders refreshToken,
                        IUserDataProviders user,
                        IUserPhoneDataProviders userPhone) {
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
