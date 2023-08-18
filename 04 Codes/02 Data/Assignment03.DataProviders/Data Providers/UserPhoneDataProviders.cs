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

    #region [ Methods - List ]
    public async Task<IList<UserPhone>> GetListByUserIdAsync(string userId) {
        var result = default(IList<UserPhone>);
        try {
            using var context = await this.GetContextAsync();
            result = await context.UsersPhones.AsNoTracking().Where(x => x.UserId == userId).ToListAsync();
            return result;
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return null;
        }
    }
    #endregion
}
