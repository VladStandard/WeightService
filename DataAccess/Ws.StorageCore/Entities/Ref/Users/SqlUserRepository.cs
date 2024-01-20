using Ws.Domain.Models.Entities.Ref;

namespace Ws.StorageCore.Entities.Ref.Users;

public sealed class SqlUserRepository : SqlTableRepositoryBase<UserEntity>
{
    #region Item

    public UserEntity GetItemByUsername(string userName)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(UserEntity.Name), userName.ToUpper()));
        return SqlCore.GetItemByCrud<UserEntity>(sqlCrudConfig);
    }
    
    public UserEntity GetItemByNameOrCreate(string username)
    {
        UserEntity user = GetItemByUsername(username);
        if (user.IsNew)
        {
            user.Name = username;
            user.LoginDt = DateTime.Now;
        }
        SqlCore.SaveOrUpdate(user);
        return user;
    }
    
    #endregion

    #region List

    public IEnumerable<UserEntity> GetEnumerable()
    {
        SqlCrudConfigModel crud = new();
        crud.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<UserEntity>(crud).ToList();
    }

    #endregion
}