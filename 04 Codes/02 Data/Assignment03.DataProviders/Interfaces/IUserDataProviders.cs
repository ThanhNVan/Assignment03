using Assignment03.EntityProviders;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public interface IUserDataProviders : IBaseDataProvider<User>
{
    #region [ Methods - Single ]
    Task<User> GetSingleBySignInAsync(SignInModel model);
    #endregion
}
