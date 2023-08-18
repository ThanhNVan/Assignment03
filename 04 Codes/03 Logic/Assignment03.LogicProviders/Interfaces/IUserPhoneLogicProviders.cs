using Assignment03.EntityProviders;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public interface IUserPhoneLogicProviders : IBaseLogicProvider<UserPhone>
{
    #region [ Methods - List ]
    Task<IList<UserPhone>> GetListByUserIdAsync(string userId);
    #endregion
}
