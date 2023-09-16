using Assignment03.EntityProviders;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public interface IOrderItemDataProviders : IBaseDataProvider<OrderItem>
{
    #region [ Methods - List ]
    Task<IList<OrderItem>> GetListByOrderIdAsync(string orderId);
    #endregion
}
