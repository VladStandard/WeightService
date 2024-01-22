using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Users;

public sealed class SqlUserRepository : IUidRepo<UserEntity>
{
    public UserEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemByUid<UserEntity>(uid);
    
    public UserEntity GetItemByUsername(string userName)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(UserEntity.Name), userName.ToUpper()));
        return SqlCoreHelper.Instance.GetItemByCrud<UserEntity>(sqlCrudConfig);
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
    
    public IEnumerable<UserEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCoreHelper.Instance.GetEnumerable<UserEntity>(crud).ToList();
    }
}