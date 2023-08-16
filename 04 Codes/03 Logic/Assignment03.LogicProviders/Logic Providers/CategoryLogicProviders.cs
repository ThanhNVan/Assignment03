using Assignment03.DataProviders;
using Assignment03.EntityProviders;
using Microsoft.Extensions.Logging;
using SharedLibrary.LogicProviders;

namespace Assignment03.LogicProviders;

public class CategoryLogicProviders : BaseLogicProvider<Category, ICategoryDataProviders>, ICategoryLogicProviders
{
    #region [ CTor ]

    public CategoryLogicProviders(ILogger<BaseLogicProvider<Category, ICategoryDataProviders>> logger,
                                    ICategoryDataProviders dataProvider) : base(logger, dataProvider) {
    }
    #endregion
}
