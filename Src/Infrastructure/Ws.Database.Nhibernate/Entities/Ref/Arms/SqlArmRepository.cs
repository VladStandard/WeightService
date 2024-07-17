using ProjectionTools.Specifications;
using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Commands;
using Ws.Database.Nhibernate.Common.Queries.Item;
using Ws.Domain.Models.Entities.Devices.Arms;

namespace Ws.Database.Nhibernate.Entities.Ref.Arms;

public sealed class SqlArmRepository : BaseRepository, IGetItemByUid<Arm>, ISave<Arm>, IUpdate<Arm>, IDelete<Arm>
{
    public Arm GetByUid(Guid uid) => Session.Get<Arm>(uid) ?? new();

    #region Specs

    public IEnumerable<Arm> GetListBySpec(Specification<Arm> spec) =>
        Session.Query<Arm>().Where(spec).OrderBy(i => i.Name).ToList();

    public Arm GetItemBySpec(Specification<Arm> spec) =>
        Session.Query<Arm>().Where(spec).SingleOrDefault() ?? new();

    #endregion

    public Arm Save(Arm item) { Session.Save(item); return item; }
    public Arm Update(Arm item) { Session.Update(item); return item; }
    public void Delete(Arm item) => Session.Delete(item);
}