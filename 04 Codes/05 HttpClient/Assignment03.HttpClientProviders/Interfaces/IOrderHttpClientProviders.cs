using Assignment03.EntityProviders;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public interface IOrderHttpClientProviders : IBaseHttpClientProvider<Order>
{
    #region [ Methods - Checkout ]
    Task<IntKeyValueModel> CheckoutAsync(CheckoutModel model, string accessToken = "");
    #endregion
}
