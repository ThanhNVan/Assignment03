using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public class UserHttpClientProviders : BaseHttpClientProvider<User>, IUserHttpClientProviders
{
    #region [ CTor ]
    public UserHttpClientProviders(IHttpClientFactory httpClientFactory, ILogger<BaseHttpClientProvider<User>> logger) : base(httpClientFactory, logger) {
    }
    #endregion
}
