using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedLibrary.EntityProviders;
using SharedLibrary.HttpClientProviders;
using System.Net.Http.Json;

namespace Assignment03.HttpClientProviders;

public class UserPhoneHttpClientProviders : BaseHttpClientProvider<UserPhone>, IUserPhoneHttpClientProviders
{
    #region [ CTor ]
    public UserPhoneHttpClientProviders(IHttpClientFactory httpClientFactory, ILogger<BaseHttpClientProvider<UserPhone>> logger) : base(httpClientFactory, logger) {
        this._entityUrl = EntityUrl.UserPhone;
    }
    #endregion
}
