using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Users;

public sealed class SqlUserRepository : IGetItemByUid<UserEntity>, IGetAll<UserEntity>
{
    public UserEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<UserEntity>(uid);
    
    public UserEntity GetItemByUsername(string userName)
    {
        DetachedCriteria criteria = DetachedCriteria.For<UserEntity>()
            .Add(SqlRestrictions.Equal(nameof(UserEntity.Name), userName.ToUpper()));
        return SqlCoreHelper.Instance.GetItemByCriteria<UserEntity>(criteria);
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
        DetachedCriteria criteria = DetachedCriteria.For<UserEntity>().AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<UserEntity>(criteria);
    }
}