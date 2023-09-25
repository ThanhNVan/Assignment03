using Assignment03.EntityProviders;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public interface IUserHttpClientProviders : IBaseHttpClientProvider<User>
{
    #region [ Methods - SignIn ]
    Task<SignInResponseModel> SignInAsync(SignInModel model);

    Task<bool> IsDuplicatedEmailAsync(string email, string accessToken = "");
    #endregion

    #region [ Methods - Add ]
    Task<bool> AddNewUserAsync(NewUserModel model, string accessToken = "");
    #endregion
}
