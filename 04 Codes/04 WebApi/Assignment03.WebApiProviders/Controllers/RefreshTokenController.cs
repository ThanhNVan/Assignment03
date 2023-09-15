using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.WebApiProviders;

namespace Assignment03.WebApiProviders;

public class RefreshTokenController : BaseWebApiController<RefreshToken, IRefreshTokenLogicProviders>
{
    #region [ CTor ]
    public RefreshTokenController(ILogger<RefreshTokenController> logger, IRefreshTokenLogicProviders logicProvider, LogicContext logicContext) : base(logger, logicProvider, logicContext) {
    }
    #endregion
}
