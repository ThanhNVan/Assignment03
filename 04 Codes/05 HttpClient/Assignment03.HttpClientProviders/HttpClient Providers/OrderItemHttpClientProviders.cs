using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedLibrary.EntityProviders;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public class OrderItemHttpClientProviders : BaseHttpClientProvider<OrderItem>, IOrderItemHttpClientProviders
{
    #region [ CTor ]
    public OrderItemHttpClientProviders(IHttpClientFactory httpClientFactory, ILogger<BaseHttpClientProvider<OrderItem>> logger, IEncriptionProvider encriptionProvider) : base(httpClientFactory, logger, encriptionProvider) {
        this._entityUrl = EntityUrl.OrderItem;
    }
    #endregion

    #region [ Methods - List ]
    public async Task<IList<OrderItem>> GetListByOrderIdAsync(string orderId,string accessToken = "")
    {
        try
        {
            var url = this._entityUrl + MethodUrl.GetListOrderId + orderId;

            var httpClient = this.CreateClient(accessToken: accessToken);

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IList<OrderItem>>(await response.Content.ReadAsStringAsync());
                return result;
            }

            return null;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return null;
        }
    }
    #endregion
}
