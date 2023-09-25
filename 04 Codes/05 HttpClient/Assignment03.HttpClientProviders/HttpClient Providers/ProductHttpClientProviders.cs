using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedLibrary.EntityProviders;
using SharedLibrary.HttpClientProviders;
using System.Net.Http.Json;

namespace Assignment03.HttpClientProviders;

public class ProductHttpClientProviders : BaseHttpClientProvider<Product>, IProductHttpClientProviders
{
    #region [ CTor ]
    public ProductHttpClientProviders(IHttpClientFactory httpClientFactory, ILogger<BaseHttpClientProvider<Product>> logger, IEncriptionProvider encriptionProvider) : base(httpClientFactory, logger, encriptionProvider) {
        this._entityUrl = EntityUrl.Product;
    }
    #endregion

    #region [ Methods - List ]
    public async Task<IList<Product>> GetListByCategoryIdAsync(string categoryId, string accessToken = "")
    {
        try
        {
            var url = this._entityUrl + MethodUrl.GetListByCategoryId + categoryId;
            var httpClient = this.CreateClient(accessToken: accessToken);

            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IList<Product>>(await response.Content.ReadAsStringAsync());
                return result;
            }

            return null;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<IList<ProductInfoModel>> GetListByBatchAsync(IList<string> idList, string accessToken = "")
    {
        try
        {
            var url = this._entityUrl + MethodUrl.GetBatch;
            var httpClient = this.CreateClient(accessToken: accessToken);

            var response = await httpClient.PostAsJsonAsync(url, idList);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<IList<ProductInfoModel>>(await response.Content.ReadAsStringAsync());
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
