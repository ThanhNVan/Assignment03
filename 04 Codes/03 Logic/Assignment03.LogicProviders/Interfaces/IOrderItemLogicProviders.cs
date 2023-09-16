using Assignment03.EntityProviders;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public interface IOrderItemLogicProviders : IBaseLogicProvider<OrderItem>
{
    #region [ Methods - List ]
    Task<IList<OrderItem>> GetListByOrderIdAsync(string orderId);
    #endregion
}
