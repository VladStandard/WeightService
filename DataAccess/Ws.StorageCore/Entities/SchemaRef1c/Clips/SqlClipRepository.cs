using Ws.StorageCore.Common;
using Ws.StorageCore.Models;
using Ws.StorageCore.OrmUtils;
using Ws.StorageCore.Utils;
namespace Ws.StorageCore.Entities.SchemaRef1c.Clips;

/// <summary>
/// SQL-контроллер таблицы CLIPS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlClipRepository : SqlTableRepositoryBase<SqlClipEntity>
{
    #region Public and private methods

    public SqlClipEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlClipEntity>();

    public IEnumerable<SqlClipEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlClipEntity>(sqlCrudConfig);
    }

    public SqlClipEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<SqlClipEntity>(sqlCrudConfig);
    }

    #endregion
}