using Assignment03.DataProviders;
using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public class OrderLogicProviders : BaseLogicProvider<Order, IOrderDataProviders>, IOrderLogicProviders
{
    #region [ CTor ]
    public OrderLogicProviders(ILogger<BaseLogicProvider<Order, IOrderDataProviders>> logger, IOrderDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion
}
