using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public class OrderItemHttpClientProviders : BaseHttpClientProvider<OrderItem>, IOrderItemHttpClientProviders
{
    #region [ CTor ]
    public OrderItemHttpClientProviders(IHttpClientFactory httpClientFactory, ILogger<BaseHttpClientProvider<OrderItem>> logger, IEncriptionProvider encriptionProvider) : base(httpClientFactory, logger, encriptionProvider) {
        this._entityUrl = EntityUrl.OrderItem;
    }
    #endregion
}
