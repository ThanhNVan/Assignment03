using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLibrary.EntityProviders;
using SharedLibrary.WebApiProviders;

namespace Assignment03.WebApiProviders;

public class OrderItemController : BaseWebApiController<OrderItem, IOrderItemLogicProviders>
{
    #region [ CTor ]
    public OrderItemController(ILogger<OrderItemController> logger, IOrderItemLogicProviders logicProvider, LogicContext logicContext) : base(logger, logicProvider, logicContext) {
    }
    #endregion

    #region [ Methods - List ]
    [HttpGet(nameof(MethodUrl.GetListByOrderId) + "/{orderId}")]
    public async Task<IActionResult> GetListByOrderIdAsync(string orderId)
    {
        try
        {
            var result = await this._logicProvider.GetListByOrderIdAsync(orderId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        } catch (ArgumentNullException ex)
        {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    #endregion
}
