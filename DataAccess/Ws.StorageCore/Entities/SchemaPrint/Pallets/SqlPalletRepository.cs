namespace Ws.StorageCore.Entities.SchemaPrint.Pallets;

public sealed class SqlPalletRepository : SqlTableRepositoryBase<SqlPalletEntity>
{

    // #region Public and private methods
    //
    // /// <summary>
    // /// Сохранить этикетку ПЛУ.
    // /// </summary>
    // public void Save(Labels.SqlLabelEntity label)
    // {
    //     if (!label.PluScale.Plu.IsCheckWeight)
    //         label.PluWeighing = null;
    //     SqlCore.Save(label);
    // }
    //
    // public List<Labels.SqlLabelEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    // {
    //     if (sqlCrudConfig.IsResultOrder)
    //         sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
    //     return SqlCore.GetEnumerable<Labels.SqlLabelEntity>(sqlCrudConfig).ToList();
    // }
    //
    // #endregion
}