using Assignment03.EntityProviders;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public interface IOrderLogicProviders : IBaseLogicProvider<Order>
{
    #region [ Methods - Checkout ]
    Task<string> CheckoutAsync(CheckoutModel model);
    #endregion
}
