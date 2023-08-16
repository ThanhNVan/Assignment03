using Assignment03.DataProviders;
using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public class RefreshTokenLogicProviders : BaseLogicProvider<RefreshToken, IRefreshTokenDataProviders>, IRefreshTokenLogicProviders
{
    #region [ CTor ]
    public RefreshTokenLogicProviders(ILogger<BaseLogicProvider<RefreshToken, IRefreshTokenDataProviders>> logger, IRefreshTokenDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion
}
