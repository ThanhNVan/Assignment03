using Assignment03.DataProviders;
using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public class UserPhoneLogicProviders : BaseLogicProvider<UserPhone, IUserPhoneDataProviders>, IUserPhoneLogicProviders
{
    #region [ CTor ]
    public UserPhoneLogicProviders(ILogger<BaseLogicProvider<UserPhone, IUserPhoneDataProviders>> logger, IUserPhoneDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion
}
