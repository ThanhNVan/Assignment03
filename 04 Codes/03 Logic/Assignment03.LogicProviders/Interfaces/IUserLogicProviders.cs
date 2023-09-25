using Assignment03.EntityProviders;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public interface IUserLogicProviders : IBaseLogicProvider<User>
{
    #region [ Methods - Single ]
    Task<SignInSuccessModel> GetSingleBySignInAsync(SignInModel model);

    Task<bool> IsDuplicatedEmailAsync(string email);
    #endregion

    #region [ Methods - Add ]
    Task<bool> AddNewUserAsync(NewUserModel model);
    #endregion
}
