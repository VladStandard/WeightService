using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Users;

public sealed class SqlUserRepository : IGetItemByUid<UserEntity>, IGetAll<UserEntity>
{
    public UserEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<UserEntity>(uid);
    
    public IEnumerable<UserEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable<UserEntity>(
            QueryOver.Of<UserEntity>().OrderBy(i => i.Name).Asc().DetachedCriteria
        );
    }
    
    public UserEntity GetItemByUsername(string userName)
    {
        return SqlCoreHelper.Instance.GetItem<UserEntity>(
            DetachedCriteria.For<UserEntity>()
                .Add(Restrictions.InsensitiveLike(nameof(UserEntity.Name), userName, MatchMode.Exact))
        );
    }
}