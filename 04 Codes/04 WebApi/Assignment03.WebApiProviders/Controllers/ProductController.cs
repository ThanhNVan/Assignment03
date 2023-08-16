using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.WebApiProviders;

namespace Assignment03.WebApiProviders;

public class ProductController : BaseWebApiController<Product, IProductLogicProviders>
{
    #region [ CTor ]
    public ProductController(ILogger<BaseWebApiController<Product, IProductLogicProviders>> logger, IProductLogicProviders logicProvider, LogicContext logicContext) : base(logger, logicProvider, logicContext) {
    }
    #endregion
}
