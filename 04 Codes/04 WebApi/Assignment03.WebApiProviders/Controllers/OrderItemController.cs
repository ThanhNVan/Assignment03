using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.WebApiProviders;

namespace Assignment03.WebApiProviders;

public class OrderItemController : BaseWebApiController<OrderItem, IOrderItemLogicProviders>
{
    #region [ CTor ]
    public OrderItemController(ILogger<OrderItemController> logger, IOrderItemLogicProviders logicProvider, LogicContext logicContext) : base(logger, logicProvider, logicContext) {
    }
    #endregion
}
