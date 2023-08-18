using Assignment03.EntityProviders;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public interface IUserPhoneHttpClientProviders : IBaseHttpClientProvider<UserPhone>
{
    #region [ Methods - List ]
    Task<IList<UserPhone>> GetListByUserIdAsync(string userId, string accessToken = "");
    #endregion
}
