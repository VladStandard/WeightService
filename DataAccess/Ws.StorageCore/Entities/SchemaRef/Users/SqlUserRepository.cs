namespace Ws.StorageCore.Entities.SchemaRef.Users;

public sealed class SqlUserRepository : SqlTableRepositoryBase<SqlUserEntity>
{
    #region Item

    public SqlUserEntity GetItemByUsername(string userName)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlEntityBase.Name), userName.ToUpper()));
        return SqlCore.GetItemByCrud<SqlUserEntity>(sqlCrudConfig);
    }
    
    public SqlUserEntity GetItemByNameOrCreate(string username)
    {
        SqlUserEntity user = GetItemByUsername(username);
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

    public IEnumerable<SqlUserEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlUserEntity>(sqlCrudConfig).ToList();
    }

    #endregion
}