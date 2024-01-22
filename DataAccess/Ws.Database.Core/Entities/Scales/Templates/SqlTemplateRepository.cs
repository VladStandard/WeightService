using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.Templates;

public sealed class SqlTemplateRepository
{
    public TemplateEntity GetById(long id) => SqlCoreHelper.Instance.GetItemById<TemplateEntity>(id);

    public IEnumerable<TemplateEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(TemplateEntity.Title)));
        return SqlCoreHelper.Instance.GetEnumerable<TemplateEntity>(sqlCrudConfig).ToList();
    }
}