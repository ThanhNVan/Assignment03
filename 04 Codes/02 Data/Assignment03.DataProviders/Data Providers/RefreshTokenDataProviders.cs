using Assignment03.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public class RefreshTokenDataProviders : BaseDataProvider<RefreshToken, AppDbContext>, IRefreshTokenDataProviders
{
    #region [ CTor ]
    public RefreshTokenDataProviders(ILogger<BaseDataProvider<RefreshToken, AppDbContext>> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion
}
