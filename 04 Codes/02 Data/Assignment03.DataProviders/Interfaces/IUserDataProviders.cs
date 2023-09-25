using Assignment03.EntityProviders;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public interface IUserDataProviders : IBaseDataProvider<User>
{
    #region [ Methods - Single ]
    Task<User> GetSingleBySignInAsync(SignInModel model);

    Task<bool> IsDuplicatedEmailAsync(string email);
    #endregion

    #region [ Methods - Add ]
    Task<bool> AddNewUserAsync(NewUserModel model);
    #endregion
}
