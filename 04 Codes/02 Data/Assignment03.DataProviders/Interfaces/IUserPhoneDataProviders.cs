using Assignment03.EntityProviders;
using SharedLibrary.DataProviders;

namespace Assignment03.DataProviders;

public interface IUserPhoneDataProviders : IBaseDataProvider<UserPhone>
{
    #region [ Methods - List ]
    Task<IList<UserPhone>> GetListByUserIdAsync(string userId);
    #endregion
}
