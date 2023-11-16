using Ws.StorageCore.Common;
using Ws.StorageCore.Models;
using Ws.StorageCore.OrmUtils;
using Ws.StorageCore.Utils;
namespace Ws.StorageCore.Entities.SchemaRef1c.Boxes;

/// <summary>
/// SQL-контроллер таблицы коробок.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlBoxRepository : SqlTableRepositoryBase<SqlBoxEntity>
{
    #region Public and private methods

    public SqlBoxEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlBoxEntity>();

    public IEnumerable<SqlBoxEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlBoxEntity>(sqlCrudConfig);
    }

    /// <summary>
    /// Получить коробку по полю UID_1C.
    /// </summary>
    public SqlBoxEntity GetItemByUid1C(Guid uid1C)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.EqualUid1C(uid1C));
        return SqlCore.GetItemByCrud<SqlBoxEntity>(sqlCrudConfig);
    }

    #endregion
}