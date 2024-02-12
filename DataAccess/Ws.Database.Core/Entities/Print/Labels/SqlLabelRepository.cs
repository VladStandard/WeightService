using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Core.Entities.Print.Labels;

public sealed class SqlLabelRepository : BaseRepository, IGetItemByUid<LabelEntity>, IGetListByQuery<LabelEntity>
{
    public LabelEntity GetByUid(Guid uid) => SqlCoreHelper.Instance.GetItemById<LabelEntity>(uid);
    
    public IEnumerable<ViewLabel> GetAllView() => SqlCoreHelper.Instance.GetEnumerable<ViewLabel>();
    
    public IEnumerable<LabelEntity> GetListByQuery(QueryOver<LabelEntity> query) 
        => SqlCoreHelper.Instance.GetEnumerable(query);
}