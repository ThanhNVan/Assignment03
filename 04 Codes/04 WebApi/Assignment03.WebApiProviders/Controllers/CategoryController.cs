using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.WebApiProviders;

namespace Assignment03.WebApiProviders;

public class CategoryController : BaseWebApiController<Category, ICategoryLogicProviders>
{
    #region [ CTor ]
    public CategoryController(ILogger<BaseWebApiController<Category, ICategoryLogicProviders>> logger, ICategoryLogicProviders logicProvider, LogicContext logicContext) : base(logger, logicProvider, logicContext) {
    }
    #endregion
}
