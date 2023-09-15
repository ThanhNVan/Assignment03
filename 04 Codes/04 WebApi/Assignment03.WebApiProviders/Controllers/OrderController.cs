using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.WebApiProviders;

namespace Assignment03.WebApiProviders;

public class OrderController : BaseWebApiController<Order, IOrderLogicProviders>
{
    #region [ CTor ]
    public OrderController(ILogger<OrderController> logger, IOrderLogicProviders logicProvider, LogicContext logicContext) : base(logger, logicProvider, logicContext) {
    }
    #endregion
}
