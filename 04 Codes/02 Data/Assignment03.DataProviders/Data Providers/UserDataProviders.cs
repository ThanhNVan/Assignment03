using Assignment03.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public class UserDataProviders : BaseDataProvider<User, AppDbContext>, IUserDataProviders
{
    #region [ CTor ]
    public UserDataProviders(ILogger<BaseDataProvider<User, AppDbContext>> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion
}
