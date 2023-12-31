﻿using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedLibrary.EntityProviders;
using SharedLibrary.HttpClientProviders;
using System.Net.Http.Json;

namespace Assignment03.HttpClientProviders;

public class UserHttpClientProviders : BaseHttpClientProvider<User>, IUserHttpClientProviders
{
    #region [ CTor ]
    public UserHttpClientProviders(IHttpClientFactory httpClientFactory, ILogger<BaseHttpClientProvider<User>> logger, IEncriptionProvider encriptionProvider) : base(httpClientFactory, logger, encriptionProvider) {
        this._entityUrl = EntityUrl.User;
    }
    #endregion

    #region [ Methods - SignIn ]
    public async Task<SignInResponseModel> SignInAsync(SignInModel model) {
        var result = default(SignInResponseModel);
        try {
            var url = this._entityUrl + MethodUrl.SignIn;
            var httpClient = this.CreateClient();
            var response = await httpClient.PostAsJsonAsync(url, model);

            if (response.IsSuccessStatusCode) {
                result = JsonConvert.DeserializeObject<SignInResponseModel>(await response.Content.ReadAsStringAsync());
                //result.Data =     
                return result;
            }

            return result;

        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return result;
        }
    }
    public async Task<bool> IsDuplicatedEmailAsync(string email, string accessToken = "")
    {
        var result = default(bool);

        try
        {
            var url = this._entityUrl + MethodUrl.IsDuplicatedEmail;

            var httpClient = this.CreateClient(accessToken: accessToken);
            var response = await httpClient.PostAsJsonAsync(url, email);
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                return result;
            }

            return false;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return result;
        }
    }
    #endregion

    #region [ Methods - Add ]
    public async Task<bool> AddNewUserAsync(NewUserModel model, string accessToken = "")
    {
        var result   = default(bool);

        try
        {
            var url = this._entityUrl + MethodUrl.AddNewUser;

            var httpClient = this.CreateClient(accessToken: accessToken);
            var response = await httpClient.PostAsJsonAsync(url, model);
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                return result;
            }

            return false;
        } catch (Exception ex)
        {
            this._logger.LogError(ex.Message);
            return result;
        }
    }
    #endregion


}
