using Assignment03.EntityProviders;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public interface IUserHttpClientProviders : IBaseHttpClientProvider<User>
{
    #region [ Methods - SignIn ]
    Task<SignInResponseModel> SignInAsync(SignInModel model);
    #endregion
}
