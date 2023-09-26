using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLibrary.EntityProviders;
using SharedLibrary.WebApiProviders;

namespace Assignment03.WebApiProviders;

public class OrderController : BaseWebApiController<Order, IOrderLogicProviders>
{
    #region [ CTor ]
    public OrderController(ILogger<OrderController> logger, IOrderLogicProviders logicProvider, LogicContext logicContext) : base(logger, logicProvider, logicContext)
    {
    }
    #endregion

    #region [ Methods - Checkout ]
    [HttpPost(nameof(MethodUrl.Checkout))]
    public async Task<IActionResult> CheckoutAsync([FromBody] CheckoutModel model)
    {
        try
        {
            var result = await this._logicProvider.CheckoutAsync(model);
            if (!string.IsNullOrEmpty(result))
            {
                return Ok(new IntKeyValueModel()
                {
                    Key = 1,
                    Value = result
                });
            }

            return BadRequest(new IntKeyValueModel()
            {
                Key = 0,
                Value = result
            });
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
