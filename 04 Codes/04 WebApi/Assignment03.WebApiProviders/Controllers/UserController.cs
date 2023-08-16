using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.WebApiProviders;

namespace Assignment03.WebApiProviders;

public class UserController : BaseWebApiController<User, IUserLogicProviders>
{
    #region [ CTor ]
    public UserController(ILogger<BaseWebApiController<User, IUserLogicProviders>> logger, IUserLogicProviders logicProvider, LogicContext logicContext) : base(logger, logicProvider, logicContext) {
    }
    #endregion
}
