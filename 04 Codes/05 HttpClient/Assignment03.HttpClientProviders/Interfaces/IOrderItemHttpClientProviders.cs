using Assignment03.EntityProviders;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public interface IOrderItemHttpClientProviders : IBaseHttpClientProvider<OrderItem>
{
    #region [ Methods - Methods ]
    Task<IList<OrderItem>> GetListByOrderIdAsync(string orderId, string accessToken = "");
    #endregion
}
