using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Users;

public sealed class SqlUserRepository : IGetItemByUid<UserEntity>, IGetAll<UserEntity>
{
    public UserEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<UserEntity>(uid);
    
    public UserEntity GetItemByUsername(string userName)
    {
        return SqlCoreHelper.Instance.GetItem<UserEntity>(
            DetachedCriteria.For<UserEntity>()
                .Add(SqlRestrictions.Equal(nameof(UserEntity.Name), userName.ToUpper()))
        );
    }
    
    public UserEntity GetItemByNameOrCreate(string username)
    {
        UserEntity user = GetItemByUsername(username);
        if (user.IsNew)
        {
            user.Name = username;
            user.LoginDt = DateTime.Now;
        }
        SqlCoreHelper.Instance.SaveOrUpdate(user);
        return user;
    }
    
    public IEnumerable<UserEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable<UserEntity>(
            DetachedCriteria.For<UserEntity>().AddOrder(SqlOrder.NameAsc())
        );
    }
}