using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedLibrary.EntityProviders;
using SharedLibrary.WebApiProviders;

namespace Assignment03.WebApiProviders;

public class UserController : BaseWebApiController<User, IUserLogicProviders>
{

    #region [ CTor ]
    public UserController(ILogger<UserController> logger,
                            IUserLogicProviders logicProvider,
                            LogicContext logicContext)
                            : base(logger, logicProvider, logicContext)
    {
    }
    #endregion

    #region [ Methods - Login/ Logout ]
    [AllowAnonymous]
    [HttpPost(nameof(MethodUrl.SignIn))]
    public async Task<IActionResult> SignInAsync([FromBody] SignInModel model)
    {
        try
        {
            var apiResponse = new SignInResponseModel();
            var result = await this._logicProvider.GetSingleBySignInAsync(model);

            if (result == null)
            {
                apiResponse.Success = false;
                apiResponse.Message = "Not correct Email or Password";
                apiResponse.Model = null;
                return BadRequest(apiResponse);
            }

            apiResponse.Success = true;
            apiResponse.Message = "Ok";
            apiResponse.Model = result;
            return Ok(apiResponse);

        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    #endregion

    #region [ Methods - Override ]
    [HttpPost(nameof(MethodUrl.AddNewUser))]
    public async Task<IActionResult> AddNewUserAsync([FromBody] NewUserModel model)
    {
        try
        {
            var result = await this._logicProvider.AddNewUserAsync(model);
            return Ok(result);
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    #endregion

    #region [ Methods - IsDuplicatedEmail ]
    [HttpPost(nameof(MethodUrl.IsDuplicatedEmail))]
    public async Task<IActionResult> IsDuplicatedEmail([FromBody] string email)
    {
        try
        {
            var result = await this._logicProvider.IsDuplicatedEmailAsync(email);
            return Ok(result);
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    #endregion
}
