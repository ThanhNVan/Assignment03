using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public class UserPhoneHttpClientProviders : BaseHttpClientProvider<UserPhone>, IUserPhoneHttpClientProviders
{
    #region [ CTor ]
    public UserPhoneHttpClientProviders(IHttpClientFactory httpClientFactory, ILogger<BaseHttpClientProvider<UserPhone>> logger) : base(httpClientFactory, logger) {
    }
    #endregion
}
