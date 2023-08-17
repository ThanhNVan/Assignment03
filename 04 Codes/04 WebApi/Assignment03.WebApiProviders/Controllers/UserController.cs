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
    #region [ Fields ]
    private readonly IAuthenticationProvider _authenticationProvider;   
    #endregion

    #region [ CTor ]
    public UserController(ILogger<BaseWebApiController<User, IUserLogicProviders>> logger, 
                            IUserLogicProviders logicProvider, 
                            LogicContext logicContext, 
                            IAuthenticationProvider authenticationProvider) 
                            : base(logger, logicProvider, logicContext) {
        _authenticationProvider = authenticationProvider;
    }
    #endregion

    #region [ Methods - Login/ Logout ]
    [AllowAnonymous]
    [HttpPost(nameof(MethodUrl.SignIn))]
    public async Task<IActionResult> SignInAsync([FromBody] SignInModel model) {
        try {
            var apiResponse = new SignInResponseModel();
            var result = await this._logicProvider.GetSingleBySignInAsync(model);

            if (result == null) {
                apiResponse.Success = false;
                apiResponse.Message = "Not correct Email or Password";
                apiResponse.Model = null;
                return BadRequest(apiResponse);
            }

            apiResponse.Success = true;
            apiResponse.Message = "Ok";
            apiResponse.Model = result;
            return Ok(apiResponse);

        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    #endregion

    #region [ Methods - Override ]
    [AllowAnonymous]
    [HttpPost(nameof(MethodUrl.Add))]
    public async override Task<IActionResult> AddAsync([FromBody] User entity) {
        var passwordHash = this._authenticationProvider.HashPassword(entity.PasswordHash);
        entity.PasswordHash = passwordHash;

        return await base.AddAsync(entity);
    }
    #endregion
}
