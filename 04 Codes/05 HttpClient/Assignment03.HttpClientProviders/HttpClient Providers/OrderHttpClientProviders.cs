using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedLibrary.EntityProviders;
using SharedLibrary.HttpClientProviders;
using System.Net.Http.Json;
using System.Text.Json;

namespace Assignment03.HttpClientProviders;

public class OrderHttpClientProviders : BaseHttpClientProvider<Order>, IOrderHttpClientProviders
{
    #region [ CTor ]
    public OrderHttpClientProviders(IHttpClientFactory httpClientFactory,
                                    ILogger<BaseHttpClientProvider<Order>> logger,
                                    IEncriptionProvider encriptionProvider) : base(httpClientFactory, logger, encriptionProvider)
    {
        this._entityUrl = EntityUrl.Order;
    }
    #endregion

    #region [ Methods - Checkout ]
    public async Task<IntKeyValueModel> CheckoutAsync(CheckoutModel model, string accessToken = "")
    {
        var result = new IntKeyValueModel()
        {
            Key = 0,
            Value = ""
        };
        try
        {
            var url = this._entityUrl + MethodUrl.Checkout;
            var httpClient = this.CreateClient(accessToken: accessToken);

            var response = await httpClient.PostAsJsonAsync(url, model);
            if (response.IsSuccessStatusCode)
            {
                result =  JsonConvert.DeserializeObject<IntKeyValueModel> (await response.Content.ReadAsStringAsync());
            }
            return result;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return result;
        }
    }
    #endregion
}
