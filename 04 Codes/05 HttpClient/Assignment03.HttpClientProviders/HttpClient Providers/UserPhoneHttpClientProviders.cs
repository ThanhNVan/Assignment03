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
    public UserPhoneHttpClientProviders(IHttpClientFactory httpClientFactory, ILogger<BaseHttpClientProvider<UserPhone>> logger, IEncriptionProvider encriptionProvider) : base(httpClientFactory, logger, encriptionProvider) {
        this._entityUrl = EntityUrl.UserPhone;
    }
    #endregion

    #region [ Methods - List ]
    public async Task<IList<UserPhone>> GetListByUserIdAsync(string userId, string accessToken = "") {
        try {
            var url = this._entityUrl + MethodUrl.GetListByUserId + userId;

            var httpClient = this.CreateClient(accessToken: accessToken);

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode) {
                var result = JsonConvert.DeserializeObject<IList<UserPhone>>(await response.Content.ReadAsStringAsync());
                return result;
            }

            return null;
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return null;
        }
    }
    #endregion
}
