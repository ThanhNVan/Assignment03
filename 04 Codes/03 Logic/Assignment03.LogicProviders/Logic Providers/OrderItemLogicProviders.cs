using Assignment03.DataProviders;
using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

internal class OrderItemLogicProviders : BaseLogicProvider<OrderItem, IOrderItemDataProviders>, IOrderItemLogicProviders
{
    #region [ CTor ]
    public OrderItemLogicProviders(ILogger<BaseLogicProvider<OrderItem, IOrderItemDataProviders>> logger, IOrderItemDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion
}
