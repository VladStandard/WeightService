namespace Ws.StorageCore.Entities.SchemaScale.Templates;

public sealed class SqlTemplateRepository : SqlTableRepositoryBase<SqlTemplateEntity>
{
    public List<SqlTemplateEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(SqlTemplateEntity.Title)));
        return SqlCore.GetEnumerable<SqlTemplateEntity>(sqlCrudConfig).ToList();
    }
}