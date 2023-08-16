using SharedLibrary.EntityProviders;

namespace SharedLibrary.HttpClientProviders;

public interface IBaseHttpClientProvider<TEntity>
    where TEntity : BaseEntity
{
    #region [ Public Methods - CRUD ]
    Task<bool> AddAsync(TEntity entity, string accessToken = "");

    Task<TEntity> GetSingleByIdAsync(string id, string accessToken = "");

    Task<bool> UpdateAsync(TEntity entity, string accessToken = "");

    Task<bool> SoftDeleteAsync(string entityId, string accessToken = "");

    Task<bool> RecoverAsync(string entityId, string accessToken = "");

    Task<bool> DestroyAsync(string entityId, string accessToken = "");

    Task<IList<TEntity>> GetListAllAsync(string accessToken = "");

    Task<IList<TEntity>> GetListIsDeletedAsync(string accessToken = "");

    Task<IList<TEntity>> GetListIsNotDeletedAsync(string accessToken = "");

    Task<int> CountAllAsync(string accessToken = "");

    Task<int> CountIsDeletedAsync(string accessToken = "");

    Task<int> CountIsNotDeletedAsync(string accessToken = "");
    #endregion
}
