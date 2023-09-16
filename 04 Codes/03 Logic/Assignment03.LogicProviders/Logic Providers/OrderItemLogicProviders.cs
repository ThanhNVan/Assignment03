using Assignment03.DataProviders;
using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

internal class OrderItemLogicProviders : BaseLogicProvider<OrderItem, IOrderItemDataProviders>, IOrderItemLogicProviders
{
    #region [ CTor ]
    public OrderItemLogicProviders(ILogger<OrderItemLogicProviders> logger, IOrderItemDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion

    #region [ Methods - List ]
    public async Task<IList<OrderItem>> GetListByOrderIdAsync(string orderId)
    {
        var result = default(IList<OrderItem>);

        if (string.IsNullOrEmpty(orderId))
        {
            return result;
        }

        result = await this._dataProvider.GetListByOrderIdAsync(orderId);

        return result;
    }
    #endregion
}
