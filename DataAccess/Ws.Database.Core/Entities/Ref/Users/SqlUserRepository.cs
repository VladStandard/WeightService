using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Users;

public sealed class SqlUserRepository : IGetItemByUid<UserEntity>, IGetAll<UserEntity>
{
    public UserEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<UserEntity>(uid);
    
    public IEnumerable<UserEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            QueryOver.Of<UserEntity>().OrderBy(i => i.Name).Asc
        );
    }
    
    public UserEntity GetItemByUsername(string userName)
    {
        return SqlCoreHelper.Instance.GetItem(
            QueryOver.Of<UserEntity>().WhereRestrictionOn(u => u.Name).IsInsensitiveLike(userName, MatchMode.Exact)
        );
    }
}