using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.PlusLines;

public sealed class SqlPluLineRepository : IGetItemByQuery<PluLineEntity>, IGetListByQuery<PluLineEntity>
{
    public PluLineEntity GetItemByQuery(QueryOver<PluLineEntity> query) =>
        SqlCoreHelper.Instance.GetItem(query);
    
    public IEnumerable<PluLineEntity> GetListByQuery(QueryOver<PluLineEntity> query) =>
        SqlCoreHelper.Instance.GetEnumerable(query).OrderBy(i => i.Plu.Number);
}