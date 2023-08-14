using SharedLibrary.DataProviders;
using SharedLibrary.EntityProviders;

namespace SharedLibrary.HttpClientProviders;

public interface IBaseHttpClientProvider<TEntity> : IBaseDataProvider<TEntity>
    where TEntity : BaseEntity
{
}
