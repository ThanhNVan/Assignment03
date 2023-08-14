using SharedLibrary.EntityProviders;

namespace SharedLibrary.DataProviders;

public interface IBaseDataProvider<TEntity>
    where TEntity : BaseEntity
{
    #region [ Public Methods - CRUD ]
    Task<bool> AddAsync(TEntity entity);

    Task<TEntity> GetSingleByIdAsync(string id);

    Task<bool> UpdateAsync(TEntity entity);

    Task<bool> SoftDeleteAsync(string entityId);

    Task<bool> RecoverAsync(string entityId);

    Task<bool> DestroyAsync(string entityId);

    Task<IList<TEntity>> GetListAllAsync();

    Task<IList<TEntity>> GetListIsDeletedAsync();

    Task<IList<TEntity>> GetListIsNotDeletedAsync();

    Task<int> CountAllAsync();

    Task<int> CountIsDeletedAsync();

    Task<int> CountIsNotDeletedAsync();
    #endregion
}
