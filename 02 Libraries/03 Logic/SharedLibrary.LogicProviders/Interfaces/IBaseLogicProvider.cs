using SharedLibrary.DataProviders;
using SharedLibrary.EntityProviders;

namespace SharedLibrary.LogicProviders;

public interface IBaseLogicProvider<TEntity> : IBaseDataProvider<TEntity>
    where TEntity : BaseEntity
{
}
