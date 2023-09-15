﻿using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLibrary.EntityProviders;
using SharedLibrary.WebApiProviders;

namespace Assignment03.WebApiProviders;

public class UserPhoneController : BaseWebApiController<UserPhone, IUserPhoneLogicProviders>
{
    #region [ CTor ]
    public UserPhoneController(ILogger<UserPhoneController> logger, IUserPhoneLogicProviders logicProvider, LogicContext logicContext) : base(logger, logicProvider, logicContext) {
    }
    #endregion

    #region [ Methods - List ]
    [HttpGet(nameof(MethodUrl.GetListByUserId) + "/{userId}")]
    public async Task<IActionResult> GetListByUserIdAsync(string userId) {
        try {
            var result = await this._logicProvider.GetListByUserIdAsync(userId);
            if (result == null || result.Count <= 0) {
                return NotFound();
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
    #endregion
}
