using Assignment03.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public class UserPhoneDataProviders : BaseDataProvider<UserPhone, AppDbContext>, IUserPhoneDataProviders
{
    #region [ CTor ]
    public UserPhoneDataProviders(ILogger<BaseDataProvider<UserPhone, AppDbContext>> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory) {
    }
    #endregion
}
