using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public class OrderHttpClientProviders : BaseHttpClientProvider<Order>, IOrderHttpClientProviders
{
    #region [ CTor ]
    public OrderHttpClientProviders(IHttpClientFactory httpClientFactory, ILogger<BaseHttpClientProvider<Order>> logger) : base(httpClientFactory, logger) {
        this._entityUrl = EntityUrl.Order;
    }
    #endregion
}
