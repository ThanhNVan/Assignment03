using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.WebApiProviders;

namespace Assignment03.WebApiProviders;

public class UserPhoneController : BaseWebApiController<UserPhone, IUserPhoneLogicProviders>
{
    #region [ CTor ]
    public UserPhoneController(ILogger<BaseWebApiController<UserPhone, IUserPhoneLogicProviders>> logger, IUserPhoneLogicProviders logicProvider, LogicContext logicContext) : base(logger, logicProvider, logicContext) {
    }
    #endregion
}
