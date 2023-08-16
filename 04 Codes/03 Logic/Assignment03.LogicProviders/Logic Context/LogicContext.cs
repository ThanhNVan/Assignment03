namespace Assignment03.LogicProviders;

public class LogicContext
{
	#region [ Properties ]
    public ICategoryLogicProviders Category { get; set; }
    public IOrderItemLogicProviders OrderItem { get; }
    public IOrderLogicProviders Order { get; }
    public IProductLogicProviders Product { get; }
    public IRefreshTokenLogicProviders RefreshToken { get; }
    public IUserLogicProviders User { get; }
    public IUserPhoneLogicProviders UserPhone { get; }
    #endregion

    #region [ CTor ]
    public LogicContext(ICategoryLogicProviders category,
						IOrderItemLogicProviders orderItem,
                        IOrderLogicProviders order,
                        IProductLogicProviders product,
                        IRefreshTokenLogicProviders refreshToken,
                        IUserLogicProviders user,
                        IUserPhoneLogicProviders userPhone) {
        Category = category;
        OrderItem = orderItem;
        Order = order;
        Product = product;
        RefreshToken = refreshToken;
        User = user;
        UserPhone = userPhone;
    }
    #endregion
}
