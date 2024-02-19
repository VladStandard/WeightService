using Ws.Domain.Models.Entities.Ref;

namespace Ws.Database.Core.Entities.Ref.ZplResources;

public class SqlZplResourceRepository :  BaseRepository, IGetItemByUid<ZplResourceEntity>, IGetAll<ZplResourceEntity>
{
    public ZplResourceEntity GetByUid(Guid uid) => Session.Get<ZplResourceEntity>(uid) ?? new();

    public IEnumerable<ZplResourceEntity> GetAll() => Session.Query<ZplResourceEntity>().OrderBy(i => i.Name).ToList();

    public ZplResourceEntity GetByName(string name) =>
        Session.Query<ZplResourceEntity>().FirstOrDefault(i => i.Name == name) ?? new();
}