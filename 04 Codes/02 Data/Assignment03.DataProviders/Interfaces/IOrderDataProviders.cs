using Assignment03.EntityProviders;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public interface IOrderDataProviders : IBaseDataProvider<Order>
{
    #region [ Methods - Checkout ]
    Task<string> CheckoutAsync(CheckoutModel model);
    #endregion
}
