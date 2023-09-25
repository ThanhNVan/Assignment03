using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLibrary.EntityProviders;
using SharedLibrary.WebApiProviders;

namespace Assignment03.WebApiProviders;

public class ProductController : BaseWebApiController<Product, IProductLogicProviders>
{
    #region [ CTor ]
    public ProductController(ILogger<ProductController> logger, IProductLogicProviders logicProvider, LogicContext logicContext) : base(logger, logicProvider, logicContext) {
    }
    #endregion

    #region [ Methods - List ]
    [HttpGet(nameof(MethodUrl.GetListByCategoryId) + "/{categoryId}")]
    public async Task<IActionResult> GetListByOrderIdAsync(string categoryId)
    {
        try
        {
            var result = await this._logicProvider.GetListByOrderIdAsync(categoryId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        } catch (ArgumentNullException ex)
        {
            this._logger.LogError(ex.Message);
            return BadRequest("Null Exception");
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(nameof(MethodUrl.GetBatch))]
    public async Task<IActionResult> GetListByBatchAsync([FromBody] IList<string> idList)
    {
        try
        {
            var result =  await this._logicProvider.GetListByBatchAsync(idList);
            if (result == null || result.Count == 0)
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
