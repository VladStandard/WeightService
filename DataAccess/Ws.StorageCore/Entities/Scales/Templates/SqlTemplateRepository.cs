using Ws.Domain.Models.Entities.Scale;

namespace Ws.StorageCore.Entities.Scales.Templates;

public sealed class SqlTemplateRepository : SqlTableRepositoryBase<TemplateEntity>
{
    public List<TemplateEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(TemplateEntity.Title)));
        return SqlCore.GetEnumerable<TemplateEntity>(sqlCrudConfig).ToList();
    }
}