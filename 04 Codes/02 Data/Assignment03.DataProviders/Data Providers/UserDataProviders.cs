using Assignment03.EntityProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public class UserDataProviders : BaseDataProvider<User, AppDbContext>, IUserDataProviders
{
    #region [ CTor ]
    public UserDataProviders(ILogger<UserDataProviders> logger, IDbContextFactory<AppDbContext> dbContextFactory) : base(logger, dbContextFactory)
    {
    }
    #endregion

    #region [ Methods - Single ]
    public async Task<User> GetSingleBySignInAsync(SignInModel model)
    {
        var result = default(User);
        try
        {
            using var context = await this.GetContextAsync();

            result = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == model.Email &&
                                                            x.PasswordHash == model.Password);
            return result;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return result;
        }
    }

    public async Task<bool> IsDuplicatedEmailAsync(string email)
    {
        var result = default(bool);
        try
        {
            using var context = await this.GetContextAsync();
            result = await context.Users.AsNoTracking().AnyAsync(x => x.Email == email);
            return result;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return false;
        }
    }
    #endregion

    #region [ Methods - Add ]
    public async Task<bool> AddNewUserAsync(NewUserModel model)
    {

        using var dbContext = await this.GetContextAsync();
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            try
            {
                await dbContext.Users.AddAsync(model.User);

                foreach (var item in model.UserPhones)
                {
                    var userPhone = new UserPhone();
                    userPhone.UserId = model.User.Id;
                    userPhone.Phone = item;

                    await dbContext.UsersPhones.AddAsync(userPhone);
                }

                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;

            } catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        });
        return true;
    }
    #endregion
}
