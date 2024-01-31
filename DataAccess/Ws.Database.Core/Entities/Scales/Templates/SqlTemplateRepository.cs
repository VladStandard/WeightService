using Ws.Database.Core.Common.Queries;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Core.Entities.Scales.Templates;

public sealed class SqlTemplateRepository : IGetAll<TemplateEntity>
{
    public TemplateEntity GetById(long id) => SqlCoreHelper.Instance.GetItemById<TemplateEntity>(id);

    public IEnumerable<TemplateEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable<TemplateEntity>(
            DetachedCriteria.For<TemplateEntity>().AddOrder(Order.Asc(nameof(TemplateEntity.Title)))
        ).ToList();
    }
}