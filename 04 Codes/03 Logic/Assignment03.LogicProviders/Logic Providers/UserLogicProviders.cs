using Assignment03.DataProviders;
using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public class UserLogicProviders : BaseLogicProvider<User, IUserDataProviders>, IUserLogicProviders
{
    #region [ CTor ]
    public UserLogicProviders(ILogger<BaseLogicProvider<User, IUserDataProviders>> logger, IUserDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion
}
