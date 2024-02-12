using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Lines;

public sealed class SqlLineRepository : BaseRepository, IGetItemByUid<LineEntity>, IGetItemByQuery<LineEntity>
{
    public LineEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<LineEntity>(uid);

    public IEnumerable<LineEntity> GetAll()
    {
        return SqlCoreHelper.Instance.GetEnumerable(
            QueryOver.Of<LineEntity>().OrderBy(i => i.Name).Asc
        );
    }
    public LineEntity GetItemByQuery(QueryOver<LineEntity> query) => SqlCoreHelper.Instance.GetItem(query);
}