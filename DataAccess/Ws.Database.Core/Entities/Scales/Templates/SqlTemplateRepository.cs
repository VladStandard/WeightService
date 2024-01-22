using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.Templates;

public sealed class SqlTemplateRepository : SqlTableRepositoryBase<TemplateEntity>
{
    public IEnumerable<TemplateEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(TemplateEntity.Title)));
        return SqlCore.GetEnumerable<TemplateEntity>(sqlCrudConfig).ToList();
    }
}