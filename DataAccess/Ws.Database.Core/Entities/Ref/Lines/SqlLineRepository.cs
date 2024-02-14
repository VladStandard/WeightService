using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.Lines;

public sealed class SqlLineRepository : BaseRepository, IGetItemByUid<LineEntity>, IGetItemByQuery<LineEntity>
{
    public LineEntity GetByUid(Guid uid) => SqlCoreHelper.GetItemById<LineEntity>(uid);

    public IEnumerable<LineEntity> GetAll()
    {
        return SqlCoreHelper.GetEnumerable(
            QueryOver.Of<LineEntity>().OrderBy(i => i.Name).Asc
        );
    }
    public LineEntity GetItemByQuery(QueryOver<LineEntity> query) => SqlCoreHelper.GetItem(query);
}