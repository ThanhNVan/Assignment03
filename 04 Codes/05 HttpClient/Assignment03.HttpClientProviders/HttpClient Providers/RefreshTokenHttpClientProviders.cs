using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public class RefreshTokenHttpClientProviders : BaseHttpClientProvider<RefreshToken>, IRefreshTokenHttpClientProviders
{
    #region [ CTor ]
    public RefreshTokenHttpClientProviders(IHttpClientFactory httpClientFactory, ILogger<BaseHttpClientProvider<RefreshToken>> logger) : base(httpClientFactory, logger) {
        this._entityUrl = EntityUrl.RefreshToken;
    }
    #endregion
}
