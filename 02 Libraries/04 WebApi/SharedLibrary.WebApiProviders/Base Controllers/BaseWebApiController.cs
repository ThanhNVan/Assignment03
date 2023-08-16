using Assignment03.LogicProviders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLibrary.EntityProviders;
using SharedLibrary.LogicProviders;

namespace SharedLibrary.WebApiProviders;

[ApiController]
[Authorize(Roles = "Admin,Manager,Employee")]
[Route("Api/V1/[controller]")]
public abstract class BaseWebApiController<TEntity, TLogicProvider> : ControllerBase
    where TEntity : BaseEntity
    where TLogicProvider : IBaseLogicProvider<TEntity>
{
    #region [ Fields ]
    protected readonly ILogger<BaseWebApiController<TEntity, TLogicProvider>> _logger;
    protected readonly TLogicProvider _logicProvider;
    protected readonly LogicContext _logicContext;
    #endregion

    #region [ CTor ]
    public BaseWebApiController(ILogger<BaseWebApiController<TEntity, TLogicProvider>> logger,
                                TLogicProvider logicProvider, LogicContext logicContext
        ) {

        this._logger = logger;
        this._logicProvider = logicProvider;
        this._logicContext = logicContext;
    }
    #endregion

    #region [ Public Methods - CRUD ]
    [HttpPost(nameof(MethodUrl.Add))]
    public virtual async Task<IActionResult> AddAsync([FromBody] TEntity entity) {
        try {
            var dbEntity = await this._logicProvider.GetSingleByIdAsync(entity.Id);

            if (dbEntity != null) {
                var error = $"Duplicated {typeof(TEntity)}";
                this._logger.LogError(error);
                return BadRequest(error);
            }

            var result = await this._logicProvider.AddAsync(entity);

            if (result) {
                return Ok("Added");
            } else {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(nameof(MethodUrl.GetSingleById) + "/{id}")]
    public virtual async Task<IActionResult> GetSingleByIdAsync(string id) {
        try {
            var result = await this._logicProvider.GetSingleByIdAsync(id);

            if (result == null) {
                return BadRequest("Empty");
            }

            return Ok(result);

        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut(nameof(MethodUrl.Update))]
    public virtual async Task<IActionResult> UpdateAsync([FromBody] TEntity entity) {
        try {
            var dbEntity = await this._logicProvider.GetSingleByIdAsync(entity.Id);
            if (dbEntity == null) {
                return BadRequest($"Not existed {typeof(TEntity)}");
            }

            var result = await this._logicProvider.UpdateAsync(entity);
            if (result) {
                return Ok("Updated");
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete(nameof(MethodUrl.SoftDelete) + "/{id}")]
    public virtual async Task<IActionResult> SoftDeleteAsync(string id) {
        try {
            var dbEntity = await this._logicProvider.GetSingleByIdAsync(id);

            if (dbEntity == null) {
                var error = $"Duplicated {typeof(TEntity)}";
                this._logger.LogError(error);
                return BadRequest(error);
            }

            var result = await this._logicProvider.SoftDeleteAsync(id);

            if (result) {
                return Ok("Soft Deleted");
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut(nameof(MethodUrl.Recover))]
    public virtual async Task<IActionResult> RecoverAsync([FromBody] string id) {
        try {
            var dbEntity = await this._logicProvider.GetSingleByIdAsync(id);

            if (dbEntity == null) {
                return BadRequest($"Not existed {typeof(TEntity)}");
            }

            var result = await this._logicProvider.RecoverAsync(id);

            if (result) {
                return Ok("Recovered");
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete(nameof(MethodUrl.Destroy) + "/{id}")]
    public virtual async Task<IActionResult> DestroyAsync(string id) {
        try {
            var dbEntity = await this._logicProvider.GetSingleByIdAsync(id);

            if (dbEntity == null) {
                return BadRequest($"Not existed {typeof(TEntity)}");
            }

            var result = await this._logicProvider.DestroyAsync(id);

            if (result) {
                return Ok("Destroyed");
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(nameof(MethodUrl.GetListAll))]
    public virtual async Task<IActionResult> GetListAllAsync() {
        try {
            var result = await this._logicProvider.GetListAllAsync();
            if (result == null || result.Count() <= 0) {
                return NotFound("Empty");
            }

            return Ok(result);
        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(nameof(MethodUrl.GetListIsDeleted))]
    public virtual async Task<IActionResult> GetListIsDeletedAsync() {
        try {
            var result = await this._logicProvider.GetListIsDeletedAsync();
            if (result == null || result.Count() <= 0) {
                return NotFound("Empty");
            }

            return Ok(result);
        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(nameof(MethodUrl.GetListIsNotDeleted))]
    public virtual async Task<IActionResult> GetListIsNotDeletedAsync() {
        try {
            var result = await this._logicProvider.GetListIsNotDeletedAsync();
            if (result == null || result.Count() <= 0) {
                return NotFound("Empty");
            }

            return Ok(result);
        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(nameof(MethodUrl.CountAll))]
    public virtual async Task<IActionResult> CountAllAsync() {
        try {
            var result = await this._logicProvider.CountAllAsync();

            return Ok(result);
        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(nameof(MethodUrl.CountIsDeleted))]
    public virtual async Task<IActionResult> CountIsDeletedAsync() {
        try {
            var result = await this._logicProvider.CountIsDeletedAsync();

            return Ok(result);
        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(nameof(MethodUrl.CountIsNotDeleted))]
    public virtual async Task<IActionResult> CountIsNotDeletedAsync() {
        try {
            var result = await this._logicProvider.CountIsNotDeletedAsync();

            return Ok(result);
        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    #endregion
}
