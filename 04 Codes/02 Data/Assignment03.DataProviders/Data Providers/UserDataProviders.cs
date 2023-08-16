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

    #region [ Methods - Single ]
    public async Task<User> GetSingleBySignInAsync(SignInModel model) {
        var result = default(User);
        try {
            using var context = await this.GetContextAsync();

            result = await context.Users.FirstOrDefaultAsync(x => x.Email == model.Email && 
                                                            x.PasswordHash == model.Password);  
            return result;
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }
    #endregion
}
