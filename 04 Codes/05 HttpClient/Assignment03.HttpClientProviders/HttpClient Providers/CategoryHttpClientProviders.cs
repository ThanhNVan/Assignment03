using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.HttpClientProviders;

namespace Assignment03.HttpClientProviders;

public class CategoryHttpClientProviders : BaseHttpClientProvider<Category>, ICategoryHttpClientProviders
{
    #region [ CTor ]
    public CategoryHttpClientProviders(IHttpClientFactory httpClientFactory,
                                        ILogger<BaseHttpClientProvider<Category>> logger,
                                        IEncriptionProvider encriptionProvider) : base(httpClientFactory, logger, encriptionProvider) {
        this._entityUrl = EntityUrl.Category;
    }
    #endregion
}
